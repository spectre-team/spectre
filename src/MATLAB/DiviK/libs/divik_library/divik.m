function [ partition, res_struct ] = divik( data, xy, varargin )
%DIVIK Performs filtering & clustering recursively.
%   partition = DIVIK( data, xy )
%	[partition, res_struct] = DIVIK( ___ )
%	partition = DIVIK( ___, 'Name', 'Value' ) - allows to specify additional
%	parameters:
%	- MaxK - maximal number of clusters considered (default 10)
%	- Level - maximal depth of splitting (default 3)
%	- UseLevels - states, whether to use levels of depth or size criterion for
%	stop (default true, uses depth of recurence)
%	- AmplitudeFiltration - states whether to use filtration of low abundance
%	features at top level (default true)
%	- VarianceFiltration - states whether to use adaptive filtration of
%	non-informative features with respect to variance for every region (default
%	true)
%	- PercentSizeLimit - the percentage of inital size used for stopping the
%	algorithm when size criterion is applied (default 0.001 for 0.1% of initial
%	size)
%	- FeaturePreservationLimit - the percentage of features preserved for sure
%	during filtration procedures (default 0.05 for at least 5% of features\
%	preserved)
%	- Metric - metric used in clustering (default 'pearson')
%	- PlotPartitions - states whether to plot partitions at top level (default
%	false)
%	- PlotRecursively - states whether to plot partitions at all levels, works
%	only if PlotPartitions is set true (default false)
%	- DecompositionPlots - states whether to plot decompositions of amplitude
%	and variance at top level (default false)
%	- DecompositionPlotsRecursively - states whether to make decomposition plots
%	at all levels, works only if DecompositionPlots is set true (default false)
%	- MaxComponentsForDecomposition - sets the maximal considered number of
%	components used for decomposition of amplitude and variance (default 10)
%	- OutPath - sets the path to directory where all outputs may be stored
%	(default '.')
%	- CachePath - sets the path to directory where cache may be stored (default
%	'.')
%	Cache - enables caching feature (default true)
%	Verbose - sets verbosity (default false)
%	KmeansOpts - additional options passed to k-means algorithm (see K_MEANS)
%	(default {})


