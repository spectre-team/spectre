%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
%
%         auxiliary function for dynamic programming - compute quality index matrix
%
function aux_mx=dyn_pr_split_w_aux(data,ygreki,PAR,PAR_sig_min)

N=length(data);

% aux_mx
aux_mx=zeros(N,N);
for kk=1:N-1
   for jj=kk+1:N
       aux_mx(kk,jj)= my_qu_ix_w(data(kk:jj-1),ygreki(kk:jj-1),PAR,PAR_sig_min);
   end
end