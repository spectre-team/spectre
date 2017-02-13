w=mdl.w;
mu=mdl.mu;
sig=mdl.sig;
KS=mdl.KS;

s=zeros(KS,size(wid_norm,2));
parfor i=1:size(wid_norm,2);
    y=wid_norm(:,i);
    for kks=1:KS
        y_gmm=w(kks)*normpdf(mz,mu(kks),sig(kks));
        wid=y.*y_gmm;
        s(kks,i)=sum(wid);
    end
end


data=s;
data=data';
save('peptydy_data.mat','data','-v7.3');
