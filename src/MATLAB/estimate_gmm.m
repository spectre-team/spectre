function mdl = estimate_gmm(mz, data)
%ESTIMATE_GMM Estimates GMM model for given data
%   mdl = ESTIMATE_GMM(mz, data, mz_thr) - estimates basic GMM model for dataset

    % mean spectrum calculation
    meanspec=mean(data,1);

    % GMM
    opts.base = 0;      %if baseline correction
    opts.draw = 0;      %if draw results
    opts.mz_thr = NaN;  %M/Z threshold for merging
    opts.if_merge = 0;  %if merge components
    opts.if_rem = 0;    %if remove additional components

    mdl = ms_gmm_run1(mz,meanspec,opts);

    mdl.meanspec = meanspec;

end
