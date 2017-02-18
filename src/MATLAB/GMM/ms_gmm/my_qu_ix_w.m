%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
%        auxiliary function - computing quality index for dynamic programming
%
%
function wyn=my_qu_ix_w(invec,yinwec,PAR,PAR_sig_min)
invec=invec(:);
yinwec=yinwec(:);
if (invec(length(invec))-invec(1))<=PAR_sig_min || sum(yinwec)<=1.0e-3
    wyn=inf;
else
   wwec=yinwec/(sum(yinwec));
   wyn1=(PAR+sqrt(sum(((invec-sum(invec.*wwec)).^2).*wwec)))/(max(invec)-min(invec));
   wyn=wyn1;
end