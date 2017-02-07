function [del,x_corr,left,right] = Bruffaerts_out(x,p,alpha,draw)
% GEN_BOX(X)
% Outlier detection for skewed and heavy-tailed distributions
%
% Input:
% x - data sample [1xn]
% p - robustness of the method with respect to outliers [0.75-0.99, default:0.9]
% alpha - desired detection rate of atypical values in the absence of
% contamination with outliers [default: 0.007]
% draw - if draw results [default: 0]
%
% Output:
% del - indices of outlier measurement
% x_corr - corrected sample
% left - indices of left tail outliers
% right - indices of right tail outliers
%
% Implemented by:
% Michal Marczyk, Michal.Marczyk@polsl.pl
%
% Authors:
% C. Bruffaerts, V. Verardi, C. Vermandele, A generalized boxplot for 
% skewed and heavy-tailed distributions, Statistics & Probability Letters, 
% Volume 95, Pages 110-117

%check inputs
if nargin < 4
    draw = 0;
    if nargin < 3
        alpha = 0.007;
        if nargin < 2
            p = 0.9;
        end
    end
end

x_s = (x-median(x))/iqr(x); %standardize the data
r = x_s - min(x_s) + 0.1;   %shift dataset
r_d = r/(min(r)+max(r));    %standardize r to (0,1) scale
w = norminv(r_d);           %consider inverse normal (probit)
w_s = (w-median(w))/(iqr(w)/1.3426);    %standardize w

%adjust values by Tukey g-and-h distribution
z_p = norminv(p);
q_p1 = quantile(w_s,p);
q_p2 = quantile(w_s,1-p);
g = (1/z_p)*log(-q_p1/q_p2);
h = (2*log(-g*(q_p1*q_p2)/(q_p1+q_p2)))/(z_p^2);

% w_sadj = (1/g)*(exp(g.*w_s)-1).*exp(h*(w_s.^2)/2);
% hist(w_s,0.7*sqrt(length(w_sadj)));

%calculate quantiles of adj. values
q_a(1) = quantile(w_s,alpha/2);
q_a(2) = quantile(w_s,1-alpha/2);
ksi = (1/g)*(exp(g.*q_a)-1).*exp(h*(q_a.^2)/2);

%calculate final extremities
B = (normcdf(median(w)+iqr(w)*ksi/1.3426)*(min(r)+max(r))+min(x_s)-0.1)*iqr(x)+median(x);

%find outliers
left = x<B(1);
right = x>B(2);

del = left | right;
x_corr = x(~del);

if draw
    disp([num2str(sum(left)) ' left tail outliers removed.'])
    disp([num2str(sum(right)) ' right tail outliers removed.'])
    figure; hist(x,sqrt(length(x))); hold on;
    plot([B(1),B(1)],ylim,'r'); plot([B(2),B(2)],ylim,'r')
    xlabel('Data'); ylabel('Counts'); title([num2str(sum(del)) ' outliers removed'])
end

function y=iqr(x)
y = quantile(x,0.75) - quantile(x,0.25);