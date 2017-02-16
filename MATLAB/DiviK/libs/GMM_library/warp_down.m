% warp_down function for warping down a piece of a spectrum
function [mz_out,y_out] = warp_down(mz,y,idx_start)
% warning('off','stats:nlinfit:IllConditionedJacobian')
% warning('off','stats:nlinfit:Overparameterized')
% warning('off','stats:nlinfit:IterationLimitExceeded')
warning('off','stats:statrobustfit:IterationLimit');
warning('off','MATLAB:nearlySingularMatrix');
warning('off','MATLAB:polyfit:RepeatedPointsOrRescale')

mz = mz(:)'; y = y(:)';
% y = y-min(y);                       %warp y to 0
x_diff = mean(diff(mz));
start = mz(1);
n = length(mz);
mz = mz - start + n;                     
check = max(floor(.5/x_diff),5);     %0.5 Da to look ahead in fitting or 5 points
check(check > n/2) = round(n/2);
l_end_point = 1; %ceil(idx/2)+1;      %how much modeled borders should we left
r_end_point = l_end_point;

if nargin < 3
    l_idx_start = 5;
    r_idx_start = 5;
    y_diff = [0 100*diff(y)/max(abs(diff(y)))];
    tmp = find(y_diff(1:round(.05*n)) > 80);
    if ~isempty(tmp); l_idx_start = tmp(1); end
    tmp = find(y_diff(end-round(.05*n)+1:end) < -80);
    if ~isempty(tmp); r_idx_start = round(.05*n)-tmp(end)+1; end
else
    l_idx_start = idx_start;
    r_idx_start = idx_start;
end

l_idx_start(l_idx_start<4 || l_idx_start>.8*n) = 4;
r_idx_start(r_idx_start<4 || r_idx_start>.8*n) = 4;

% left side
[l_res,l_idx] = line_fit(mz,y,l_idx_start,check);
if eq(l_res,1); l_end_point = l_idx; end
[l_mz,l_y] = curve_model(mz,y,l_idx,l_res,l_end_point);

% right side
[r_res,r_idx] = line_fit(mz,fliplr(y),r_idx_start,check);
if eq(r_res,1); r_end_point = r_idx; end
[r_mz,r_y] = curve_model(fliplr(mz),fliplr(y),r_idx,r_res,r_end_point);

r_mz = fliplr(r_mz); r_y = fliplr(r_y);
%merging output
ind = (l_idx-l_end_point+1:n-r_idx+r_end_point);
mz_out = [l_mz mz(ind) r_mz]';
mz_out = mz_out + start - n;
y_out = [l_y y(ind) r_y]';

function [res,idx] = line_fit(mz,y,idx,check)
%fitting line to signal and check the slope
stop = 1;
[~,stats] = robustfit(mz(1:idx),y(1:idx));
mse_old = stats.s;
while stop
    idx = idx + 1;    
    if idx > length(mz)/2; idx = 3; break; end
    
    [~,stats] = robustfit(mz(1:idx),y(1:idx));
    mse = stats.s;
    if mse > mse_old
        mse_tmp = inf(1,check);
        if idx+check > length(mz)/2; check = round(length(mz)/2) - idx; end
        for a=1:check
            [~,stats] = robustfit(mz(1:idx+a),y(1:idx+a));
            mse_tmp(a) = stats.s;
        end
        if sum(mse_tmp < mse_old) == 0
            idx = idx - 1;
            stop = 0;
        elseif mse_tmp(check) < mse_old && mse_tmp(check) < min(mse_tmp)
            idx = idx + check;
            mse_old = mse_tmp(check);
        else
            [~,ind] = min(mse_tmp);
            idx = idx - 1 + ind;
            stop = 0;
        end
    else
       mse_old = mse; 
    end
end

[beta,stats] = robustfit(mz(1:idx),y(1:idx));
if stats.p(2) > 0.1 || (beta(2)>-1 && beta(2)<0)
    res = 1;        %no peaks, just noise or improper shape
elseif beta(2) > 0;
    res = 3;        %proper shape
