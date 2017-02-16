% FUNCTION: 
% gaussian_mixture

% PURPOSE: 
% computes Gaussian mixture decomposition 
% the signal to be decomposed is the mass spectrum 

% INPUT:
% measurements are specified by vector x
% KS - number of Gaussian components

% METHOD: 
% EM iteriations with initial conditions for means defined by inverting the
% empirical cumulative distribution, iterations for standard deviations stabilized
% by using prior Wishart distribution

% OUTPUT:
% pp_est - vector of esimated weights
% mu_est - vector of estimated component means
% sig_est - vector of estimated standard deviations of components
% l_lik - log likelihood of the estimated decomposition

% AUTHOR:
% Andrzej Polanski
% email: andrzej.polanski@polsl.pl

function [pp_est,mu_est,sig_est,TIC,l_lik]=gaussian_mixture_simple(x,counts,KS)

TIC=sum(x.*counts);
if KS==1,
	mu_est=nanmean(x);
	sig_est=std(x);
	pp_est=1;
	l_lik=NaN;	
else
     Nb=floor(sqrt(length(x)));
     [y_out,mz_out]=hist(x,Nb);
    
     aux_mx=dyn_pr_split_w_aux(mz_out,y_out);    

     [Q,opt_part]=dyn_pr_split_w(mz_out,y_out,KS-1,aux_mx);
     part_cl=[1 opt_part Nb+1]; 
       
     % set initial cond
     pp_ini=zeros(1,KS);
     mu_ini=zeros(1,KS);
     sig_ini=zeros(1,KS);
     for kkps=1:KS
         invec=mz_out(part_cl(kkps):part_cl(kkps+1)-1);
         yinwec=y_out(part_cl(kkps):part_cl(kkps+1)-1);
         wwec=yinwec/(sum(yinwec));
         pp_ini(kkps)=sum(yinwec)/sum(y_out);
         mu_ini(kkps)=sum(invec.*wwec);
         %sig_ini(kkps)=sqrt(sum(((invec-sum(invec.*wwec')).^2).*wwec'));
         sig_ini(kkps)=0.5*(max(invec)-min(invec));
      end
          %
      [pp_est,mu_est,sig_est,l_lik] = g_mix_est_fast_lik(x,KS,mu_ini,sig_ini,pp_ini);
end
          
      

