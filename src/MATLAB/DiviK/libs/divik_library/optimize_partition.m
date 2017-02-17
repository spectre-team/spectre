function [partition, index] = optimize_partition(data, max_K, metric_name, k_means_opts)
%OPTIMIZE_PARTITION
%	[partition, index] = OPTIMIZE_PARTITION(data, max_K, metric_name, k_means_opts)
	optimum = -Inf;
% 	[features_number,~] = size(data);
	for K=max_K:-1:2
		%INDEX TO BE FOUND IN NEW VERSION
		%CENTERS TO BE FOUND IN FULL SPACE
		%[ partition, index, centers ] = k_means( K, data(var_filter,:),'furthest',distance_metric,'arithmetic',0.00001,100);

		partition = k_means(data, K, k_means_opts{:}, 'Metric', metric_name);

% 		centers = NaN*zeros(features_number,K);
% 		for i=1:K
% 			centers(:,i) = mean(data(:,partition==i),2);
% 		end

		index = dunn(data,partition,metric_name);

		if (index > optimum && index~=Inf) || K==max_K || (K==2 &&	optimum == -Inf)
			optimum = index;
			optimal_partition = partition;
% 			optimal_centers = centers;
% 			best_K = K;
		end
	end
	partition = optimal_partition;
	index = optimum;
end
