function [merged,orig] = gaussian_mixture(x,y,draw,gmm_opts)
% GAUSSIAN_MIXTURE(X,Y)
% Computes Gaussian mixture decomposition of the MALDI proteomic mass
% spectrum. Initial conditions obtained by heuristic step using dynamic
% programming or peak detection. Gaussian mixture decomposition by
% EM algoritm. Merging Gaussian components based on L-inf distance
% INPUT:
% x - m/z values [1,n]
% y - corresponding ion counts [1,n]
% draw - if draw results
% gmm_opts - gmm parameters
% OUTPUT:
% merged - model of merged components
% orig - model before merging
% Both output structures contain following fields:
% pp_est - vector of esimated weights (probabilities)
% mu_est - vector of estimated component means
% sig_est - vector of estimated standard deviations of components
% bic - value of the Bayesian information criterion

% INPUT CONTROL
if length(x) ~= length(y); error('x and y must be vectors of the same lengths.'); end
if min(size(x))>1 || min(size(y))>1; error('x and y must be vectors.'); end
if sum(y)<1; error('Wrong intensity(y) values.'); end
if min(y)<0; error('y must be non negative.'); end
N=length(x);
if min((x(2:N)-x(1:N-1)))<=0; error('Improperly spaced vector x.'); end

% INPUT CORRECTION IF NECESSARY
x = x(:); y = y(:);

if draw
    disp('Input control..... OK')
    disp('Estimating initial condition for EM algorithm.')
end

% poszukiwanie warunkow poczatkowych
[pp_v, mu_v, sig_v] = find_ic(x,y,draw,gmm_opts);
    
KS=length(pp_v);
if draw
    disp(['Estimation of initial values completed: ' num2str(KS) ' components detected'])
    disp('Gaussian mixture decomposition by EM algoritm')
end

if gmm_opts.extra_comp
    extra_nb = round(0.1 * length(pp_v));
    pp_v = [pp_v mean(pp_v)*ones(1,extra_nb)];
    pp_v = pp_v/sum(pp_v);
    mu_v_tmp = linspace(min(x),max(x),extra_nb+2);
    mu_v = [mu_v mu_v_tmp(2:end-1)];
    sig_v = [sig_v mean(diff(mu_v_tmp))*ones(1,extra_nb)];
    [mu_v,ind] = sort(mu_v);
    pp_v = pp_v(ind); sig_v = sig_v(ind);
end
gmm_opts.SW = 0;
[pp_est,mu_est,sig_est,~,logL,bic] = EM_iter(x,y,pp_v,mu_v,sig_v,draw,gmm_opts);
orig.alpha = pp_est; orig.mass = mu_est; orig.sig = sig_est;
orig.logL = logL; orig.bic = bic;
if draw; 
    disp(['EM iterations completed: ' num2str(length(pp_est)) ' components']);
    figure(3)
    plot_spect_vs_decomp(x,y,pp_est,mu_est,sig_est,1);
    title(['Original model. No of components detected: ' num2str(length(pp_est))])
    xlabel('M/Z'); ylabel('Intensity')
end

if gmm_opts.merge
    if draw; disp('Merging Gaussian components based on L-inf similarity');end   

    [tol_s_tmp,tol_d_tmp,stats] = par_vec(gmm_opts.tol_s,gmm_opts.tol_d);
    pp = cell(1,stats.iter); mu = pp; sig = pp; logL = pp; bic = pp;
    if draw; parfor_progress(stats.iter); end;
    parfor a=1:stats.iter
        [pp_tmp,mu_tmp,sig_tmp] = merging(pp_est,mu_est,sig_est,tol_s_tmp(a),tol_d_tmp(a));
        [pp{a},mu{a},sig{a},~,logL{a},bic{a}] = EM_iter(x,y,pp_tmp,mu_tmp,sig_tmp,0,gmm_opts);
        if draw; parfor_progress; end
    end

    merged = cell(1,stats.iter);
    for a=1:stats.iter
        merged{a}.alpha = pp{a};
        merged{a}.mass = mu{a};
        merged{a}.sig = sig{a};
        merged{a}.logL = logL{a};
        merged{a}.bic = bic{a};
    end
    merged = reshape(merged,stats.len_row,stats.len_col);
    if draw && length(merged)==1; disp([num2str(length(orig.alpha)-length(merged{1}.alpha)) ' components merged.']); end
else
    merged = [];
end

function [pp_est,mu_est,sig_est] = merging(pp_est,mu_est,sig_est,tol_s,tol_d)

[pp_est,mu_est,sig_est]=anal_g_merge(pp_est,mu_est,sig_est,tol_s,tol_d);
while 1
   tmp = length(pp_est);
   [pp_est,mu_est,sig_est] = anal_g_merge(pp_est,mu_est,sig_est,tol_s,tol_d);
   if length(pp_est) == tmp; break; end
end