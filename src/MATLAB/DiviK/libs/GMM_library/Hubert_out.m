function idx=Hubert_out(A)
%outlier detection using Hubert's method

n=length(A);
medn=median(A);
MC = medcouple(A);
Q1=prctile(A,25);
Q3=prctile(A,75);

if MC>0
    low_lim=Q1-1.5*exp(-4*MC)*(Q3-Q1);
    up_lim=Q3+1.5*exp(3*MC)*(Q3-Q1);
else
    low_lim=Q1-1.5*exp(-3*MC)*(Q3-Q1);
    up_lim=Q3+1.5*exp(4*MC)*(Q3-Q1);
end

w1=min(A>low_lim);
w2=max(A<up_lim);

AO=zeros(1,n);
for ii=1:n,
    if A(ii)>medn,
        AO(ii)=(A(ii)-medn)/(w2-medn);
    else
        AO(ii)=(medn-A(ii))/(medn-w1);
    end
end

jj=find(AO < prctile(AO,25) - 1.5*exp(-4*medcouple(AO))*iqr(AO));
ii=find(AO > prctile(AO,75) + 1.5*exp(3*medcouple(AO))*iqr(AO));

idx = zeros(1,n);
idx([jj,ii]) = 1;
idx = logical(idx);

function [mc] = medcouple(x)
% 'medcouple' computes the medcouple measure, a robust measure of skewness
% for a skewed distribution. It takes into account cases where the
% observations are equal to the median of the series.
%
% Data in 'x' are organized so that columns are the time series and rows
% are the time intervals. All series contain the same number of
% observations.
%
% [mc] = medcouple(x) returns the following:
% mc    - vector with the medcouple measure of the data series
%
% Created by Francisco Augusto Alcaraz Garcia
%            alcaraz_garcia@yahoo.com
%
% References:
%
% 1) G. Brys; M. Hubert; A. Struyf (2004). A Robust Measure of Skewness.
% Journal of Computational and Graphical Statistics 13(4), 996-1017.

x = x(:);
[n, c] = size(x);
[s_x,~] = sort(x);
x_med = median(x);
z = s_x - repmat(x_med,n,1);

mc = zeros(1,c);
for w = 1:c,
    [ip,~] = find(z(:,w)>=0); % These are the positions in z of z+
    [im,~] = find(z(:,w)<=0); % These are the positions in z of z-

    p = size(ip,1);
    q = size(im,1);

    [mi, mj] = ind2sub([p,q],1:p*q); % Positions of all combinations of z+
    % and z- as elements in a pxq matrix

    zp = z(ip,w); % z+ repeated to account for all cells in the matrix
    zm = z(im,w); % z- repeated to account for all cells in the matrix

    h = (zp(mi)+zm(mj))./(zp(mi)-zm(mj)); % same size as mi, mj

    [ipz,~]= find(zp==0); % row numbers of z+ = 0, i.e., x_{i} = median(x)
    [imz,~]= find(zm==0); % row numbers of z- = 0, i.e., x_{i} = median(x)
    piz = ismember(mi,ipz); % positions in mi where z+=0
    pjz = ismember(mj,imz); % positions in mi where z-=0
    zmember = piz+pjz; % same size as mi, mj
    pijz = find(zmember == 2); % positions where z+ = z- = 0, i.e., x_{i} =
    % = x_{j} = median(x)
    [indi,indj] = ind2sub([p,q],pijz); % pxq matrix position of the zero entries
    indi = indi - min(indi) + 1; % row position of the zero entries as if they
    % were in a separated matrix
    indj = indj - min(indj) + 1; % column position of the zero entries as if they
    % were in a separated matrix

    for i=1:size(pijz,2),
        if (indi(i) + indj(i) - 1) > size(find(z==0),1),
            h(pijz(i)) = 1;
        elseif (indi(i) + indj(i) - 1) < size(find(z==0),1),
            h(pijz(i)) = -1;
        else
            h(pijz(i)) = 0;
        end
    end
    mc(w) = median(h);
end