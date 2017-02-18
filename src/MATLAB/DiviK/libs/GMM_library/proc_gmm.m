function [proc,merged,orig,stats] = proc_gmm(mz,y,draw,gmm_opts)

if nargin<3; draw = 0; end
if nargin<4; gmm_opts = default_gmm_opts(); end

mz = mz(:); y = y(:);

%baseline correction
if gmm_opts.base
    f = @(mz) gmm_opts.a + gmm_opts.b .* mz;
    y = msbackadj(mz,y,'WINDOWSIZE',f,'STEPSIZE',f);
end
y(y<0) = 0;

% estimatiting signal components using GMM
[merged,orig] = gaussian_mixture(mz',y,draw,gmm_opts);

% post processing
if draw; disp('Filtering Gaussian component based on CV'); end
proc = cell(length(gmm_opts.tol_s),length(gmm_opts.tol_d)); 
stats = proc;
for a=1:length(gmm_opts.tol_s)
    for b=1:length(gmm_opts.tol_d)
        [proc{a,b},stats{a,b}] = post_proc(merged{a,b},draw);
    end
end
if draw && length(proc)==1 
    figure; plot_spect_vs_decomp(mz,y,proc{1}.alpha,proc{1}.mass,proc{1}.sig,1);
    title(['Processed model. No of components detected: ' num2str(length(proc{1}.alpha))])
    xlabel('M/Z'); ylabel('Intensity')
end