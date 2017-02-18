%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
%       gmm decomposition of a segment based on
%       dynamic programming initialization
%
%
function [ww_dec,mu_dec,sig_dec]=gmm_decomp_segment(mz,y_bas,ww_mx_1,mu_mx_1,sig_mx_1,P,Splitt_v,Par_mcv,Fr_No)

% parameters
DRAW=ms_gmm_params(1);
QFPAR=ms_gmm_params(8);
Res_par_2=ms_gmm_params(9);
Prec_Par_2=ms_gmm_params(12);
Penet_Par_2=ms_gmm_params(13);
Buf_size_seg_Par=ms_gmm_params(15);
em_tol=ms_gmm_params(16);
   
   
 % buffers
ww_dec=zeros(1,Buf_size_seg_Par);
mu_dec=zeros(1,Buf_size_seg_Par);
sig_dec=zeros(1,Buf_size_seg_Par);

% assign fragment number to ksp
KSP=length(Splitt_v);
ksp=Fr_No;

% find separated fragments
if ksp>0
    mu_l=mu_mx_1(ksp,:);
    ww_l=ww_mx_1(ksp,:);
    sig_l=sig_mx_1(ksp,:);
    ktr=max((find(ww_l>0)));
    mu_l=mu_l(1:ktr);
    ww_l=ww_l(1:ktr);
    sig_l=sig_l(1:ktr);
 else
    mu_l=[];
    ww_l=[];
    sig_l=[];
 end
    
 if ksp<KSP
    mu_p=mu_mx_1(ksp+1,:);
    ww_p=ww_mx_1(ksp+1,:);
    sig_p=sig_mx_1(ksp+1,:);
    ktr=max((find(ww_p>0)));
    mu_p=mu_p(1:ktr);
    ww_p=ww_p(1:ktr);
    sig_p=sig_p(1:ktr);
 else
    mu_p=[];
    ww_p=[];
    sig_p=[];
 end
         
 [mz_out,y_out]=find_segment(ksp,P,Splitt_v,mz,y_bas,mu_l,ww_l,sig_l,mu_p,ww_p,sig_p);  
      
  mz_out=mz_out(:);
  y_out=y_out(:);
  if length(mz_out) > 300
     dmz=(mz_out(length(mz_out))-mz_out(1))/200;
     mz_out_bb=(mz_out(1):dmz:mz_out(length(mz_out)));
     mz_out_b=mz_out_bb(1:200)+0.5*dmz;
     [y_out_b,yb] = bindata(y_out,mz_out,mz_out_bb);
     ixnn=find(~isnan(y_out_b));
     y_out_b=y_out_b(ixnn);
     mz_out_b=mz_out_b(ixnn);
     y_out_b=y_out_b(:);
     mz_out_b=mz_out_b(:);
  else
     y_out_b=y_out;
     mz_out_b=mz_out;
  end

% find appropriate gmm model for the segment
   
 quamin=inf;
 N=length(mz_out);
 Nb=length(mz_out_b);
 PAR_sig_min=Res_par_2*Par_mcv*mean(mz_out);  
  
if length(mz_out)<3
    return
else
    KSmin=min([1 (floor((mz_out(N)-mz_out(1))/PAR_sig_min)-1)]);
    if KSmin<=0
        wwec=y_out/(sum(y_out));
        mu_est=sum(mz_out.*wwec);
        pp_est=1;
        sig_est=sqrt(sum(((mz_out-sum(mz_out.*wwec)).^2).*wwec));
        [qua,scale]=qua_scal(mz_out,y_out,pp_est,mu_est,sig_est);
    else   
         KS=KSmin;
         % penetration - how far are we searching for minimum
         PAR_penet=min([Penet_Par_2 floor((mz_out(N)-mz_out(1))/PAR_sig_min) floor(length(mz_out)/4)]);
         kpen=0;
      
         %name=['dane_nr' num2str(ksp)];
         %save(name, 'mz_out', 'y_out', 'mz_out_b', 'y_out_b', 'QFPAR', 'PAR_sig_min', 'PAR_penet');   
         aux_mx=dyn_pr_split_w_aux(mz_out_b,y_out_b,QFPAR,PAR_sig_min);    
         while KS < Buf_size_seg_Par 
             KS=KS+1;
             kpen=kpen+1;
             
             if KS > KSmin+1 && KS >= length(mz_out)/2
                 break
             end
         
             [Q,opt_part]=dyn_pr_split_w(mz_out_b,y_out_b,KS-1,aux_mx,QFPAR,PAR_sig_min);
             part_cl=[1 opt_part Nb+1]; 
       
             % set initial cond
             pp_ini=zeros(1,KS);
             mu_ini=zeros(1,KS);
             sig_ini=zeros(1,KS);
             for kkps=1:KS
                 invec=mz_out_b(part_cl(kkps):part_cl(kkps+1)-1);
                 yinwec=y_out_b(part_cl(kkps):part_cl(kkps+1)-1);
                 wwec=yinwec/(sum(yinwec));
                 pp_ini(kkps)=sum(yinwec)/sum(y_out);
                 mu_ini(kkps)=sum(invec.*wwec);
                 %sig_ini(kkps)=sqrt(sum(((invec-sum(invec.*wwec')).^2).*wwec'));
                 sig_ini(kkps)=0.5*(max(invec)-min(invec));
             end
          %
             [pp_est,mu_est,sig_est,TIC,l_lik,bic]=my_EM_iter(mz_out,y_out,pp_ini,mu_ini,sig_ini,0,PAR_sig_min,em_tol);
       
              
             % compute quality indices of gmm model of the fragment   
             [qua,scale]=qua_scal(mz_out,y_out,pp_est,mu_est,sig_est);
             quatest=qua+Prec_Par_2*KS;
            
            if (quatest < quamin)
                 quamin=quatest;
                 pp_min=pp_est;
                 mu_min=mu_est;
                 sig_min=sig_est;
                 scale_min=scale;
            end
       
            if (quatest > quamin) && (kpen>PAR_penet)
                 pp_est=pp_min;
                 mu_est=mu_min;
                 sig_est=sig_min;
                 scale=scale_min;
                 break
            end
       end
    end
    
     if DRAW==1
          figure(2)
          subplot(3,1,3)
          hold off
          ok=plot_res_new_scale(mz_out,y_out,pp_est*scale,mu_est,sig_est);
          xlabel(['Segment: '  num2str(Fr_No)  '     KS= ' num2str(length(pp_est))])
          drawnow
     end
   
     ww_o=pp_est*scale;
     mu_o=mu_est;
     sig_o=sig_est;
    
     for kkpick=1:length(ww_o)
         mu_dec(kkpick)=mu_o(kkpick);
         ww_dec(kkpick)=ww_o(kkpick);
         sig_dec(kkpick)=sig_o(kkpick);
     end  
    
end   

