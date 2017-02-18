function opts = default_gmm_opts()

%baseline correction
opts.base = 1;      %if remove baseline using window y = a + b*mz
opts.a = 100;       
opts.b = 0;

%Segmentation parameters
opts.EM_init = 'Peaks';  %method of finding segments borders ['Peaks','External']
opts.mass = [];             %when using external peak detection

%EM for GMM parameters
opts.eps_change = 3e-4; %stop criterion threshold
opts.SW = 1e-2;         %weighting coefficient for sigma
opts.unif_corr = 1;     %if nonuniform spaced vector correction
opts.thr_alpha = 1e-5;  %small alpha threshold
opts.thr_sig2 = opts.SW^2;%small sigma threshold
opts.extra_comp = 1;    %if add extra wide components
opts.crit = 'BIC';      %model selection criterion {'BIC','AIC','AICc','ICL-BIC'} 

%Build-in peak detection
opts.thr = 5e-3;    %peak amplitude threshold as % of max intensity 
opts.cond1 = 1.3;   %conditon for small peaks reduction, ratio of max to min intensity of a peak [1-1.5]
opts.cond2 = 1e-2;  %condition for noise peaks reduction, % m/z difference between two peaks with similar intensities

%Merging parameters
opts.merge = 1;     %if merge components
opts.tol_s = 0.1;   %tolerance for probability density function [0.1-0.5]
opts.tol_d = 0.4;   %tolerance for first derivative of the pdf [0.1-0.8]

%Post-processing
opts.post = 1;      %if post-processing components