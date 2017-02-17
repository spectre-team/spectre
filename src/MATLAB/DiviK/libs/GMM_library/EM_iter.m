function [pp_est,mu_est,sig_est,TIC,logL,IC] = EM_iter(x,y,alpha,mu,sig,draw,gmm_opts)
% EM_ITER(X,Y,PP_INI,MU_INI,SIG_INI)
% EM algorithm for Gaussian mixture model.
% Input:
% x - m/z values [1,n]
% y - corresponding ion counts [1,n]
% alpha - vector of initial weights (probabilities)
% mu - vector of initial component means
% sig - vector of initial standard deviations of components
% draw- if draw results

if nargin<5; error('Not enough input arguments.'); end 
if nargin<6; draw = 0; end
if nargin<7; gmm_opts = default_gmm_opts(); end

x = x(:); y = y(:)'; alpha = alpha(:)'; mu = mu(:)'; sig = sig(:)';
[x,ind] = sort(x); y = y(1,ind);
N = length(y);        % data length

if gmm_opts.unif_corr
    % correction for nonunifom density
    dens = [(x(2:N)-x(1:N-1));(x(N)-x(N-1))]';
    y = y.*dens;
end

sig2 = sig.^2;
change = 1e20;   
count = 1;
TIC = sum(y);
KS = length(alpha);

if draw; fprintf(1,'EM iter. no. 1. Change:infinity'); end
% MAIN LOOP
while change > gmm_opts.eps_change && count < 10000; 
    
    %removing small and wide or thin components 
    ind = alpha < gmm_opts.thr_alpha | sig2 <= gmm_opts.thr_sig2;
    alpha(ind) = []; mu(ind) = []; sig2(ind) = [];
    KS = length(alpha);
    old_alpha = alpha;
    old_sig2 = sig2;
    
    f = norm_pdf(repmat(x,1,KS),repmat(mu,N,1),repmat(sqrt(sig2),N,1))';
    px = alpha * f;
    px(isnan(px) | px==0) = 5e-324;
    for a=1:KS
       pk = ((alpha(a)*f(a,:)).*y)./px;
       denom = sum(pk);
       mu(a) = (pk*x)/denom;
       sig2num = sum(pk*((x-mu(a)).^2));
       sig2(a) = gmm_opts.SW + sig2num/denom;
       alpha(a) = denom/TIC;
    end
   
    change = sum(abs(alpha-old_alpha)) + sum(((abs(sig2-old_sig2))./sig2))/(length(alpha));
    if draw
        tmp = ceil(log10(count)) + 18;
        for a = 1:tmp; fprintf(1,'\b'); end
        fprintf(1,'%d. Change: %8.2g',count,change);
    end
    count = count+1;
end
if draw; fprintf(1,'\n'); end

% RETURN RESULTS
logL = sum(log(px).*y);
[mu_est,ind] = sort(mu);
sig_est = sqrt(sig2(ind));
pp_est = alpha(ind);
switch gmm_opts.crit
    case 'BIC'
        IC = -2*logL + (3*KS-1)*log(TIC);   
    case 'ICL-BIC'
        pk(isnan(pk) | pk==0) = 5e-324;
        EN = -sum(sum(pk.*log(pk)));
        IC = -2*logL + 2*EN + (3*KS-1)*log(TIC);
    case 'AIC'
        IC = -2*logL + 2*(3*KS-1);   
    case 'AICc'
        IC = -2*logL + 2*(3*KS-1)*(TIC/(TIC-(3*KS-1)-1)); 
    otherwise
        error('Wrong model selection criterion.')
end

function y = norm_pdf(x,mu,sigma)
y = exp(-0.5*(((x - mu)./sigma).^2)) ./(2.506628274631 * sigma);