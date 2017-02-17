% compute quality indices and scale of gmm model of the segment
function [qua,scale]=qua_scal(data,ygreki,ppoc,mivoc,sigvoc)

xx=data;
y_a=ygreki;
ploty=0*xx;

KS=length(ppoc);

for kks=1:KS
      ploty=ploty+ppoc(kks)*normpdf(xx,mivoc(kks),sigvoc(kks));
end

scale=sum(y_a)/sum(ploty);
qua=sum(abs(y_a/scale-ploty))/sum(y_a/scale);