function [mdl_orig,mdl_area,mdl_height,mdl_merge] = ms_gmm_run(mz,y,opts)

mz = mz(:); y = y(:);
if nargin <3
    opts.base = 1;
    opts.draw = 1;
    opts.mz_thr = 0.3;
    opts.if_merge = 1;
    opts.if_rem = 1;
end

%baseline correction
if opts.base
    y = msbackadj(mz,y,'showplot',opts.draw);
end
y(y<0) = 0;

%run ms_gmm
[ww_gmm,mu_gmm,sig_gmm]=ms_gmm1(mz,y);

%plot results
if opts.draw;
    plot_gmm(mz,y,ww_gmm,mu_gmm,sig_gmm);
    title(['Original model: ' num2str(length(ww_gmm)) ' components.'])
    disp(['Original model: ' num2str(length(ww_gmm)) ' components.'])
end

mdl_orig.w = ww_gmm;
mdl_orig.mu = mu_gmm;
mdl_orig.sig = sig_gmm;
mdl_orig.KS = length(ww_gmm);

%make post-processing
[mdl_area,mdl_height,mdl_merge] = post_proc1(mz,y,mdl_orig,opts);
if opts.draw
    disp(['Merged model: ' num2str(length(mdl_merge.w)) ' components.'])
    disp(['Area-filtered model: ' num2str(length(mdl_area.w)) ' components.'])
    disp(['Height-filtered model: ' num2str(length(mdl_height.w)) ' components.'])
end
