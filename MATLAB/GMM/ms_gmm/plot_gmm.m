% plot MS signal and its GMM model
function ok=plot_gmm(mz,y,ww_gmm,mu_gmm,sig_gmm)

ploty=0*mz;
plot(mz,y,'k');
hold on

KS=length(ww_gmm);
for kks=1:KS
    ixmz=find(abs((mz-mu_gmm(kks))/sig_gmm(kks))<4);
    ploty(ixmz)=ploty(ixmz)+ww_gmm(kks)*normpdf(mz(ixmz),mu_gmm(kks),sig_gmm(kks));
    plot(mz(ixmz),ww_gmm(kks)*normpdf(mz(ixmz),mu_gmm(kks),sig_gmm(kks)),'g');
end


% plot(mz,ploty,'r');

% grid on; 

xlabel('M/Z'); 
ylabel('Intensity');

ok=1;

