function similarity_classes = wrapped_dice_approximation_binding(~, partition, approximated_region, varargin)

    similarity_classes = 2 - approximate_by_dice(partition, approximated_region, false);

end