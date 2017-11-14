function [approximation, index, fig, all_approximations, best] = approximate_by_dice(labeling, is_ground_truth, make_plot)
%APPROXIMATE_BY_DICE Approximates ground truth by clusters maximizing Dice
%   [approximation, dice, fig] = approximate_by_dice(labeling, is_ground_truth, make_plot)
%   [approximation, dice, fig, all_approximations, best] = approximate_by_dice(labeling, is_ground_truth, make_plot)
    
    labeling = reshape(labeling, [], 1);
    is_ground_truth = reshape(is_ground_truth, [], 1);
    
    if nargin < 3
        
        make_plot = false;
        
    end
    
    labels = sort_labels_descending_by_index(labeling, is_ground_truth, @ppv);

    approximation = false(length(labeling), length(labels) + 1);
    coverage = zeros(1, length(labels) + 1);

    for i=1:length(labels)

        approximation(:, i + 1) = approximation(:, i) | (labeling == labels(i));
        coverage(i + 1) = dice(approximation(:, i + 1), is_ground_truth);

    end

    best = coverage == max(coverage);

    if make_plot
    
        fig = invisible_figure();
        ax = axes(fig);
        plot(ax, ...
            (0:length(labels)) / length(labels), coverage, 'b.', ...
            find(best) / length(labels), coverage(best), 'r.');
    
    end
    
    all_approximations = approximation;
    best = find(best, 1);
    index = coverage(best);
    approximation = approximation(:, best);

end

function labels = sort_labels_descending_by_index(labeling, is_ground_truth, index)

    labels = unique(labeling);
    score = NaN(size(labels));
    
    parfor i=1:length(labels)
        
        cluster_selector = (labeling == labels(i));
        score(i) = feval(index, cluster_selector, is_ground_truth);
        
    end
    
    [~, order] = sort(score, 'descend');
    labels = labels(order);

end