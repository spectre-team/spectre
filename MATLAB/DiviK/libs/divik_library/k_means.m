function [ partition, centers ] = k_means( data, K, varargin)
%k_means Performs k-means algorithm
%	[ partition, centers ] = K_MEANS( data, K ) - Performs k-means
%	algorithm with specified distance metrics and specified mean. Returns
%	assignment of the points to specific clusters and centers of the
%	clusters. Data points should be organized in columns, features in rows.
%	K - number of clusters into which the data has to be split. Can be a
%	vector to support multiple number of clusters at once.
%	[ partition, centers ] = K_MEANS( data, K, Name, Value ) - performs the
%	computations with additional parameters. Parameters available:
%	- 'Metric' - name of the metric used during calculations:
%		- 'pearson' - Pearson correletive distance (1-s, where s is Pearson
%		correlation coefficient)
%		- 'spearman' - same as above, however uses Spearman correlation
%		coefficient
%		- 'euclidean' - Euclidean distance
%		- 'cityblock' - see pdist MATLAB function specification
%		- 'minkowski' - see pdist MATLAB function specification
%		- 'chebychev' - see pdist MATLAB function specification
%		- 'mahalanobis' - see pdist MATLAB function specification
%		- 'cosine' - see pdist MATLAB function specification
%		- 'hamming' - see pdist MATLAB function specification
%		- 'jaccard' - see pdist MATLAB function specification
%	- 'Init' - initialization technique:
%		- 'furthest' - builds the linear model of the data. Picks an object
%		with highest error as a first center. The next K-1 points are
%		chosen sequentially in such a way that the lowest distance to
%		existing centers should be maximal. Distance is chosen with respect
%		to the metric given or the additionally specified.
%		- 'random' - picks K objects at random to be the cluster centers
%	- 'StartsNum' - number of starts, makes sense only for random
%	initializations
%	- 'StopCrit' - cell array containing all the stop criterions chosen
%	(default is empty):
%		- 'AssignmentPercentChanging' - stops when less than small
%		percentage of all objects changed their assignment
%		- 'IndicePercentChanging' - stops when the percentage change of the
%		separation indice is small
%	Moreover, stops if following conditions are found to be true:
%		- assignments cycle is detected
%		- no assignment was changed
%		- reached maximal number of iterations
%	- 'MaxIters' - sets maximal number of iterations to be performed
%	(default 100)
%	- 'AssignmentChangeThreshold' - sets threshold for
%	'AssignmentPercentChanging' stop criterion (default 0.1% of items which
%	changed its assignment)
%	- 'IndiceChangeThreshold' - sets threshold for 'IndicePercentChanging'
%	stop criterion (default 0.00001%)
%	- 'Indice' - sets the indice used for 'IndicePercentChanging' stop
%	criterion (default 'dunn'):
%		- 'dunn'
%		- 'silhouette' (not implemented yet!)
%	- 'DunnInter' - sets intercluster distance calculation method from the
%	following (default 'Centers'):
%		- 'Centers' - the distance between the centers of the clusters
%	- 'DunnIntra' - sets intracluster distance calculation method from the
%	following (default 'MeanRadius'):
%		- 'Diameter' - the distance between two furthest points in the
%		cluster
%		- 'Radius' - the distance between the center and the furthest point
%		in the cluster
%		- 'MeanRadius' - the mean distance between the cluster center and
%		points assigned
%	- 'InitializationMetric' - allows to pick different metric for
%	initialization. Default is the same as clustering one. Supports all of
%	the metrics specified for clusterization.
%	- 'Normalization' - normalization of features is used during
%	clusterization process. Since several types are available, therefore
%	some of them were implemented (default 'none'):
%		- 'none'
%		- 'z-score'
%		- 'scaling' - scales features dividing them by magnitude
%	- 'InitNormalization' - normalization of features during initialization
%	is used. Since several types are available, therefore some of them were
%	implemented (default 'none'):
%		- 'none'
%		- 'z-score'
%		- 'scaling' - scales features dividing them by magnitude
%	- 'Verbose' - allows to enable current status printing (default true).
%	- 'CachePath' - allows to use specific location for caching (default
%	is current directory)
%	- 'EnableCache' - allows to enable caching feature (default true)
%	
%	Future extensions:
%	- possibility of disabling cache
%	- cache optimization
%	- custom initialization centers
%	- custom metrics
%	- custom indices
	
	%settings initialization
	settings = set_defaults();
	
	%user-settings reading
	settings = apply_user_configs(settings,varargin);
	
	%checking cache for existing results
	[cached_centers, partitions, iter_num, centers_known] = fetch_result_from_cache(data,K,settings);
	
	%centers initialization
	[centers, prepared_data] = internal_init(K,...
											 data,...
											 settings,...
											 cached_centers,...
											 centers_known);
    
	[features_number,objects_number] = size(prepared_data);
	% we store the indexes, because user may want to end when convergence
	% is low and the index changes very slightly
	n_indexes = indexes_needed(settings);
	if n_indexes>0
		indexes = -Inf(settings.MaxIters,n_indexes);
	else
		indexes = NaN;
	end
	
	if settings.Verbose
		fprintf('Initialized.\n')
		fprintf('Iteration: 00000\n')
	end
	
	% we store all of the partitions obtained, because user may want to end
	% when convergence is low or just to detect assignment cycles
	
	stop = false;
	
	while ~stop
		% we find new partition (stored for every iteration because we have
		% to be able to find loops in partitioning) and new centers
		[partitions(iter_num,:),centers{iter_num+1}] = internal_repartition(K,...
															  prepared_data,...
															  centers{iter_num},...
															  settings.Metric);
		% we calculate index specified by user in the settings if any is
		% specified
		if n_indexes>0
			indexes(iter_num,:) = internal_indexes(K,...
												   partitions,...
												   centers{iter_num+1},...
												   prepared_data,...
												   settings);
		end
		% we check stop condition
		stop = check_stop_conditions(partitions,...
									 indexes,...
									 iter_num,...
									 settings);
		iter_num = iter_num+1;
		if settings.Verbose
			fprintf('\b\b\b\b\b\b%05d\n',iter_num)
		end
	end
	
	check_and_save(data,partitions,centers,K,settings);
	
	partition = partitions(iter_num-1,:);
	centers = internal_denormalize(centers{iter_num},...
								   data,...
								   settings.Normalization);
	
	if settings.Verbose
		fprintf('Clusterization of %d objects described by %d features into %d partitions done in %d iterations.\n', ...
			objects_number,...
			features_number,...
			K,...
			iter_num-1);
	end
