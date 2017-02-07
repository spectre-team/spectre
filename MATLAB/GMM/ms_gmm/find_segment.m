%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
%    find segment between two neighboring splitters
%
function [mz_out,y_out]=find_segment(ksp,P,Splitt_v,mz,y_bas,mu_l,ww_l,sig_l,mu_p,ww_p,sig_p)

DRAW=ms_gmm_params(1);
KSP=length(Splitt_v);

if ksp>0
    mzl=P(Splitt_v(ksp),1);
else
    mzl=mz(1);
end
if ksp<KSP
    mzp=P(Splitt_v(ksp+1),1);
else
    mzp=mz(length(mz));
end

idm=find(mz>=mzl & mz<=mzp);
mz_o=mz(idm);
y_o=y_bas(idm);

if DRAW==1
   mzll=max([(mzl-round((mzp-mzl)/5)) mz(1)]);
   mzpp=min([(mzp+round((mzp-mzl)/5)) mz(length(mz))]);
   ixmzp=find((mz>mzll) & (mz<mzpp));
   mz_o_p=mz(ixmzp);
   y_o_p=y_bas(ixmzp);
   figure(2)
   subplot(3,1,1)
   hold off
   plot(mz_o_p,y_o_p,'k')
   hold on
   title('Splitters')
   grid on
end

pyl=0*mz_o;
if ksp>0
   KS=length(ww_l);
   for kks=1:KS
         pyl=pyl+ww_l(kks)*normpdf(mz_o,mu_l(kks),sig_l(kks));
   end
   if DRAW==1
         ok=fill_red(ww_l,mu_l,sig_l);
   end
end

pyp=0*mz_o;
if ksp<KSP
   KS=length(ww_p);
   for kks=1:KS
         pyp=pyp+ww_p(kks)*normpdf(mz_o,mu_p(kks),sig_p(kks));
   end
   if DRAW==1
       ok=fill_red(ww_p,mu_p,sig_p);
   end
end

y_out=y_o-pyl-pyp;

if ksp>0
   iyl=find(pyl > 0.05*max(pyl));
   if length(iyl)>1
      mz_ol=mz_o(iyl);
      y_ol=y_out(iyl);
      [mp,imp]=min(y_ol);
       mz_l=mz_ol(imp(1));
   else
       mz_l=mz_o(1);
   end
else
   mz_l=mzl;
end

if ksp<KSP
   iyp=find(pyp > 0.05*max(pyp));
   if length(iyp)>1
      mz_op=mz_o(iyp);
      y_op=y_out(iyp);
      [mp,imp]=min(y_op);
      mz_p=mz_op(imp(1));
   else
      mz_p=mz_o(length(mz_o));
   end
else
   mz_p=mzp;
end

imz=find((mz_o <= mz_p) & (mz_o >= mz_l));
y_out=y_out(imz);
mz_out=mz_o(imz);

iy=find(y_out > 0 );
y_out=y_out(iy);
mz_out=mz_out(iy);

if DRAW==1
   subplot(3,1,2)
   hold off
   plot(mz_o,0*y_o,'r')
   hold on
   plot(mz_out,y_out,'k')
   title(['Segment:' num2str(ksp)])
   grid on
end

