%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
%         find splitting peaks 
%
function [Splitt_v, seg_vec_c]=find_split_peaks(P,mz,y_bas,Par_mcv)

% read parameter for peak detection sensitivity
Par_P_Sens=ms_gmm_params(3);
% read parameter for peak quality threshold
Par_Q_Thr=ms_gmm_params(4);
% read parameter for initial jump
Par_Ini_J=ms_gmm_params(5);
% read parameter for range for peak lookup
Par_P_L_R=ms_gmm_params(6);
% read parameter for jump
Par_P_J=ms_gmm_params(7);
%
seg_vec=zeros(size(P,1),1);
for kkp=1:size(P,1)-1
   zakm=find(mz<=P(kkp+1,1) & mz>=P(kkp,1));
   warty=y_bas(zakm);
   [miny,idxm]=min(warty);
   prawzak=zakm(idxm(1));
   seg_vec(kkp)=prawzak;
end
seg_vec(length(seg_vec))=length(mz);
seg_vec_c=[1; seg_vec];
M_B_H=zeros(size(P,1),1);
for kk=1:size(P,1)
  M_B_H(kk)=max([y_bas(seg_vec_c(kk)) y_bas(seg_vec_c(kk+1))])+1;
end
MaxP=max(P(:,2));
PPe=P(:,2)./M_B_H;
Kini=f_par_mcv(1,Par_Ini_J,P,Par_mcv);
Splitt_v=zeros(floor((mz(length(mz))-mz(1))/(mz(1)*Par_mcv)),1);
kspl=0;
%
while 1   
    if f_par_mcv( Kini,Par_P_L_R,P,Par_mcv )==0
        break
    end
    Top=Kini+f_par_mcv( Kini,Par_P_L_R,P,Par_mcv );
    Zak=Kini:Top;
    % verify quality condition for the best peak in the range Zak=Kini:Top
    pzak2=P(Zak,2);
    ppezak=PPe(Zak);
    ixq=find((pzak2 > Par_P_Sens*MaxP) & (ppezak > Par_Q_Thr));
    if length(ixq)>=1
       [mpk,impk]=max(PPe(Zak(ixq)));
       kkt=ixq(impk(1));
       kspl=kspl+1;
       Splitt_v(kspl)=Zak(kkt);
       Kini=Top+f_par_mcv( Top,Par_P_J,P,Par_mcv );
    else
       Kini=Top+f_par_mcv( Top,1,P,Par_mcv );
    end
end
Splitt_v=Splitt_v(1:kspl);

