function [peaksmz peaksy minima] = proc_peakdetect(mz,y,thr,cond1,cond2)
% PEAKDETECT(MZ,Y,THR,COND1,COND2)
%
% This function founds peaks in a mass spectrum spectrum
% using first derivative and then reduces its number using 
% small peaks reduction, low-intensity peaks reduction and/
% noise peaks reduction.
%
% IN:
%   mz - m/z ratios [nx1]
%   y - spectrum intensity [nx1]
%   thr - intensity threshold value [0.01]
%   cond1 - conditon for small peaks reduction [1.25]
%   cond2 - condition for noise peaks reduction [0.001]
%
% Author:
% Michal Marczyk
% michal_marczyk@op.pl

if nargin < 2; help peakdetect; return; end

if nargin < 5
    disp('Default parameter values will be used.')
    thr = 0.01;
    cond1 = 1.3;
    cond2 = 0.001;
end

mz = mz(:);
y = y(:);
thr = thr * max(y);    % threshold for low intensities peaks removal    
sign = diff(y)>0;      % finds all extrema
maxima = find(diff(sign)<0) + 1;                %all maxima
minima = [find(diff(sign)>0)+1 ;length(y)];     %all minima
if minima(1) > maxima(1)
    minima = [1 ;minima];
end

% small peaks removal by slopes
ys = y;
ys(ys<1) = 1;
count = 1;
peak = zeros(1,length(maxima));
for i=1:length(maxima)
    y_check = y(maxima(i));
    if abs(y_check/ys(minima(i)))>cond1 || abs(y_check/ys(minima(i+1)))>cond1
        peak(count) = maxima(i);
        count = count + 1;
    end
end
peak(count:end) = [];

% low-intensity peaks removal
peak = peak(y(peak) > thr);

i = 1;
%noise peaks removal
while i<length(peak)
    match = check([mz(peak(i)) mz(peak(i+1))],[y(peak(i)) y(peak(i+1))],cond1,cond2);
    startpos = i;
    while match == 1
        i = i+1;
        if i<length(peak)
            match = check([mz(peak(startpos)) mz(peak(i+1))],[y(peak(i)) y(peak(i+1))],cond1,cond2);
        else
            break
        end
    end
    endpos = i;
    if endpos-startpos > 0
        sign = diff(y(peak(startpos)-1:peak(endpos)+1))>0;
        maxima = find(diff(sign)<0) + 1;
        [~, ind] = max(y(peak(startpos)+maxima(:)-2));
        peak(endpos) = peak(startpos) - 2 + maxima(ind);
        peak(startpos:endpos-1) = 0;
    end
    i = endpos + 1;    
end
peak(peak == 0) = [];

peaksmz = mz(peak);
peaksy = y(peak);

function match = check(tempmz,tempint,cond1,cond2)
% match test under 2 conditions
match = 0;
if tempint(1)/tempint(2) < cond1 && tempint(1)/tempint(2) >  1/cond1
    if abs(tempmz(1) - tempmz(2)) < cond2*tempmz(1)
        match = 1;
    end
end