function [mdl_proc,ind_del] = post_proc_gmm(mz,y,mdl,name,opts)

KS = mdl.KS;
switch name
    case 'SNR'
        x = mdl.mu./mdl.sig;
        tail = 'left';
    case 'CV'
        x = 100 * mdl.sig./mdl.mu;
        name = 'CV perc';
        tail = 'right';
    case 'sig'
        x = mdl.sig;
        tail = 'right';
    case 'var'
        x = mdl.sig.^2;
        tail = 'right';
    case 'area'
        x = zeros(KS,1);
        for a=1:KS
            y_tmp = mdl.w(a)*normpdf(mz,mdl.mu(a),mdl.sig(a));
            x(a) = trapz(mz,y_tmp);          
        end
        tail = 'left';
    case 'height'
        x = zeros(KS,1);
        for a=1:KS
            y_tmp = mdl.w(a)*normpdf(mz,mdl.mu(a),mdl.sig(a));
            x(a) = max(y_tmp);          
        end
        tail = 'left';
end

if opts.draw; disp(['Model filtering using components ' name ]); end

% if opts.draw
%     figure; subplot(2,1,1); hist(x,sqrt(KS));
%     xlabel(name); ylabel('Counts')
%     subplot(2,1,2); hist(log(x),sqrt(KS));
%     xlabel(['Log(' name ')']); ylabel('Counts')
% end

if opts.log
    x = log(x);
    disp('Logarithmic transformation of signal.');
end

% outlier detection
if strcmp(opts.out,'Bruffaerts')
    [~,~,idxl,idxr] = Bruffaerts_out(x);
elseif strcmp(opts.out,'Hubert')
    [~,~,idxl,idxr] = Hubert_out(x,1.5,0);
elseif strcmp(opts.out,'Tukey')
    [~,~,idxl,idxr] = Tukey_out(x,1.5);
else
    idxl = false(KS,1); idxr = idxl;
end

if strcmp(tail,'left')
   x_out = x(~idxl);
   if opts.draw; disp([num2str(sum(idxl)) ' left tail outliers removed.']); end
elseif strcmp(tail,'right')
   x_out = x(~idxr);
   if opts.draw; disp([num2str(sum(idxr)) ' right tail outliers removed.']); end
end

% adaptive filtering
max_KS = 7; rep_no = 50;
thr = inf(1,max_KS*rep_no); bic = thr; 
alpha = cell(1,max_KS); %K_noise = thr; 
[KS,~,stats_vec] = par_vec(1:max_KS,1:rep_no);

parfor a=1:stats_vec.iter
    [thr(a),bic(a),stats_tmp] = gauss_rem(x_out, KS(a),0,0);
%     K_noise(a) = stats_tmp.K_noise;
    alpha{a} = stats_tmp.alpha;
end

thr = reshape(thr,stats_vec.len_row,stats_vec.len_col);
bic = reshape(bic,stats_vec.len_row,stats_vec.len_col);
% K_noise = reshape(K_noise,stats_vec.len_row,stats_vec.len_col);
alpha = reshape(alpha,stats_vec.len_row,stats_vec.len_col);

%find optimal no. of components
[~,cmp_nb] = min(nanmedian(bic,2));
[~,ind] = sort(bic(cmp_nb,:));

%correct result by removing small alpha components models
[ind,cmp_nb] = rem_small_alpha(alpha,cmp_nb,ind,bic);
% cmp_nb_noise = K_noise(cmp_nb,ind);

if opts.draw; disp([num2str(cmp_nb) ' components ' name ' model.']); end

%estimating threshold including outlier detection and GMM filtering
thr_filt = thr(cmp_nb,ind); 
if strcmp(tail,'left')
    thr_filt = max(thr_filt,-Inf);
    if thr_filt > quantile(x,.5); thr_filt = -Inf; end
    if sum(idxl) >0; if max(x(idxl)) <= quantile(x,.5)
        thr_filt = max(thr_filt,max(x(idxl))); end; end
    ind_del = x <= thr_filt;
