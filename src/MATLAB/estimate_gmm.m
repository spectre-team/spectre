function mdl = estimate_gmm(mz, data, merge, remove)
%ESTIMATE_GMM Estimates GMM model for given data
%   mdl = ESTIMATE_GMM(mz, data) - estimates basic GMM model for dataset
%   mdl = ESTIMATE_GMM(mz, data, merge) - allows to specify whether
%   components merging should be applied
%   mdl = ESTIMATE_GMM(mz, data, merge, remove) - allows to specify whether
%   additional shaping components should be removed

    if nargin < 3
        merge = 0;
    end
    if nargin < 4
        remove = 0;
    end

    % wyliczenie widma œredniego do GMM
    meanspec=mean(data,1);

    % GMM
    opts.base = 0;      %if baseline correction
    opts.draw = 1;      %if draw results
    opts.mz_thr = 0.3;  %M/Z threshold for merging
    opts.if_merge = merge;  %if merge components
    opts.if_rem = remove;    %if remove additional components

    mdl = ms_gmm_run1(mz,meanspec,opts);

end