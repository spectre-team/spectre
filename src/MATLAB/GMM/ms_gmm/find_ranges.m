% auxiliary function
% find reasonable ranges for a group of Gaussian components
function [mzlow,mzhigh]=find_ranges(mu_v,sig_v)
K=length(mu_v);
mzlv=zeros(K,1);
mzpv=zeros(K,1);

for kk=1:K
    mzlv(kk)=mu_v(kk)-3*sig_v(kk);
    mzpv(kk)=mu_v(kk)+3*sig_v(kk);
end
mzlow=min(mzlv);
mzhigh=max(mzpv);
return