elseif strcmp(tail,'right')
    thr_filt = min(thr_filt,Inf);
    if thr_filt < quantile(x,.5); thr_filt = Inf; end
    if sum(idxr) >0; if min(x(idxr)) > quantile(x,.5)
            thr_filt = min(thr_filt,min(x(idxr))); end; end
    ind_del = x >= thr_filt;
end
if opts.draw; disp([num2str(sum(ind_del)) ' components filtered.']); end
   
%find filtered model
mdl_proc = del_gmm(mdl,ind_del);

if opts.draw
    figure; subplot(2,1,1)
    hist(x,round(sqrt(length(x)))); 
    if opts.log; 
        xlabel(['Log(' name ')']);
        title(['Log ' name ' filter. ' num2str(mdl_proc.KS) ' peaks left.' num2str(mdl.KS-mdl_proc.KS) ' removed.'])
    else
        xlabel(name); 
        title([name ' filter. ' num2str(mdl_proc.KS) ' peaks left.' num2str(mdl.KS-mdl_proc.KS) ' removed.'])
    end
    ylabel('Counts')
    if ~isempty(thr); hold on; plot([thr_filt,thr_filt],ylim,'r'); end
    
    subplot(2,1,2); plot_gmm(mz,y,mdl_proc.w,mdl_proc.mu,mdl_proc.sig);
end

function mdl = del_gmm(mdl,ind)
mdl.w = mdl.w(~ind);
mdl.mu = mdl.mu(~ind);
mdl.sig = mdl.sig(~ind);
mdl.KS = length(mdl.w);

function [var1_out,var2_out,stats] = par_vec(var1,var2)
%[var1_out,var2_out] = par_vec(var1,var2)
%Function for vectorizing 2 variables to work in parfor loop

stats = struct;
len_col = length(var2);
len_row = length(var1);
par_iter = len_row*len_col;
var1_out = nan(1,par_iter); 
var2_out = var1_out;
for a=1:len_col
    var2_out((a-1)*len_row + 1:a*len_row) = var2(a)*ones(1,len_row);
    var1_out((a-1)*len_row + 1:a*len_row) = var1;
end

stats.len_col = len_col;
stats.len_row = len_row;
stats.iter = par_iter;

function [ind,cmp_nb] = rem_small_alpha(alpha,cmp_nb,ind,bic)
%removing small and thin components
alpha_tmp = alpha{cmp_nb,ind(1)};
a=2; 
while sum(alpha_tmp < 1e-3/cmp_nb)
    a = a+1;
    if a>10
        if cmp_nb > 1
            cmp_nb = cmp_nb - 1;
            [~,ind] = sort(bic(cmp_nb,:));
            alpha_tmp = alpha{cmp_nb,ind(1)};
            a=2;
        else
            [~,ind] = sort(bic(cmp_nb,:));
            ind = ind(5);
            break;
        end
    else
        alpha_tmp = alpha{cmp_nb,ind(a)};
    end
end
ind = ind(a);

function [thr,bic,stats] = gauss_rem(xvec, K,K_noise,draw)
%GAUSS_REM  Estimating noise components using Gaussian mixture model
% Input:
% xvec - vector of data (1xn)
% K - number of Gaussian components
% K_noise - number of noise components from left
% draw - if draw results
% Output:
% thr - noise threshold
% bic - Bayesian Information Criterion for estimated model
% stats - additional statistics
% 
% Author: Michal Marczyk
% Michal.Marczyk@polsl.pl

if nargin < 3
    error('Insufficient number of arguments.')
end
if nargin < 4
    draw = 0;
end

xvec = sort(xvec(:));
it = 1;                 %no. of EM iterations
N = length(xvec);       %nb of measurements
epsilon = 1e-5;         %EM stop criterion threshold
bic = Inf;

