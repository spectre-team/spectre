function [partition, index] = optimize_partition(data, max_K, metric_name, k_means_opts)
%OPTIMIZE_PARTITION
%    [partition, index] = OPTIMIZE_PARTITION(data, max_K, metric_name, k_means_opts)
    
    optimum = -Inf;
    
    if are_distances_unimodal(data, metric_name)
        partition = ones(1, size(data, 2));
        index = -Inf;
        return;
    end
    
    for K=max_K:-1:2
        %INDEX TO BE FOUND IN NEW VERSION
        %CENTERS TO BE FOUND IN FULL SPACE

        partition = k_means(data, K, k_means_opts{:}, 'Metric', metric_name);

        index = dunn(data,partition,metric_name);

        if (index > optimum && index~=Inf) || K==max_K || (K==2 && optimum == -Inf)
            optimum = index;
            optimal_partition = partition;
        end
    end
    
    partition = optimal_partition;
    index = optimum;
end

function unimodal = are_distances_unimodal(data, metric_name)

    if strcmp(metric_name, 'pearson')
        distances = pdist(data, 'correlation');
    else
        distances = pdist(data, metric_name);
    end
    
    unimodal = is_unimodal(distances);

end
