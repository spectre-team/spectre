function index = ppv(region, ground_truth)
%PPV Positive predictive value
%   index = PPV(region, ground_truth) - same API as dice

    index = sum(region & ground_truth) / sum(region);

end