% EM iterations 
function [ppsort,musort,sigsort,l_lik] = g_mix_est_fast_lik(sample,KS,muv,sigv,pp)
% input data:
% sample - vector of observations
% muv,sigv,pp - initial values of mixture parameters
% output:
% musort,sigsort,ppsort - estimated mixture parameters
% l_lik - log likeliood

min_sg=0.1;

mivoc=muv;
sigvoc=max(sigv.^2, min_sg^2);
ppoc=pp;

% make vectors verticsl
if size(sample,1) > 1
    sample=sample';
end

if size(mivoc,2) > 1
    mivoc=mivoc';
end

if size(sigvoc,2) > 1
    sigvoc=sigvoc';
end

if size(ppoc,2) > 1
    ppoc=ppoc';
end



N=length(sample);

% OK oceniamy iteracyjnie wg wzorow z artykulu Bilmsa
pssmac=zeros(KS,N);
change=1;
while change > 1.5e-4;
   oldppoc=ppoc;
   oldsigvoc=sigvoc;
   
   % lower limit for component weights
   ppoc=max(ppoc,0.001);
     
   for kskla=1:KS
      pssmac(kskla,:)=ppoc(kskla)*normpdf(sample,mivoc(kskla),sqrt(sigvoc(kskla)));
   end
   psummac=ones(KS,1)*sum(pssmac,1);
   pskmac=pssmac./psummac;
   ppp=sum(pskmac,2);
   ppoc=ppp/N;
   mivoc=pskmac*sample';
   mivoc=mivoc./ppp;
   sigmac=(ones(KS,1)*sample-mivoc*ones(1,N)).*((ones(KS,1)*sample-mivoc*ones(1,N)));
   for kkk=1:KS
      % lower limit for component variances 
      sigvoc(kkk)=max([pskmac(kkk,:)*sigmac(kkk,:)' min_sg^2]);
   end
   sigvoc=sigvoc./ppp;
 
   %
   change=sum(abs(ppoc-oldppoc))+sum(abs(sigvoc-oldsigvoc))/KS;
end      


% compute likelihood
l_lik=sum(log(sum(pssmac,1))); 

% sort estimates
[musort,isort]=sort(mivoc);
sigsort=sqrt(sigvoc(isort));
ppsort=ppoc(isort);