% invisible:
% settings.TransposeData = true; % has to be changed in recursive calls
% settings.PrimaryObjectsNumber = NaN; % has to be updated in top call

    if iscell(data)
        data = unpack_doublecell(data);
    end
    if iscell(xy)
        xy = unpack_doublecell(xy);
    end

	settings = apply_user_configs(@get_defaults,varargin);

	if settings.TransposeData
		data = data';
		xy = xy';
	end

	if isnan(settings.PrimaryObjectsNumber)
		settings.PrimaryObjectsNumber = size(data,1);
	end

	settings.PlotRecursively = settings.PlotRecursively ...
		& settings.PlotPartitions;
	settings.DecompositionPlotsRecursively = ...
		settings.DecompositionPlotsRecursively & settings.DecompositionPlots;

	settings.KmeansOpts = { 'EnableCache', settings.Cache, ...
							'CachePath', settings.CachePath, ...
	 						'Verbose', settings.Verbose, ...
							'Metric', settings.Metric, ...
                            'MaxIter', settings.KmeansMaxIters, ...
							settings.KmeansOpts{:} };

	percent_size_limit = settings.PercentSizeLimit;
	percent_of_features_limit = settings.FeaturePreservationLimit;
	out_path = settings.OutPath;
	perform_denoising = settings.AmplitudeFiltration;
	cacher_path = settings.CachePath;
	max_K = settings.MaxK;
	level = settings.Level;
	primary_objects_number = settings.PrimaryObjectsNumber;
	metric_name = settings.Metric;
	k_means_opts = settings.KmeansOpts;

	[features_number,~] = size(data);

	variance_filtering_threshold = 0;
    amplitude_filtering_threshold = 0;

	if perform_denoising
		if settings.DecompositionPlots
			fig_path = [out_path '\amplitude_decomposition.png'];
		else
			fig_path = '';
		end
		amplitudes = mean(data,2);
		thr = fetch_thresholds(log2(amplitudes), ...
			'Threshold', amplitude_filtering_threshold, ...
			'CachePath',cacher_path, ...
			'FigurePath',fig_path, ...
			'MaxComponents',settings.MaxComponentsForDecomposition, ...
			'Cache', settings.Cache);
		thr = thr{1};
		if length(thr)<1 || sum(log2(amplitudes)>thr(1))/length(amplitudes) ...
				<percent_of_features_limit
			amp_filter = true(features_number,1);
			res_struct.amp_thr = -Inf;
		else
			amp_filter = log2(amplitudes)>thr(1);
			res_struct.amp_thr = thr(1);
		end
		data = data(amp_filter,:);

		res_struct.amp_filter = amp_filter;

		features_number = sum(amp_filter);

	end

	if settings.VarianceFiltration
		if settings.DecompositionPlots
			fig_path = [out_path '\variance_decomposition.png'];
		else
			fig_path = '';
		end
		%var filters fetching
		variances = log2(var(data,1,2));
		thr = fetch_thresholds(variances, ...
			'Threshold',variance_filtering_threshold, ...
			'CachePath',cacher_path, ...
			'FigurePath',fig_path, ...
			'MaxComponents',settings.MaxComponentsForDecomposition, ...
			'Cache', settings.Cache);
		thr = thr{1};
		if length(thr)<1
			var_filter = true(features_number,1);
			res_struct.var_thr = -Inf;
		else
			cur = length(thr);
			var_filter = false(features_number,1);
			while sum(var_filter)/length(var_filter) ...
					< percent_of_features_limit && cur>0
				var_filter = variances>thr(cur);
				res_struct.var_thr = thr(cur);
				cur = cur - 1;
			end
			if cur==0
				var_filter = true(features_number,1);
				res_struct.var_thr = -Inf;
			end
		end
		res_struct.var_filter = var_filter;
	else
		var_filter = true(features_number,1);
	end

	%%clusterization
	[partition,index] = cacher(@optimize_partition,...
							   {
									data(var_filter,:),...
									max_K,...
									metric_name,...
									k_means_opts
								},...
								'CachePath',cacher_path, ...
								'Enabled', settings.Cache);
	res_struct.partition = partition;
	res_struct.index = index;
	res_struct.centroids = fetch_centroids_from_partition(data,partition);
	if index==-Inf
		% rmdir(out_path,'s');
		partition = [];
		res_struct = struct();
		return;
	end

	%%results saving
	if settings.PlotPartitions
		partition_plotter(xy, partition, index, out_path, ...
			['primary_' num2str(sum(var_filter)) '_features' ], true);
	end
	best_K = max(partition);

	if level>1 || ~settings.UseLevels
		passed = cell_settings(settings);
		merged = partition;
		res_struct.subregions = cell(1,best_K);
		for i = 1:best_K
			if sum(partition==i)>(primary_objects_number*percent_size_limit)
				[partitions, r] = divik( ...
					data(:,partition==i), ...
					xy(:,partition==i), ...
					passed{:}, ... %setting copy
					'Level', level-1, ... %override
					'OutPath', [out_path '\' num2str(i)], ... %override
					'AmplitudeFiltration', false, ... %override
					'PlotPartitions', settings.PlotRecursively, ... %override
					'DecompositionPlots', ...
						settings.DecompositionPlotsRecursively, ... %override
					'TransposeData', false); %override

				if ~isempty(fieldnames(r))
					res_struct.subregions{i} = r;
				end
				merged = filthy_merge(merged,partitions,i);
			end
		end
		res_struct.merged = merged;
		partition = merged;

		if settings.PlotPartitions
			partition_plotter(xy, partition, index, out_path, ...
				'downmerged', false);
		end
	end

end

function settings = get_defaults()
	settings = struct();
	settings.MaxK = 10;
	settings.Level = 3;
	settings.UseLevels = true;
	settings.AmplitudeFiltration = true;
	settings.VarianceFiltration = true;
	settings.PercentSizeLimit = 0.001;
	settings.FeaturePreservationLimit = 0.05;
	settings.Metric = 'pearson';
	settings.PlotPartitions = false;
	settings.PlotRecursively = false;
	settings.DecompositionPlots = false;
	settings.DecompositionPlotsRecursively = false;
	settings.MaxComponentsForDecomposition = 10;
	settings.OutPath = '.';
	settings.CachePath = '.';
	settings.Cache = true;
	settings.Verbose = false;
	settings.KmeansOpts = {};
    settings.KmeansMaxIters = 100;
	%invisibles
	settings.TransposeData = true;
	settings.PrimaryObjectsNumber = NaN;
end

function unpacked = unpack_doublecell(doublecell)
    unpacked = cell2mat(cellfun(@cell2mat,doublecell, ...
        'UniformOutput',false)');
end