end

function settings = set_defaults()
%SET_DEFAULTS Sets default settings of the algorithm.
	
	settings.Metric = 'pearson';
	settings.Init = 'furthest';
	settings.InitializationMetric = '';
	settings.StartsNum = 1;
	settings.StopCrit = {};
	settings.Indice = 'dunn';
	settings.IndiceChangeThreshold = 0.00001;
	settings.Normalization = 'none';
	settings.InitNormalization = 'none';
	settings.Verbose = true;
	settings.MaxIters = 100;
	settings.AssignmentChangeThreshold = 0.1;
	settings.DunnInter = 'Centers';
	settings.DunnIntra = 'MeanRadius';
	settings.CachePath = '.';
	settings.EnableCache = true;
	
end

function settings = apply_user_configs(settings,configs)
%APPLY_USER_CONFIGS Applies all of the features specified by the user.
%	settings = APPLY_USER_CONFIGS(settings,configs)
	
	nvararg = length(configs);
	
	for i = 1:nvararg
		if mod(i,2)==1
			property_name = configs{i};
		else
			settings.(property_name) = configs{i};
		end
	end
	
	settings.IndiceChangeThreshold = settings.IndiceChangeThreshold/100;
	settings.AssignmentChangeThreshold = settings.AssignmentChangeThreshold/100;
	if strcmp(settings.InitializationMetric,'')
		settings.InitializationMetric = settings.Metric;
	end
	
end

