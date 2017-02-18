% emergency correction function
% launched in the case of (rare) overlap between splitters
function [ww_a_l_new,mu_a_l_new,sig_a_l_new,ww_a_p_new,mu_a_p_new,sig_a_p_new]=emcor(ksp,mu_mx_1,ww_mx_1,sig_mx_1,mz,y_bas,Splitt_v,P,Par_mcv)

Res_par_2=ms_gmm_params(9);
em_tol=ms_gmm_params(16);

KSP=length(Splitt_v);

ww_a_l_new=0*ww_mx_1(1,:);
mu_a_l_new=0*ww_mx_1(1,:);
sig_a_l_new=0*ww_mx_1(1,:);

ww_a_p_new=0*ww_mx_1(1,:);
mu_a_p_new=0*ww_mx_1(1,:);
sig_a_p_new=0*ww_mx_1(1,:);


if ksp>0
   mu_l=mu_mx_1(ksp,:);
   ww_l=ww_mx_1(ksp,:);
   sig_l=sig_mx_1(ksp,:);
   ixp=max(find(ww_l > 0));
   
   if isempty(ixp)
       KSll=0;
       mzll=mz(1);
       mu_l=[];
       ww_l=[];
       sig_l=[];
   else
       mu_l=mu_l(1:ixp);
       ww_l=ww_l(1:ixp);
       sig_l=sig_l(1:ixp);
       KSll=length(ww_l);
       [mzll,mzlp]=find_ranges(mu_l,sig_l);
   end
else
   KSll=0;
   mzll=mz(1);
   mu_l=[];
   ww_l=[];
   sig_l=[];
end

if ksp<KSP
   mu_p=mu_mx_1(ksp+1,:);
   ww_p=ww_mx_1(ksp+1,:);
   sig_p=sig_mx_1(ksp+1,:);
   ixp=max(find(ww_p > 0));
   mu_p=mu_p(1:ixp);
   ww_p=ww_p(1:ixp);
   sig_p=sig_p(1:ixp);
   KSpp=length(ww_p);
   [mzlp,mzpp]=find_ranges(mu_p,sig_p);
else
   KSpp=0;
   mzpp=mz(end);
   mu_p=[];
   ww_p=[];
   sig_p=[];
end


idm=find(mz>=mzll & mz<=mzpp);
mz_o=mz(idm);
y_o=y_bas(idm);
pyl=0*mz_o;
pyp=0*mz_o;

if ksp>0
   for kks=1:KSll
         pyl=pyl+ww_l(kks)*normpdf(mz_o,mu_l(kks),sig_l(kks));
   end
   idl=find(mz_o>=mzll & mz_o<=P(Splitt_v(ksp),1));
   pylpds=pyl(idl);
   y_o(idl)=pylpds;
end

if ksp<KSP
   for kks=1:KSpp
         pyp=pyp+ww_p(kks)*normpdf(mz_o,mu_p(kks),sig_p(kks));
   end
   idp=find(mz_o>=P(Splitt_v(ksp+1),1) & mz_o<=mzpp);
   pyppds=pyp(idp);
   y_o(idp)=pyppds;
end

KS=KSll+KSpp;

pp_ini=[ww_l ww_p]/sum([ww_l ww_p]);
mu_ini=[mu_l mu_p];
sig_ini=[sig_l sig_p];

PAR_sig_min=Res_par_2*Par_mcv*mean(mz_o);  
[pp_est,mu_est,sig_est]=my_EM_iter(mz_o,y_o,pp_ini,mu_ini,sig_ini,0,PAR_sig_min,em_tol);
KS = length(pp_est);
KSpp = KS - KSll;

[~,scale]=qua_scal(mz_o,y_o,pp_est,mu_est,sig_est);
ww_est=pp_est*scale;

if ksp>0
    ww_a_l_new(1:KSll)=ww_est(1:KSll);
    mu_a_l_new(1:KSll)=mu_est(1:KSll);
    sig_a_l_new(1:KSll)=sig_est(1:KSll);
end
if ksp<KSP
   ww_a_p_new(1:KSpp)=ww_est(KSll+1:KS);
   mu_a_p_new(1:KSpp)=mu_est(KSll+1:KS);
   sig_a_p_new(1:KSpp)=sig_est(KSll+1:KS);
end

%figure(3)
%ok=plot_res_new_scale(mz_o,y_o,ww_est,mu_est,sig_est);

