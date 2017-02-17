function seg_min = peaks_part(mz,y,gmm_opts,draw)

if nargin<3; gmm_opts = default_gmm_opts(); end

if strcmp(gmm_opts.EM_init,'Peaks')
    if draw; disp(['Phase 1: coarse partition by peaks detection using ' gmm_opts.EM_init ' algorithm.']); end
    mass = proc_peakdetect(mz,y,gmm_opts.thr,gmm_opts.cond1,gmm_opts.cond2);
elseif strcmp(gmm_opts.EM_init,'External')
    if draw; disp(['Phase 1: coarse partition using external peaks.']); end
    mass = gmm_opts.mass;
else 
    error('Wrong type of peak detection.')
end

n = length(mass);
ind = zeros(1,n);
for a=1:n; ind(a) = find(eq(mz,mass(a))); end

seg_min = zeros(1,n-1);
for a=1:n-1
    [~,y_ind] = min(y(ind(a):ind(a+1)));
    seg_min(a) = ind(a)+y_ind-1;  
end
diff_seg = [seg_min(1) diff(seg_min)];
seg_min(diff_seg < 9) = [];


