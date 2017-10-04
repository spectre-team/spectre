function reduced = reduce_gmm_by_component_height(mdl, original_mz, mean_spectrum)
%REDUCE_GMM_BY_COMPONENT_HEIGHT
%   reduced = REDUCE_GMM_BY_COMPONENT_HEIGHT(mdl, original_mz, mean_spectrum)

    opts.if_rem = 1;
    opts.if_merge = 0;
    [~,reduced,~] = post_proc1(original_mz, mean_spectrum, mdl, opts)

end
