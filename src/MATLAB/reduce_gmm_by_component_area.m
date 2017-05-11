function reduced = reduce_gmm_by_component_area(mdl, original_mz, mean_spectrum)
%REDUCE_GMM_BY_COMPONENT_AREA
%   reduced = REDUCE_GMM_BY_COMPONENT_AREA(mdl, original_mz, mean_spectrum)

    opts.if_rem = 1;
    [reduced,~,~] = post_proc1(original_mz, mean_spectrum, mdl, opts)

end
