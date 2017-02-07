function [pp_est,mu_est,sig_est] = heu_ic(xx,yy,draw,gmm_opts)

if nargin<3; draw = 0; end
if nargin<4; gmm_opts = default_gmm_opts(); end

%segmentation
seg_min = peaks_part(xx,yy,gmm_opts,draw);
    
N = length(xx);
% correcting borders
h_se = 2.5*max(10,length(xx)/range(xx));
seg_nb = length(seg_min);
if seg_min(1) <= h_se;
    seg_min = seg_min(2:seg_nb);
    seg_nb = seg_nb-1;
end
if seg_min(seg_nb) >= N-h_se;
    seg_min = seg_min(1:seg_nb-1);
    seg_nb = seg_nb-1;
end

if draw
    disp('Coarse partition completed, result shown in figure 1')
    disp([num2str(seg_nb) ' segments found.'])
    figure(1)
    plot(xx,yy,'k')
    hold on
    mmm = max(yy);
    for a=1:length(seg_min)
        plot([xx(seg_min(a)),xx(seg_min(a))],[0 mmm],'r')    
    end
    title('Coarse partition of m/z axis'); xlabel('m/z')
    disp('Phase 2: fine allocation of Gaussian components by segments analysis and BIC')
end

seg_min = [1,seg_min,N+1];
x_temp = cell(1,seg_nb); y_temp = x_temp;
for a=1:seg_nb
    x_temp{a} = xx(seg_min(a):seg_min(a+1)-1);
    y_temp{a} = yy(seg_min(a):seg_min(a+1)-1);
end

gmm_opts.eps_change = 1e-4;
pp_list = cell(1,seg_nb); mu_list = pp_list; sig_list = pp_list; K_list = zeros(1,seg_nb);
parfor a=1:seg_nb
    gmm_opts_tmp = gmm_opts;
    % warp down  
    [x_temp_new,y_temp_new] = warp_down(x_temp{a},y_temp{a});
   
    cmp = 20; stop = 1; b = 1;
    D = nan(1,cmp); fit = D; logL = D;
    alpha = cell(1,cmp); mu = alpha; sigma = alpha;
    while stop
        [pp_ini,mu_ini,sig_ini] = inv_cdf_init(x_temp_new,y_temp_new,b);
        [alpha{b},mu{b},sigma{b},~,logL(b),fit(b)] = EM_iter(x_temp_new,y_temp_new,pp_ini,mu_ini,sig_ini,0,gmm_opts_tmp);

        if b > 1 && (strcmp(gmm_opts_tmp.crit,'BIC') || strcmp(gmm_opts_tmp.crit,'ICL-BIC'))
            D(b) = -2*logL(b-1) + 2*logL(b);
            if 1 - chi2cdf(D(b),3) > 0.05
                stop = 0;
            end
        end
        b = b+1;
    end
    [~,cmp_nb] = min(fit); 
    [pp_est,mu_est,sig_est] = EM_iter(x_temp_new,y_temp_new,alpha{cmp_nb},mu{cmp_nb},sigma{cmp_nb},0,gmm_opts_tmp);

   % trim and sort
   i_trim = find(mu_est > min(x_temp{a}) & mu_est < max(x_temp{a}));
   mu_est = mu_est(i_trim);
   pp_est = pp_est(i_trim);
   sig_est = sig_est(i_trim);
   [mu_list{a},ind] = sort(mu_est);
   pp_list{a} = pp_est(ind);
   sig_list{a} = sig_est(ind);
   K_list(a) = length(pp_list{a});
end

K = 0; count = 1;
for a=1:seg_nb; K = K + K_list(a); end
pp_est = zeros(1,K); mu_est = pp_est; sig_est = pp_est;
for a=1:seg_nb
    if K_list(a) > 0
        pp_est(count:count+K_list(a)-1) = pp_list{a};
        mu_est(count:count+K_list(a)-1) = mu_list{a};
        sig_est(count:count+K_list(a)-1) = sig_list{a};
        count = count + K_list(a);
    end
end
pp_est = pp_est/sum(pp_est);