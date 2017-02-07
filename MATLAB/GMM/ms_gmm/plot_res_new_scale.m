%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%5
%
%
%
%    plot MS signal versus GMM model (used for for segments)
%
%
function ok=plot_res_new_scale(data,ygreki,wwoc,mivoc,sigvoc)

xx=data;
y_a=ygreki;

ploty=0*xx;

KS=length(wwoc);

for kks=1:KS
      ploty=ploty+wwoc(kks)*normpdf(xx,mivoc(kks),sigvoc(kks));
end

plot(xx,y_a,'k',xx,ploty,'r');

hold on
for kks=1:KS
      plot(xx,wwoc(kks)*normpdf(xx,mivoc(kks),sigvoc(kks)),'g');
end

grid on; 

xlabel('m/z'); 

ok=1;