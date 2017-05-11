function merged = merge_gmm_model_components(mdl, original_mz, mean_spectrum, mz_thr)
%MERGE_GMM_MODEL_COMPONENTS
%   reduced = MERGE_GMM_MODEL_COMPONENTS(mdl, original_mz, mean_spectrum, mz_thr)

    opts.if_merge = 1;
    opts.if_rem = 0;
    opts.mz_thr = mz_thr;
    [~,~,merged] = post_proc1(original_mz, mean_spectrum, mdl, opts)

end
