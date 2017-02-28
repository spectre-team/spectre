%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
%                           ms_gmm
%
%
% decomposition of the proteomic spectrum into Gaussian mixture model
%
%
function [ww_gmm,mu_gmm,sig_gmm]=ms_gmm1(mz,y_bas)

%
% INITIALIZATION
%
mz=mz(:);
y_bas=y_bas(:);

% find peaks
[P, PFWHH]=mspeaks(mz,y_bas,'SHOWPLOT',0);

% read paramter for gmm decomp resolution
Res_Par=ms_gmm_params(2);

% estimate resolution
Par_mcv=Res_Par*mean((PFWHH(:,2)-PFWHH(:,1))./P(:,1));

%plot(P(:,1),(PFWHH(:,2)-PFWHH(:,1)),'.')
%pause

% find good quality splitting peaks
%
[Splitt_v, seg_vec_c]=find_split_peaks(P,mz,y_bas,Par_mcv);
KSP=length(Splitt_v); % number of plitting peaks
% figure; hold on; plot(mz,y_bas);for a=1:KSP;plot([mz(seg_vec_c(Splitt_v(a))),mz(seg_vec_c(Splitt_v(a)))],ylim,'r');end

%                          PHASE 1 - SPLITTERS AND GMM DECOMPOSITIONS OF SPLITTING SEGMENTS
%
% buffers for splitter parameters
Buf_size_split_Par=ms_gmm_params(14);
mu_mx_1=zeros(KSP,Buf_size_split_Par);
ww_mx_1=zeros(KSP,Buf_size_split_Par);
sig_mx_1=zeros(KSP,Buf_size_split_Par);

parfor kk=1:KSP   
%     disp(['Split Progress: ' num2str(kk) ' of ' num2str(KSP)]);
    % Gaussian mixture decomposition of the splitting segment
    [ww_pick,mu_pick,sig_pick]=gmm_decomp_split_segment(mz,y_bas,Splitt_v,seg_vec_c,P,Par_mcv,kk); 
     mu_mx_1(kk,:)=mu_pick;
     ww_mx_1(kk,:)=ww_pick;
     sig_mx_1(kk,:)=sig_pick;  
end

%
%
%              PHASE 2 - GMM DECOMPOSITIONS OF SEGMENTS
%
% buffers for segments decompositions paramteres 
Buf_size_seg_Par=ms_gmm_params(15);
KSP1=KSP+1;
mu_mx_2=zeros(KSP1,Buf_size_seg_Par);
ww_mx_2=zeros(KSP1,Buf_size_seg_Par);
sig_mx_2=zeros(KSP1,Buf_size_seg_Par);
parfor ksp=1:KSP1    
%    disp(['Seg Progress: ' num2str(ksp) ' of ' num2str(KSP1)]);
   [ww_out,mu_out,sig_out]=gmm_decomp_segment1(mz,y_bas,ww_mx_1,mu_mx_1,sig_mx_1,P,Splitt_v,Par_mcv,ksp-1);
   mu_mx_2(ksp,:)=mu_out;
   ww_mx_2(ksp,:)=ww_out;
   sig_mx_2(ksp,:)=sig_out;
end

% AGGREGATION
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%% aggregate components computed in PHASE 1 and in PHASE 2

NUMPICK=size(P,1);
mu_vec=zeros(7*NUMPICK,1);
ww_vec=zeros(7*NUMPICK,1);
sig_vec=zeros(7*NUMPICK,1);

kacum=0;

for ksp=1:KSP+1
   mu_a=mu_mx_2(ksp,:);
   ww_a=ww_mx_2(ksp,:);
   sig_a=sig_mx_2(ksp,:);
   ixp=max(find(ww_a > 0));
   if length(ixp)==0
       % emergency correction in the case of detected ovelap between splitters 
       [ww_a_l_new,mu_a_l_new,sig_a_l_new,ww_a_p_new,mu_a_p_new,sig_a_p_new]=emcor(ksp-1,mu_mx_1,ww_mx_1,sig_mx_1,mz,y_bas,Splitt_v,P,Par_mcv);
       if ksp>1
          ww_mx_1(ksp-1,:)=ww_a_l_new;
          mu_mx_1(ksp-1,:)=mu_a_l_new;
          sig_mx_1(ksp-1,:)=sig_a_l_new;
       end
       if ksp<KSP+1
          ww_mx_1(ksp,:)=ww_a_p_new;
          mu_mx_1(ksp,:)=mu_a_p_new;
          sig_mx_1(ksp,:)=sig_a_p_new;
       end
   else
      mu_a=mu_a(1:ixp);
      ww_a=ww_a(1:ixp);
      sig_a=sig_a(1:ixp);
   %
      for kk=1:length(ww_a)
          kacum=kacum+1;
          mu_vec(kacum)=mu_a(kk);
          ww_vec(kacum)=ww_a(kk);
          sig_vec(kacum)=sig_a(kk);
      end
   end  
end

for ksp=1:KSP
   mu_a=mu_mx_1(ksp,:);
   ww_a=ww_mx_1(ksp,:);
   sig_a=sig_mx_1(ksp,:);
   ixp=max(find(ww_a > 0));
   mu_a=mu_a(1:ixp);
   ww_a=ww_a(1:ixp);
   sig_a=sig_a(1:ixp);
   % 
   for kk=1:length(ww_a)
       kacum=kacum+1;
       mu_vec(kacum)=mu_a(kk);
       ww_vec(kacum)=ww_a(kk);
       sig_vec(kacum)=sig_a(kk);
   end
end


mu_vec=mu_vec(1:kacum);
ww_vec=ww_vec(1:kacum);
sig_vec=sig_vec(1:kacum);

[mu_sort,muixs]=sort(mu_vec);
mu_gmm=mu_sort;
ww_gmm=ww_vec(muixs);
sig_gmm=sig_vec(muixs);



