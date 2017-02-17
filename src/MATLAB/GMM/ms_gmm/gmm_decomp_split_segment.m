%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
% gmm decomposition of splitter segment based on 
% dynamic programming initialization for splitting segments
%
function [ww_pick,mu_pick,sig_pick]=gmm_decomp_split_segment(mz,y_bas,Splitt_v,seg_vec_c,P,Par_mcv,Sp_No)
% input
% mz,y_bas - spectrum
% Splitt_v, seg_vec_c - list of splitting peaks and segment bounds computed by find_split_peaks
% P,Par_mcv - peaks, 
% Sp_no - number of splitting segment
  
%  find appropriate gmm model for the splitting segment no Sp_No
   % 
   % parameters
   DRAW=ms_gmm_params(1);
   QFPAR=ms_gmm_params(8);
   Res_par_2=ms_gmm_params(9);
   Prec_Par_1=ms_gmm_params(10);
   Penet_Par_1=ms_gmm_params(11);
   Buf_size_split_Par=ms_gmm_params(14);
   em_tol=ms_gmm_params(16);
   
   % buffers
   ww_pick=zeros(1,Buf_size_split_Par);
   mu_pick=zeros(1,Buf_size_split_Par);
   sig_pick=zeros(1,Buf_size_split_Par);
   
   % un-binned data
   [mz_out,y_out]=find_split_segment(Splitt_v(Sp_No),mz,y_bas,seg_vec_c,P,Par_mcv);
   mz_out=mz_out(:);
   y_out=y_out(:);
   
    % bin if necessary
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
    
   N=length(mz_out);
   Nb=length(mz_out_b);
   quamin=inf;
   PAR_sig_min=Res_par_2*Par_mcv*mean(mz_out);  
   
   KSmin=min([2 (floor((mz_out(N)-mz_out(1))/PAR_sig_min)-1)]);
   if KSmin<=0
       wwec=y_out/(sum(y_out));
       mu_est=sum(mz_out.*wwec);
       pp_est=1;
       sig_est=sqrt(sum(((mz_out-sum(mz_out.*wwec)).^2).*wwec));
       [qua,scale]=qua_scal(mz_out,y_out,pp_est,mu_est,sig_est);
   else   
   
      KS=KSmin;
      PAR_penet=min([Penet_Par_1 floor((mz_out(N)-mz_out(1))/PAR_sig_min)]);
      kpen=0;
  
      aux_mx=dyn_pr_split_w_aux(mz_out_b,y_out_b,QFPAR,PAR_sig_min);    
      while KS <= 2*(KSmin+PAR_penet) 
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
              pp_ini(kkps)=sum(yinwec)/sum(y_out_b);
              mu_ini(kkps)=sum(invec.*wwec);
              %sig_ini(kkps)=sqrt(sum(((invec-sum(invec.*wwec')).^2).*wwec'));
              sig_ini(kkps)=0.5*(max(invec)-min(invec));
          end
       

          [pp_est,mu_est,sig_est,TIC,l_lik,bic]=my_EM_iter(mz_out,y_out,pp_ini,mu_ini,sig_ini,0,PAR_sig_min,em_tol);
                  
          % compute quality indices and scale of gmm model of the fragment
          [qua,scale]=qua_scal(mz_out,y_out,pp_est,mu_est,sig_est);
          quatest=qua+Prec_Par_1*KS;           
          
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
      
   end  %(if -- else)
   
   % pick and store results
    dist=abs((mu_est-P(Splitt_v(Sp_No),1))./sig_est);
    ixf=find(dist<=3);
    if isempty(ixf)
        [tmp,ixf] = min(dist);
        ixnf = find(dist>tmp);
    else
        ixnf=find(dist>3);
    end
      
    mu_p=mu_est(ixf);
    ww_p=scale*pp_est(ixf);
    sig_p=sig_est(ixf);
    
    mu_t=mu_est(ixnf);
    ww_t=scale*pp_est(ixnf);
    sig_t=sig_est(ixnf);
    
    inn=find(mu_t<max(mu_p) & mu_t>min(mu_p));
    mu_tp=mu_t(inn);
    ww_tp=ww_t(inn);
    sig_tp=sig_t(inn);
    
    mu_pp=[mu_p mu_tp];
    ww_pp=[ww_p ww_tp];
    sig_pp=[sig_p sig_tp];
    
    for kkpick=1:length(ww_pp)
       mu_pick(kkpick)=mu_pp(kkpick);
       ww_pick(kkpick)=ww_pp(kkpick);
       sig_pick(kkpick)=sig_pp(kkpick);
    end  
   
    %%%%%%%%%%%%%%%%%%%%%%%%%%   
    %%%%%%%%%%%%%%%%%%%% plots
    if DRAW==1
       figure(1)
       subplot(2,1,1)
       hold off
       plot(mz_out,y_out,'k') 
       hold on
       plot([P(Splitt_v(Sp_No),1) P(Splitt_v(Sp_No),1)],[0 max(y_out)],'r')
       ylabel('y (no. of counts)');
       title(['Splitter segment: ' num2str(Sp_No)]);
       grid on
       subplot(2,1,2)
       hold off
       ww_est=scale*pp_est;
       ok=plot_gmm(mz_out,y_out,ww_est,mu_est,sig_est);
       ok=fill_red(ww_pp,mu_pp,sig_pp);
       title(['Splitter: ' num2str(Sp_No)])
       drawnow
    end
       