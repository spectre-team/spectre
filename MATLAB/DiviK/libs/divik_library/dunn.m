function value = dunn( data, partition, metric_name )
%DUNN Calculates Dunn's index for set partition
%	value = DUNN( data, partition, metric_name )
%   AWFUL VERSION
	
	[features_number,~] = size(data);
	K = max(partition);
	centers = NaN(features_number,K);
	for i=1:K
		centers(:,i) = mean(data(:,partition==i),2);
	end
	intracluster_metric = 'MeanRadius';
	intercluster_metric = '';

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