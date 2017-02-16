%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
% find splitter segment around the split peak no k
%
%
%
function [mz_ss,y_ss]=find_split_segment(k_pick,mz,y_bas,seg_vec_c,P,Par_mcv)

zakl=seg_vec_c(max([1 (k_pick-4)]));
zakp=seg_vec_c(min([size(P,1) (k_pick+5)]));

mzp=mz(zakp);
mzl=mz(zakl);
mzPP=P(k_pick,1);

if (mzp-mzPP)<5*mzPP*Par_mcv
   zakm=find(mz>=mzp & mz<=mzp+5*mzPP*Par_mcv);
   warty=y_bas(zakm);
   [miny,idxm]=min(warty);
   prawzak=zakm(idxm(1));
else
   prawzak=zakp;
end

if (mzPP-mzl)<5*mzPP*Par_mcv
   zakm=find(mz<=mzl & mz>=mzl-5*mzPP*Par_mcv);
   warty=y_bas(zakm);
   [miny,idxm]=min(warty);
   lewzak=zakm(idxm(1));
else
   lewzak=zakl;
end

mz_o=mz(lewzak:prawzak);
y_bas_o=y_bas(lewzak:prawzak);

yp=y_bas(prawzak);
yl=y_bas(lewzak);

dmzl=mz(lewzak+1)-mz(lewzak);
dmzp=mz(prawzak)-mz(prawzak-1);

mzaugl=mz(lewzak)-6*Par_mcv*mz(lewzak):dmzl:mz(lewzak)-dmzl;
mzaugp=mz(prawzak)+dmzp:dmzp:mz(prawzak)+6*Par_mcv*mz(prawzak);

yop=sqrt(2*pi)*(2*Par_mcv*mz(prawzak))*yp*normpdf(mzaugp-mzp,0,2*Par_mcv*mz(prawzak));
yol=sqrt(2*pi)*(2*Par_mcv*mz(lewzak))*yl*normpdf(mzaugl-mzl,0,2*Par_mcv*mz(lewzak));


mz_ss=[mzaugl'; mz_o; mzaugp'];
y_ss=[yol'; y_bas_o; yop'];
