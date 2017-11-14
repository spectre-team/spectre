function similarity_classes = wrapped_exroi_binding(data, partition, approximated_region, varargin)
    
    representative = mean(data(approximated_region, :), 1);
    [centroids, partition] = reorganize(data, partition);
    similarity_classes = exroi(representative, centroids, partition, varargin{:});
    
end

function [centroids, partition] = reorganize(data, filthy_partition)
    
    merged = filthy_partition;
    partition = NaN(size(merged));
    class_names = unique(merged);
    centroids = NaN(length(class_names), size(data, 2));
    
    for ii = 1:length(class_names)
        
        selection = merged == class_names(ii);
        assert(sum(selection) > 0);
        partition(selection) = ii;
        centroids(ii, :) = mean(data(selection, :), 1);
        
    end
    
    assert(all(all(~isnan(centroids))));
    
end