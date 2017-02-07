% auxiliary function
% plot spestrum versus decomposition
% spectral signal is drawn in black
% Gaussian decomposition model is drawn in red
% Individual components are drawn in green

function y_est=plot_spect_vs_decomp(xx,y_a,ppoc,mivoc,sigvoc,draw)

ploty=0*xx;
KS=length(ppoc);
for kks=1:KS
   ploty=ploty+ppoc(kks)*normpdf(xx,mivoc(kks),sigvoc(kks));
end

scale=sum(y_a)/sum(ploty);
if draw
    plot(xx,y_a,'b',xx,scale*ploty,'r','MarkerSize',10,'LineWidth',2);
    hold on
    for kks=1:KS
       plot(xx,scale*ppoc(kks)*normpdf(xx,mivoc(kks),sigvoc(kks)),'k','MarkerSize',10,'LineWidth',2);
    end
    % 'norm'   
    norm(y_a/scale-ploty);
    grid on; 
    xlabel('M/Z'); ylabel('Intensity')
    legend({'Spectrum','GMM model','Components'})
end

y_est=scale*ploty;