function summarize_similarity_classes_binding(method, data, xy, partition, approximated_roi, compared_roi, ...
    destination, varargin)
    
    xy = translate_min_to_zero(xy);
    approximated_roi_indication = rows_in(xy, approximated_roi);
    compared_roi_indication = rows_in(xy, compared_roi);
    partition = reshape(partition, size(data,1), 1);
    similarity_classes = approximate_region_binding(method, data, partition, approximated_roi_indication, varargin{:});
%     similarity_classes = 1 - (similarity_classes == min(similarity_classes));
    
    build_filename = @(name) [destination filesep method filesep name];
    
    dump_report(similarity_classes, compared_roi_indication, partition, build_filename('report.csv'));
    dump_visualizations(similarity_classes, xy, compared_roi_indication, build_filename);
    
end

function confusions = get_confusion_matrices(similarity_classes, region)

    unique_classes = length(unique(similarity_classes));
    confusions = NaN(unique_classes, 4);
    
    for ii = 1:unique_classes - 1
        
        prediction_positive = similarity_classes <= ii;
        tp = sum(prediction_positive & region);
        tn = sum(~prediction_positive & ~region);
        fp = sum(prediction_positive & ~region);
        fn = sum(~prediction_positive & region);
        confusions(ii, :) = [tp, tn, fp, fn];
        
    end

end

function coverages = get_pixel_coverages(similarity_classes)
    
    unique_classes = length(unique(similarity_classes));
    coverages = NaN(unique_classes, 2);
    
    for ii = 1:unique_classes - 1
        
        prediction_positive = similarity_classes <= ii;
        number_predicted = sum(prediction_positive);
        coverages(ii, :) = [number_predicted, number_predicted / length(similarity_classes)];
        
    end
    
end

function coverages = get_cluster_coverages(similarity_classes, partition)
    
    unique_similarity_classes = length(unique(similarity_classes));
    unique_classes = unique(partition);
    coverages = NaN(unique_similarity_classes, 2);
    
    for ii = 1:unique_similarity_classes - 1
        
        prediction_positive = rows_in(unique_classes, partition(similarity_classes <= ii));
        number_predicted = sum(prediction_positive);
        coverages(ii, :) = [number_predicted, number_predicted / length(unique_classes)];
        
    end
    
end

function report = get_report(similarity_classes, region, merged_partition)

    confusions = get_confusion_matrices(similarity_classes, region);
    pixel_coverages = get_pixel_coverages(similarity_classes);
    cluster_coverages = get_cluster_coverages(similarity_classes, merged_partition);
    report = array2table([confusions, pixel_coverages, cluster_coverages], ...
        'VariableNames', ...
        {'TP', 'TN', 'FP', 'FN', 'PixelsNumber', 'PixelsPercent', 'ClustersNumber', 'ClustersPercent'});
    
end

function dump_report(similarity_classes, region, merged_partition, filename)
    
    report = get_report(similarity_classes, region, merged_partition);
    mkfiledir(filename);
    writetable(report, filename);
    
end

function visualizations = visualize_similarity_classes(similarity_classes, xy)
    
    unique_classes = length(unique(similarity_classes));
    visualizations = cell(unique_classes, 1);
    
    for ii = 1:unique_classes - 1
        
%         visualizations{ii} = list2img(xy, to_visualizable(similarity_classes <= ii));
        visualizations{ii} = list2img(xy, 255 * uint8(similarity_classes <= ii), uint8(0));
        
    end
    
end

function dump_visualizations(similarity_classes, xy, ~, build_filename)
    
    visualized = visualize_similarity_classes(similarity_classes, xy);
    
    for ii = 1:length(visualized)
        
        imwrite(visualized{ii}, build_filename(['similarity_level_' num2str(ii) '.png']));
%         blended = mark_roi(visualized{ii}, xy, region, 'blend');
%         imwrite(blended, build_filename(['similarity_level_' num2str(ii) '_blend.png']));
%         falsecolor = mark_roi(visualized{ii}, xy, region, 'falsecolor');
%         imwrite(falsecolor, build_filename(['similarity_level_' num2str(ii) '_falsecolor.png']));
        
    end
    
end
