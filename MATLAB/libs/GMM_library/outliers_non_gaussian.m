function [B,indx]=outliers_non_gaussian(A,lambda)
% 1 - lagodnie odstajace
% >1 - ekstremalnie odstajace
% detekcja wielkosci odstajacych dla rozk³adów niesymetrycznych,
% odbiegajacych od rozk³adu normalnego

% M.Huberta and S van der Veeken, Journal of Chemometrics, 2008, 22:235-246

%medn=median(A);
%madn=1.483*median(abs(A-medn));
%SDO=abs(A-medn)/madn; % Stahel and Donoho outlyingness

medn=median(A); nA=length(A);
n=(length(A)+1)/2; h=[];
jk1=find([1:nA]<=n); X1=A(jk1); nx1=length(X1);
jk1=find([1:nA]>=n); X2=A(jk1); nx2=length(X2);
for ii=1:nx1
    for jj=1:nx2
        if (X1(ii)~=X2(jj))
            h=[h;((X2(jj)-medn)-(medn-X1(ii)))/(X2(jj)-X1(ii))];
        elseif ii==jj,
            h=[h;0];
        else
            h=[h;-1];
        end
    end
end

MC=median(h) % medcouple
Q1=prctile(A,25);
Q3=prctile(A,75);
if MC>0
    low_lim=Q1-lambda*exp(-4*MC)*(Q3-Q1)
    up_lim=Q3+lambda*exp(3*MC)*(Q3-Q1)
else
   low_lim=Q1-lambda*exp(-3*MC)*(Q3-Q1)
    up_lim=Q3+lambda*exp(4*MC)*(Q3-Q1)
end

jk1=find(A>up_lim);
jk2=find(A<low_lim);
indx=[jk1,jk2];
B=A; B(indx)=[];


% w1=min(A>low_lim);
% w2=max(A<up_lim);
% 
% AO=[];
% for ii=1:n,
%     if A(ii)>medn,
%         AO(ii)=(A(ii)-medn)/(w2-medn);
%     else
%         AO(ii)=(medn-A(ii))/(medn-w1);
%     end
% end
% 
% jj=find((AO<-4)==1);
% ii=find((AO>+4)==1);
% B=A;
% B(jj)=NaN;
% B(ii)=NaN;
% indx=[jj;ii];
% 
