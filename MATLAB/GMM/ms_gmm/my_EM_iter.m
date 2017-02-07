%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
%
%
%             EM iterations
%
% EM_iter
% function - iterations of the EM algorithm
%
function [pp_est,mu_est,sig_est,TIC,l_lik,bic]=my_EM_iter(x,y,pp_ini,mu_ini,sig_ini,DRAW,SIG_MIN,eps_change)
%

% VALUES FOR CONSTANTS
% threshold value for terminating iterations
% eps_change;

% DRAW=1 - show graphically positions of means and standard deviations of components during
% iterartions
% DRAW=0 - omit show option;

% SIG_MIN - minimum value of sigma
% squared


SIG_SQ=SIG_MIN*SIG_MIN;

% data length
N=length(y);

% correct sig_in if necessary
sig_ini=max(sig_ini,SIG_MIN);

% main buffer for intermediate computational results
% pssmac=zeros(KS,N);
% initial values for weights, means and standard deviations of Gaussian
% components
ppoc=pp_ini;
mivoc=mu_ini;
sigkwvoc=sig_ini.^2;

change=1.0;   

% MAIN LOOP
while change > eps_change; 
    
    ixu=find(ppoc>1.0e-3);
    ppoc=ppoc(ixu);
    mivoc=mivoc(ixu);
    sigkwvoc=sigkwvoc(ixu);
    %ixu=find(sigkwvoc>0.01);
    %ppoc=ppoc(ixu);
    %mivoc=mivoc(ixu);
    %sigkwvoc=sigkwvoc(ixu);
    KS=length(ixu);
    
    pssmac=zeros(KS,N);   
    
    oldppoc=ppoc;
    oldsigkwvoc=sigkwvoc;
    
    
    ppoc=max(ppoc,1.0e-6);

    for kskla=1:KS
       pssmac(kskla,:)=normpdf(x,mivoc(kskla),sqrt(sigkwvoc(kskla)));
    end

    denpss=ppoc*pssmac;
    denpss = max(min(denpss(denpss>0)),denpss);
    for kk=1:KS
       macwwwpom=((ppoc(kk)*pssmac(kk,:)).*y')./denpss;
       denom=sum(macwwwpom);
       minum=sum(macwwwpom*x);
       mivacoc=minum/denom;
       mivoc(kk)=mivacoc;
       pomvec=(x-mivacoc*(ones(N,1))).*(x-mivacoc*(ones(N,1)));
       sigkwnum = sum(macwwwpom*pomvec);
       sigkwvoc(kk) = max([sigkwnum/denom SIG_SQ]);
       ppoc(kk)=sum(macwwwpom)/sum(y);
    end
    
   
    change=sum(abs(ppoc-oldppoc))+ sum(((abs(sigkwvoc-oldsigkwvoc))./sigkwvoc))/(length(ppoc));

    if DRAW==1
      plot(mivoc,sqrt(sigkwvoc),'*')
      xlabel('means')
      ylabel('standard deviations')
      title(['Progress of the EM algorithm: change=' num2str(change)]);
      drawnow 
    end
    
end

% RETURN RESULTS
l_lik=sum(log(denpss).*y');
[mu_est,isort]=sort(mivoc);
sig_est=sqrt(sigkwvoc(isort));
pp_est=ppoc(isort);
TIC=sum(y);
bic=l_lik-((3*KS-1)/2)*log(TIC); 