%to be extended
function [centers, partitions, iter_num, centers_known] = fetch_result_from_cache(data,K,settings)
%FETCH_RESULT_FROM_CACHE Seeks cache for ready results
%	[centers, partitions, iter_num, centers_known] = fetch_result_from_cache(data,K,settings)
	
	if ~settings.EnableCache
		%same as for absent cache
		[~,objects_number] = size(data);
		centers = NaN*ones(1,K);
		partitions = NaN*ones(settings.MaxIters,objects_number);
		iter_num = 1;
		centers_known = 0;
		return;
	end
	
	try
		data_hash = DataHash(data);
	catch
		warning('K-means: Input data hash calculation failed. Skipping any partial results checks.');
		%same as for absent cache
		[~,objects_number] = size(data);
		centers = NaN*ones(1,K);
		partitions = NaN*ones(settings.MaxIters,objects_number);
		iter_num = 1;
		centers_known = 0;
		return;
	end
		
	path = [settings.CachePath '\kmeans_cache'];
	%init path construction
	path = [path '\' data_hash];
	path = [path '\' settings.Init];
	path = [path '\' settings.InitializationMetric];
	path = [path '\' settings.InitNormalization];
	init_path = [path '\init.mat'];
	%result path construction
	path = [path '\' settings.Metric];
	path = [path '\' settings.Normalization];
	path = [path '\' num2str(K)];
	result_path = [path '\result.mat'];
	
	result_exist = exist(result_path,'file');
	init_exist = exist(init_path,'file');
	
	if result_exist~=0
		load(result_path);
		% if the results were cached yet and are surely enough
		if settings.MaxIters<length(results.terminal)
			
			centers{1} = results.centers{settings.MaxIters+1};
			partitions = results.partitions(1:settings.MaxIters,:);
			iter_num = settings.MaxIters;
			centers_known = K;
			
		%if the results were not necessarily cached, but we recall last
		%best possible result
		else
			
			centers = results.centers;
			partitions = results.partitions;
			iter_num = length(results.terminal)-1;
			centers_known = K;
			
		end
	elseif init_exist~=0
		load(init_path);
		[~,w] = size(inits.centers);
		[~,objects_number] = size(data);
		%if the init isn't fully cached
		if w<K
			
			%if any center is cached, it is cached as index array
			centers = inits.centers;
			partitions = NaN*ones(settings.MaxIters,objects_number);
			iter_num = 1;
			centers_known = w;
			
		%if the init is fully cached
		else
			
			%if any center is cached, it is cached as index array
			centers = inits.centers(1:K);
			partitions = NaN*ones(settings.MaxIters,objects_number);
			iter_num = 1;
			centers_known = K;
			
		end
	%if cache is absent
	else
		
		[~,objects_number] = size(data);
		centers = NaN*ones(1,K);
		partitions = NaN*ones(settings.MaxIters,objects_number);
		iter_num = 1;
		centers_known = 0;
		
	end
	
end

function stop = check_stop_conditions(partitions,indexes,iter_num,settings)
%CHECK_STOP_CONDITIONS
%	stop = CHECK_STOP_CONDITIONS(partitions,data,settings)
	
	stop = false;
	
	% simple iterations number check
	if iter_num >= settings.MaxIters
		stop = true;
		return;
	end
	
	% we check if the change comparison is necessary and if it is possible
	% to check it
	if indexes_needed(settings) && iter_num>1
		% we compare change in the index
		if abs((indexes(iter_num)-indexes(iter_num-1)))/indexes(iter_num-1)<settings.IndiceChangeThreshold;
			stop = true;
			return;
		end
	end
	
	% we check for too small change in partition
	percentage_assgnmt_chck = false;
	for i=1:length(settings.StopCrit)
		percentage_assgnmt_chck = percentage_assgnmt_chck || strcmp(settings.StopCrit,'AssignmentPercentChanging');
	end
	if percentage_assgnmt_chck && iter_num>1
		if sum(partitions(iter_num-1,:) == partitions(iter_num,:)) >= length(partitions(iter_num,:))*(1-settings.AssignmentChangeThreshold);
			stop = true;
			return;
		end
	end
	
	% we check for the partition loop
	for i=(iter_num-1):-1:1
		if  sum(partitions(i,:) == partitions(iter_num,:)) == length(partitions(iter_num,:))
			stop = true;
			return;
		end
	end
	
end

function centers_idx = internal_init_regression_furthest( K, data, metric_name, centers, centers_known )
%internal_init_regression_furthest Initialized k-means algorithm using
%linear regression
%	centers_idx = internal_init_regression_furthest( K, data, metric_name, centers, centers_known )
%   Finds the point which is the furthest from the approximation of
%   dataset, then finds next points taking the points with maximal minimal
%   distance to the chosen yet.

	[features_number,objects_number] = size(data);
	
	centers_idx = NaN(1,K);
	centers_idx(1:centers_known) = centers(1:centers_known);
	centers = NaN(features_number,K);
	centers(:,1:centers_known) = data(:,centers_idx(1:centers_known));
	
	if centers_known<1
		b = regress(transpose(data(1,:)),[ones(objects_number,1) transpose(data(2:end,:))]);
		guesses = transpose([ones(objects_number,1) transpose(data(2:end,:))]*b);
		differences_between_prediction_and_guess = abs(guesses - data(1,:));

		maxindex = find(differences_between_prediction_and_guess(:)==max(differences_between_prediction_and_guess(:)));
		maxindex = maxindex(1);
		centers(:,1) = data(:,maxindex);
		centers_idx(1) = maxindex;
		centers_known = 1;
	end
	
	if centers_known<1
		error('Furthest init failed: no initial center found.');
	else
		dst = any_dist(metric_name,data(:,centers_idx(~isnan(centers_idx))),data);
		minimal_distances = min(dst,[],1);
	end
	
	for i = (centers_known+1):K
		max_ind = find(minimal_distances == max(minimal_distances), 1);
		centers(:,i) = data(:,max_ind);
		centers_idx(i) = max_ind;
		dst = any_dist(metric_name,data(:,max_ind),data);
		minimal_distances = min([minimal_distances; dst],[],1);
	end

end

function n = indexes_needed(settings)
%INDEXES_NEEDED Checks, how many indexes have to be calculated
%	n = INDEXES_NEEDED(settings)
	
	n = 0;
	for i=1:length(settings.StopCrit)
		n = n + strcmp(settings.StopCrit,'IndicePercentChanging');
	end
	
end

%to be extended
function [ centers, prepared_data ] = internal_init(K,data,settings,centers,centers_known)
%INTERNAL_INIT Initializes starting parameters for k-means algorithm
%   [ centers, prepared_data ] = INTERNAL_INIT(K,data,settings,centers,centers_known)
	
	init_type = settings.Init;
	init_normalization_type = settings.InitNormalization;
	normalization_type = settings.Normalization;
	init_metric_name = settings.InitializationMetric;
	
	
% 	[features_number,objects_number] = size(data);
	[~,objects_number] = size(data);
	
	%clustering data normalization
	if strcmp(normalization_type,'none')
		prepared_data = data;
	elseif strcmp(normalization_type,'z-score')
		prepared_data = zscore(data,1,2);
	elseif strcmp(normalization_type,'scaling')
		prepared_data = bsxfun(@rdivide,data,max(data,[],2));
	else
		error('Unknown data normalization type (for clustering process).');
	end
	
	if centers_known>=K
		if ~iscell(centers)
			centers = {prepared_data(:,centers)};
		end
		return;
	end
	
	%initialization data normalization
	if strcmp(init_normalization_type,'none')
		init_data = data;
	elseif strcmp(init_normalization_type,'z-score')
		init_data = zscore(data,1,2);
	elseif strcmp(init_normalization_type,'scaling')
		init_data = bsxfun(@rdivide,data,max(data,[],2));
		
% % 		ANOTHER VERSION - whole data is scaled by the same number
% 		init_data = data/max(max(abs(data)));
	else
		error('Unknown data normalization type (for initialization process).');
	end
	
	%centers finding
	if strcmp(init_type,'random')
		tmp = 1:objects_number;
		tmp(centers) = 0;
		tmp = tmp(tmp~=0);
		idx = [centers, randsample(tmp,K - centers_known)];
	elseif strcmp(init_type,'furthest')
		idx = internal_init_regression_furthest(K,init_data,init_metric_name,centers,centers_known);
	else
		error('Unknown initialization type.');
	end
	
	check_init_and_save(data,idx,settings);
	
	centers = {prepared_data(:,idx)};
end

function [partition, centers] = internal_repartition(K,data,old_centers,metric_name)
%INTERNAL_REPARTITION Calculates new partitioning of the points along the clusters
%   [partition, centers] = INTERNAL_REPARTITION(K,data,old_centers,metric_name)
%	Uses specified metric to assign each data point to one of the clusters.

    [~,objects_number] = size(data);
    partition = NaN*zeros(1,objects_number);
	
	dists = any_dist(metric_name,old_centers,data);
	mins = min(dists);
	for i = 1:K
		partition(dists(i,:)==mins) = i;
	end
	
    centers = internal_centers(data,partition,K,'arithmetic');
end

function [ centers ] = internal_centers( data, partition, K, mean_name )
%INTERNAL_CENTERS Calculates centers for given data and its partitioning
%	[ centers ] = INTERNAL_CENTERS( data, partition, K, mean_name )
%   Calculates centers of the clusters basing on input data, its
%   partitioning, using specified mean. Now uses only arithmetic or
%   harmonic mean. Should consider case when clusters disappear.

    [features_number,~] = size(data);
    centers = NaN*zeros(features_number,K);
    
	for i = 1:K
		if strcmp(mean_name,'arithmetic')
			centers(:,i) = mean(data(:,partition==i),2);
		elseif strcmp(mean_name,'harmonic')
			centers(:,i) = harmmean(data(:,partition==i),2);
		elseif strcmp(mean_name,'median')
			centers(:,i) = median(data(:,partition==i),2);
		else
			error('Unknown center calculation method.');
		end
	end
    
end

%to rework
function [ value ] = internal_dunn_index( K, partition, centers, data, metric_name, intercluster_metric, intracluster_metric ) %#ok<INUSL>
%INTERNAL_DUNN_INDEX Calculates Dunn's index
%	value = INTERNAL_DUNN_INDEX( K, partition, centers, data, metric_name, intercluster_metric, intracluster_metric )
%   Calculates Dunn's index for current partition of given dataset. As
%   intracluster distance the mean distance between any point and the
%   center is chosen (or other specified), as intercluster distance, the
%   distance between the centers is chosen.
%   TO REWORK. (WHEN LOWEST RAM USAGE SPECIFIED, IT USES FULL RAM + CREATES
%   CACHE EVERY TIME)
	
	clusters = cell(1,K);
	indexes = cell(1,K);
	for i=1:K
		part = partition==i;
		if sum(part)<2
			value = -Inf;
			return;
		end
		indexes{i} = find(part);
		clusters{i} = data(:,indexes{i});
	end
	
	% %INTRACLUSTER DISTANCE
	
	%DIAMETER
	intracluster_distances = -Inf(1,K);
	if strcmp(intracluster_metric,'Diameter')
		cache = any_dist(metric_name,data,data);
		ccache = cell(K,1);
		for i=1:K
			query = partition==i;
			ccache{i} = cache(query,query);
		end
		parfor c_num=1:K
			intracluster_distances(c_num) = max(max(ccache{c_num}));
		end
	%RADIUS
	elseif strcmp(intracluster_metric,'Radius')
		parfor c_num=1:K
			intracluster_distances(c_num) = max(any_dist(metric_name,clusters{c_num},centers(:,c_num)));
		end
	%MEAN RADIUS
	elseif strcmp(intracluster_metric,'MeanRadius')
		parfor c_num=1:K
			cluster = clusters{c_num};
			center = centers(:,c_num);
			intracluster_distances(c_num) = mean(any_dist(metric_name,cluster,center));
		end
	else
		error('Unknown intracluster distance measure for Dunn''s index calculation.');
	end
	
	
	max_intracluster_distance = max(intracluster_distances);
	
	% % INTERCLUSTER DISTANCE
	intercluster_distances = any_dist(metric_name,centers,centers);
	intercluster_distances = max(Inf*eye(size(intercluster_distances)),intercluster_distances);
	min_intercluster_distance = min(min(intercluster_distances));
	
	value = min_intercluster_distance / max_intracluster_distance;	
end

%to be extended
function indexes = internal_indexes(K,partitions,centers,prepared_data,settings)
	
	warning('IndicePercentChanging feature is implemented only partially. It is recommended to not to use this feature yet.');
	n_stops = length(settings.StopCrit);
	any_index_needed = false;
	for i = 1:n_stops
		any_index_needed = any_index_needed || strcmp(settings.StopCrit,'IndicePercentChanging');
	end
	
	if any_index_needed
		if strcmp(settings.Indice,'dunn')
			indexes(1) = internal_dunn_index( K, partitions(end,:), centers, prepared_data, settings.Metric, settings.DunnIntra, '' );
		elseif strcmp(settings.Indice,'silhouette')
			error('Not implemented yet.');
		else
			error('Unknown indice.');
		end
	end
	
end

function centers = internal_denormalize(centers,data,normalization_type)
%INTERNAL_DENORMALIZE Performs denormalization procedure for the centers
%found.
%	centers = INTERNAL_DENORMALIZE(centers,data,normalization_type)
	
	%clustering data normalization
	if strcmp(normalization_type,'none')
		return;
	elseif strcmp(normalization_type,'z-score')
		centers = bsxfun(@plus,bsxfun(@rdivide,centers,1./std(data,1,2)),mean(data,2));
	elseif strcmp(normalization_type,'scaling')
		centers = bsxfun(@rdivide,centers,1./max(data,[],2));
	else
		error('Unknown data normalization type (for results denormalization process).');
	end
	
end

function check_init_and_save(data,idx,settings)
%CHECK_INIT_AND_SAVE
%	CHECK_INIT_AND_SAVE(data,idx,settings)

	if ~settings.EnableCache
		return
	end
	
	try
		data_hash = DataHash(data);
	catch
		%skipping any partial saves for init
		warning('K-means: Skipping any partial saves for init.');
		return;
	end
	
	path = [settings.CachePath '\kmeans_cache'];
	%init path construction
	path = [path '\' data_hash];
	path = [path '\' settings.Init];
	path = [path '\' settings.InitializationMetric];
	path = [path '\' settings.InitNormalization];
	
	if exist(path,'dir')==0
		mkdir(path);
	end
	
	init_path = [path '\init.mat'];
	
	init_exist = exist(init_path,'file');
	
	if init_exist~=0
		load(init_path);
		if length(inits.centers) < length(idx) %#ok<NODEF>
			inits.centers = idx;
			save(init_path,'inits');
		end
	else
		inits.centers = idx; %#ok<STRNU>
		save(init_path,'inits');
	end
	
end

function check_and_save(data,partitions,centers,K,settings)
%CHECK_AND_SAVE
%	CHECK_AND_SAVE(data,partitions,centers,K,settings)
	
	if ~settings.EnableCache
		return;
	end
	
	try
		data_hash = DataHash(data);
	catch
		warning('K-means: Skipping partial result save.');
		return;
	end
		
	path = [settings.CachePath '\kmeans_cache'];
	%result path construction
	path = [path '\' data_hash];
	path = [path '\' settings.Init];
	path = [path '\' settings.InitializationMetric];
	path = [path '\' settings.InitNormalization];
	path = [path '\' settings.Metric];
	path = [path '\' settings.Normalization];
	path = [path '\' num2str(K)];
	
	if exist(path,'dir')==0
		mkdir(path);
	end
	
	result_path = [path '\result.mat'];
	
	result_exist = exist(result_path,'file');
	
	if result_exist~=0
		load(result_path);
		if length(results.terminal) < length(centers)-1 %#ok<NODEF>
			results.centers = centers;
			results.partitions = partitions;
			results.terminal = false(length(centers)-1,1);
			save(result_path,'results');
		end
	else
		results.centers = centers;
		results.partitions = partitions;
		results.terminal = false(length(centers)-1,1);
		save(result_path,'results');
	end
end