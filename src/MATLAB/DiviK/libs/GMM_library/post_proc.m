function [del_cv,stats] = post_proc(gmm,draw)
% components post-processing

x = 100 * gmm.sig./gmm.mass;

% outlier detection
idx = Hubert_out(x); stats.out_cv = sum(idx);

%filtering
thr = inf(1,5); bic = thr;
K_noise = thr; alpha = cell(1,10);
x_tmp = x(~idx);
for a=1:5
    for b=1:50
        [thr(a,b),bic(a,b),stats_tmp] = gauss_rem(x_tmp, a,0,0);
        K_noise(a,b) = stats_tmp.K_noise;
        alpha{a,b} = stats_tmp.alpha;
    end
end
[~,stats.cmp_nb_cv] = min(nanmedian(bic,2));
[~,ind] = sort(bic(stats.cmp_nb_cv,:));

%removing small alpha components
alpha_tmp = alpha{stats.cmp_nb_cv,ind(1)};
a=2; 
while sum(alpha_tmp < 1e-3/stats.cmp_nb_cv)
    a = a+1;
    if a>10
        if stats.cmp_nb_cv > 1
            stats.cmp_nb_cv = stats.cmp_nb_cv - 1;
            [~,ind] = sort(bic(stats.cmp_nb_cv,:));
            alpha_tmp = alpha{stats.cmp_nb_cv,ind(1)};
            a=2;
        else
            [~,ind] = sort(bic(stats.cmp_nb_cv,:));
            ind = ind(5);
            break;
        end
    else
        alpha_tmp = alpha{stats.cmp_nb_cv,ind(a)};
    end
end
ind = ind(a);

stats.cmp_nb_cv_noise = K_noise(stats.cmp_nb_cv,ind);

thr = thr(stats.cmp_nb_cv,ind);
if thr > quantile(x,0.5)
    if sum(idx) > 0 && thr > max(x(idx)) && max(x(idx)) > quantile(x,0.25)
        thr = max(x(idx));
    end
else
    thr = Inf;
end
ind = x > thr;

stats.thr_cv = thr;
stats.del_cv = sum(ind);
del_cv = del_gmm(gmm,ind);

if draw
    disp([num2str(stats.del_cv) ' wide components filtered.'])
    figure; hold on; box on;
    hist(x,sqrt(length(x))); 
    if ~eq(thr,Inf); lim = ylim; plot([thr,thr],[0,lim(2)],'k','LineWidth',2); end
    xlabel('100*sigma/mean'); ylabel('Counts'); title([num2str(stats.del_cv) ' components filtered'])
end


function gmm = del_gmm(gmm,ind)
gmm.alpha = gmm.alpha(~ind);
gmm.mass = gmm.mass(~ind);
gmm.sig = gmm.sig(~ind);