while bic == Inf || bic == 0
    
    %initial conditions
    alpha = rand(1,K); alpha = alpha/sum(alpha);
    mi = min(xvec) + range(xvec)*rand(1,K);
    mi = sort(mi);
    temp = diff([min(xvec) mi max(xvec)]);
    sigma = zeros(1,K);
    for a=1:K
        sigma(a) = max([temp(a) temp(a+1)]);
    end
    f = zeros(N,K); p = f; L_old = 1;
    
    %histogram of input data
    [n,x] = hist(xvec,round(sqrt(N)));
    for k=1:K; f(:,k) = alpha(k) * normpdf(xvec,mi(k),sigma(k)); end
    f(isnan(f)) = 1e-45; f(f==0) = 1e-45;
        
    if draw
        disp('Starting values')
        disp(num2str([alpha; mi; sigma]))
    end

    %main loop
    while it < 1000   
        px = sum(f,2);
        L_new = sum(log(px),1);
        
        %stop criterion
        if abs(L_new - L_old) < epsilon || isnan(L_new) || isinf(L_new)
            break
        end
        for k=1:K
            p(:,k) = f(:,k)./px;
            sum_pk = sum(p(:,k),1);
            alpha(k) = sum_pk/N;
            mi(k) = sum(xvec.*p(:,k),1)/sum_pk;
            sigma(k) = sqrt(sum(((xvec-mi(k)).^2) .* p(:,k),1)/sum_pk);
            if sigma(k) <= 0; sigma = 1e-5; disp('Sigma too low. Too many components.');end
            f(:,k) = alpha(k) * normpdf(xvec,mi(k),sigma(k)); 
        end
        f(isnan(f)) = 1e-45; f(f==0) = 1e-45;
        L_old = L_new;
        it = it + 1;       
    end
    
    %calculating BIC
    bic = -2*L_old + (3*K - 1)*log(N);
    if bic == Inf || bic == 0
        disp('EM crash. Repeat calculations.')
        it = 1;
    else    %finding threshold
        [mi,ind] = sort(mi); alpha = alpha(ind); sigma = sigma(ind);
        f_temp = zeros(1e5,k); x_temp = linspace(min(xvec),max(xvec),1e5)';
        step_x_temp = [min(diff(x_temp)); diff(x_temp)];
        for k=1:K; f_temp(:,k) = alpha(k)*normpdf(x_temp,mi(k),sigma(k)).*step_x_temp; end 
        temp2 = nan(1,K-1);
        for a=1:K-1
            [~,ind]= max(f_temp(:,a:a+1),[],2);    
            temp = x_temp(diff(ind)==1);
            if length(temp) > 1; temp = temp(temp>mi(a) & temp<mi(a+1)); end
            if isempty(temp); temp = NaN; end
            temp2(a) = temp(1);
        end        
    end
end

if K_noise >= K; 
    thr = max(xvec) + 1e-10;
elseif K_noise >= 0 && K_noise < K
    if K == 1
        K_noise = 0;
    elseif K == 2
        K_noise = 1;
    elseif K_noise == 0
        temp3 = [alpha;mi;sigma]';
        idx = kmeans(temp3,2,'emptyaction','singleton','replicates',20);
        K_noise = sum(idx == idx(1));
    end
elseif K_noise < 0; 
    thr = min(xvec) - 1e-10;     
end

if ~exist('thr','var')
    if isempty(temp2) 
        thr = NaN;
    else
        thr = temp2(K_noise);
    end
end

stats.thr = thr;
stats.alpha = alpha;
stats.mu = mi;
stats.K_noise = K_noise;
stats.K = K;
stats.sigma = sigma;
stats.logL = L_old;

%drawing results
if draw
    disp('After EM:')
    disp(num2str([alpha; mi; sigma]))
    disp(['Iterations: ' num2str(it) ' Stop crit: ' num2str(abs(L_new - L_old))])
    
    figure; hold on; box on
    bar(x,n,[min(xvec) max(xvec)],'hist');
    plot(x_temp',mean(diff(x))*N*sum(f_temp./repmat(step_x_temp,1,K),2),'b.');
    colors(1:K_noise) = 'r'; colors(K_noise+1:K) = 'g'; 
    for a=1:K
        plot(x_temp',mean(diff(x))*N*f_temp(:,a)./step_x_temp,colors(a));
    end
    plot(temp2,zeros(1,K-1),'r.')
    title('After EM')
    lines = findobj(gca,'Type','Line');
    set(lines,'LineWidth',2)
    set(get(gca,'Ylabel'),'FontSize',14)
end

function y = normpdf(x,mu,sigma)
y = exp(-0.5 * ((x - mu)./sigma).^2) ./ (sqrt(2*pi) .* sigma);