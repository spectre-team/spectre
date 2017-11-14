function [similarity_classes, thresholds, distances] = exroi(representative, centroids, partition, varargin)
%EXROI estimates similarity classes w.r.t. a representative
%   similarity_classes = EXROI(representative, centroids, partition)
%   [similarity_classes, thresholds, distances] = EXROI(representative, centroids, partition)
%   ___ = EXROI(___, Name, Value) - allows to specify optional parameters:
%   - Metric - metric w.r.t. which similarity is considered. Supported all
%   of the metrics from pdist.
%   - GMM_ParameterName - parameters passed to GMM decomposition prefixed
%   with 'GMM_'. For more details see fetch_thresholds.
%   NOTICE: There is an assumption made, that centroids can be indexed by
%   partition vector content

    options = apply_user_configs(@get_defaults, varargin);
    distances = pdist2(representative, centroids, options.Metric)';
    thresholds = fetch_thresholds(distances, ...
                                  'MaxComponents', options.GMM_MaxComponents, ...
                                  'CachePath', options.GMM_CachePath, ...
                                  'Cache', options.GMM_Cache, ...
                                  'FigurePath', options.GMM_FigurePath, ...
                                  'Threshold', options.GMM_Threshold);
    thresholds = thresholds{1};
    similarity_classes = zeros(size(partition));
    t_partition = transpose(partition);
    for ii = 1:length(thresholds)
        
        worse_class_centroids = find(distances >= thresholds(ii));
        worse_class_observations = rows_in(t_partition, worse_class_centroids);
        similarity_classes(worse_class_observations) = ii;
        
    end
    similarity_classes = similarity_classes' + 1;

end

function options = get_defaults()

    options = struct();
    options.Metric = 'correlation';
    options.GMM_MaxComponents = 10;
    options.GMM_CachePath = '.';
    options.GMM_Cache = false;
    options.GMM_FigurePath = '';
    options.GMM_Threshold = 0;

end