else
    res = 2;        %improper shape
end

function [mz_out,y_out] = curve_model(mz,y,idx,res,end_point)

x_diff = mean(diff(mz));
if eq(res,1)
    x_pred = mz(1)-floor(y(1))*x_diff:x_diff:mz(1)-x_diff;
    if length(x_pred) > length(mz)
        zero_point =  x_pred(length(x_pred) - length(mz));
        x_pred = mz(1)-round((mz(1)-zero_point)/x_diff)*x_diff:x_diff:mz(1)-x_diff;
    end
    if isempty(x_pred); x_pred = mz(1)-x_diff; end
    y_pred = linspace(0,y(1)-1,length(x_pred));
else
    if eq(res,2)
        beta = robustfit(mz(1:idx),fliplr(y(1:idx)));
    else
        beta = robustfit(mz(1:idx),y(1:idx));
    end
    zero_point = fzero(@(x)fun(x,beta),mz(1));
    x_pred = mz(1)-round((mz(1)-zero_point)/x_diff)*x_diff:x_diff:mz(1)-x_diff;
    if length(x_pred) > length(mz)
        zero_point = x_pred(length(x_pred) - length(mz));
        beta = robustfit([mz(1:idx) zero_point],[y(1:idx) 0]);
        x_pred = mz(1)-round((mz(1)-zero_point)/x_diff)*x_diff:x_diff:mz(1)-x_diff;
    end
    if isempty(x_pred); x_pred = mz(1)-x_diff; end
    y_pred = beta(1)+beta(2)*x_pred;
end

if eq(res,3)
%     mdl = @(a,x)(a(1) + a(2)*x + a(3)*x.^2);
    beta = [0;1;0];
    deg = 2;
else
%     mdl = @(a,x)(a(1) + a(2)*x + a(3)*x.^2 + a(4)*x.^3);
    beta = [0;1;0;0];
    deg = 3;
end
z_len = round(max(idx,length(x_pred))/2);
x_mod = [x_pred(1)-z_len*x_diff:x_diff:x_pred(1)-x_diff x_pred mz(1:idx)];
noise = .1*mean(y_pred).*randn(1,length(y_pred));
y_mod = [zeros(1,z_len) y_pred+noise y(1:idx)];
y_mod(y_mod<0) = 0;

mz_out = x_mod(1:end-end_point);

% nonlinear regression
% options = statset('nlinfit'); 
% options.MaxIter = 1000; 
% options.Robust = 'on';
% [beta,r,~,covv] = nlinfit(x_mod,y_mod,mdl,beta,options);
% y_out = nlpredci(mdl,mz_out,beta,r,'covar',covv);

% polynomial fit
% beta = polyfit(x_mod,y_mod,deg);
% y_out = polyval(beta,mz_out);

% lsq constraint fit
y_fit = fit_constr(x_mod,y_mod,deg,mz_out(end),y_mod(end-end_point),beta);
y_out = y_fit(1:end-end_point);

ind = find(y_out < 0);
if ~isempty(ind);
    if length(ind) > 1
        mz_out(1:ind(end-1)) = [];
        y_out(1:ind(end-1)) = [];
    end
    y_out(1) = 0;
end
mz_out = mz_out(:)'; y_out = y_out(:)';

function y_fit = fit_constr(x,y,n,x0,y0,beta)
%n - degree of polynomial to fit

options = optimset('lsqlin'); 
options = optimset(options,'Display','off','MaxIter',500,'LargeScale','off');

x = x(:); y = y(:);

V(:,n+1) = ones(length(x),1,class(x));
for j = n:-1:1; V(:,j) = x.*V(:,j+1); end

% We use linear equality constraints to force the curve to hit the required point. In
% this case, 'Aeq' is the Vandermoonde matrix for 'x0'
Aeq = x0.^(n:-1:0);
% and 'beq' is the value the curve should take at that point
beq = y0;
p = lsqlin(V,y,[],[],Aeq,beq,[],[],beta,options);
y_fit = polyval( p, x );

function y = fun(x,a)
y = a(1) + a(2)*x;