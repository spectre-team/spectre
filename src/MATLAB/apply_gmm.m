function modelled = apply_gmm(mdl, data, mz)
%APPLY_GMM applies estimated model on data
%   modelled = APPLY_GMM(mdl, data) returns data modelled using estimated
%   GMM parameters.

    w=mdl.w;
    mu=mdl.mu;
    sig=mdl.sig;
    KS=mdl.KS;

    modelled=zeros(size(data,1), KS);
    for kks=1:KS
        y_gmm = w(kks)*normpdf(mz, mu(kks), sig(kks));
        parfor i=1:size(data,1)
            y=data(i,:);
            wid=y.*y_gmm;
            modelled(i, kks)=sum(wid);
        end
    end

end
