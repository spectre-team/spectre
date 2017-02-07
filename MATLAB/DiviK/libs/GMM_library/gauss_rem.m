function [thr,IC,stats] = gauss_rem(xvec, K,K_noise,draw)
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

if nargin < 3; error('Insufficient number of arguments.'); end
if nargin < 4; draw = 0; end
if K_noise > K
    thr = max(xvec); IC = 0;
    return 
end
if K_noise < 0 || K < 1 
    thr = min(xvec); IC = 0;
    return 
end

xvec = sort(xvec(:));
N = length(xvec);       %nb of measurements

%initial conditions
alpha = rand(1,K); alpha = alpha/sum(alpha);
mi = min(xvec) + range(xvec)*rand(1,K);
mi = sort(mi);
temp = diff([min(xvec) mi max(xvec)]);
sigma = zeros(1,K);
for a=1:K; sigma(a) = max([temp(a) temp(a+1)]); end
    
if draw
    disp('Starting values')
    disp(num2str([alpha; mi; sigma]))
end
   
%histogram of input data
[n,x] = hist(xvec,round(sqrt(N)));

%EM parameters
gmm_opts = default_gmm_opts();
gmm_opts.unif_corr = 0; 
gmm_opts.SW = (min(sigma)^2)/1000;   
gmm_opts.thr_sig2 = 0;
gmm_opts.thr_alpha = 0; 

%main loop
xx = unique(xvec); 
y = zeros(1,length(xx));
for a=1:length(xx); y(a) = sum(eq(xvec,xx(a))); end
[alpha,mi,sigma,~,logL,IC] = EM_iter(xx,y,alpha,mi,sigma,draw,gmm_opts);

%finding threshold
[mi,ind] = sort(mi); alpha = alpha(ind); sigma = sigma(ind);
f_temp = zeros(1e5,K); x_temp = linspace(min(xvec),max(xvec),1e5)';
for k=1:K; f_temp(:,k) = alpha(k)*normpdf(x_temp,mi(k),sigma(k)); end 
temp2 = nan(1,K-1);
for a=1:K-1
    [~,ind]= max(f_temp(:,a:a+1),[],2);    
    temp = x_temp(diff(ind)==1);
    if length(temp) > 1; temp = temp(temp>mi(a) & temp<mi(a+1)); end
    if isempty(temp); temp = NaN; end
    temp2(a) = temp;
end        

if ~K_noise 
    if K == 1
        K_noise = 0;
    elseif K == 2
        K_noise = 1;
    else
        temp3 = [alpha;mi;sigma]';
        idx = kmeans(temp3,2,'emptyaction','singleton','replicates',20);
        K_noise = sum(eq(idx,idx(1)));
    end
end

if isempty(temp2)
    thr = NaN;
else
    thr = temp2(K_noise);
end

stats.thr = temp2;
stats.alpha = alpha;
stats.mu = mi;
stats.sigma = sigma;
stats.logL = logL;
stats.K = K;
stats.K_noise = K_noise;

%drawing results
if draw
    disp('After EM:')
    disp(num2str([alpha; mi; sigma]))
   
    figure; hold on; box on
    bar(x,n,[min(xvec) max(xvec)],'hist');
    plot(x_temp',mean(diff(x))*N*sum(f_temp,2),'b.');
    colors(1:K_noise) = 'r'; colors(K_noise+1:K) = 'g'; 
    for a=1:K
        plot(x_temp',mean(diff(x))*N*f_temp(:,a),colors(a));
    end
    plot(temp2,zeros(1,K-1),'r.')
    title('After EM')
    lines = findobj(gca,'Type','Line');
    set(lines,'LineWidth',2)
    set(get(gca,'Ylabel'),'FontSize',14)
end

function y = normpdf(x,mu,sigma)
y = exp(-0.5 * ((x - mu)./sigma).^2) ./ (2.506628274631 .* sigma);