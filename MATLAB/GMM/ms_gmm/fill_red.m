% auxiliary function for filling splitter components in red
function ok=fill_red(ww_v,mu_v,sig_v)

KS=length(ww_v);
[mzl,mzp]=find_ranges(mu_v,sig_v);
dlmz=(mzp-mzl)/100;
mzr=mzl:dlmz:mzp;
py=0*mzr;
for kks=1:KS
   py=py+ww_v(kks)*normpdf(mzr,mu_v(kks),sig_v(kks));
end 
fill(mzr,py,'r')
ok=1;