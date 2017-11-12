function index = dice(region, ground_truth)
%DICE Dice's index of region similarity
%   index = DICE(region, ground_truth) - region & ground_truth are bool
%   masks representing estimate and ground truth regions.

    index = 2 * sum(region & ground_truth) / (sum(region) + sum(ground_truth));

end