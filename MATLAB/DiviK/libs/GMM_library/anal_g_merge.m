function [pp_new,mu_new,sig_new]=anal_g_merge(pp_est,mu_est,sig_est,tol_s,tol_d)
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% anal_g_merge
% auxiliary function 
% merging similar components

pp_new=pp_est;
mu_new=mu_est;
sig_new=sig_est;

kk=0;
while 1
    kk=kk+1;
    if kk+1 > length(pp_new)
        break
    end
%     if kk==11; 
%         disp('stop');end
    cmp=com_mer_v(pp_new(kk:kk+1),mu_new(kk:kk+1),sig_new(kk:kk+1),tol_s,tol_d);
    if cmp==1
        [pp_e,mu_e,sig_e]=mer_v(pp_new(kk:kk+1),mu_new(kk:kk+1),sig_new(kk:kk+1));
        pp_new=[pp_new(1:kk-1) pp_e pp_new(kk+2:length(pp_new))];
        mu_new=[mu_new(1:kk-1) mu_e mu_new(kk+2:length(mu_new))];
        sig_new=[sig_new(1:kk-1) sig_e sig_new(kk+2:length(sig_new))];
    end
end

kk=0;
while 1
    kk=kk+1;
    if kk+2 > length(pp_new)
        break
    end
    cmp=com_mer_v(pp_new(kk:kk+2),mu_new(kk:kk+2),sig_new(kk:kk+2),tol_s,tol_d);
    if cmp==1
        [pp_e,mu_e,sig_e]=mer_v(pp_new(kk:kk+2),mu_new(kk:kk+2),sig_new(kk:kk+2));
        pp_new=[pp_new(1:kk-1) pp_e pp_new(kk+3:length(pp_new))];
        mu_new=[mu_new(1:kk-1) mu_e mu_new(kk+3:length(mu_new))];
        sig_new=[sig_new(1:kk-1) sig_e sig_new(kk+3:length(sig_new))];
    end
end

kk=0;
while 1
    kk=kk+1;
    if kk+3 > length(pp_new)
        break
    end
    cmp=com_mer_v(pp_new(kk:kk+3),mu_new(kk:kk+3),sig_new(kk:kk+3),tol_s,tol_d);
    if cmp==1
        [pp_e,mu_e,sig_e]=mer_v(pp_new(kk:kk+3),mu_new(kk:kk+3),sig_new(kk:kk+3));
        pp_new=[pp_new(1:kk-1) pp_e pp_new(kk+4:length(pp_new))];
        mu_new=[mu_new(1:kk-1) mu_e mu_new(kk+4:length(mu_new))];
        sig_new=[sig_new(1:kk-1) sig_e sig_new(kk+4:length(sig_new))];
    end
end



%
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% com_mer_v
% auxiliary function
% compare - merge
function m_y_n=com_mer_v(pp_v,mu_v,sig_v,tol_s,tol_d)
pp_e=pp_v/sum(pp_v);
mu_e=sum(pp_e.*mu_v);
sig_e=sqrt(sum(pp_e.*(mu_v.^2+sig_v.^2))-mu_e^2);
if sig_e == 0; 
    m_y_n = 0; 
else
    dlt_x=sig_e/100;
    zakr_x=mu_e-3*sig_e:dlt_x:mu_e+3*sig_e;
    praw_x=0*zakr_x;
    for kk=1:length(pp_v)
         praw_x=praw_x+pp_e(kk)*normpdf(zakr_x,mu_v(kk),sig_v(kk));
    end
    apr_x=normpdf(zakr_x,mu_e,sig_e);

    praw_dx=0*zakr_x;
    for kk=1:length(pp_v)
        praw_dx=praw_dx+(pp_e(kk)*(zakr_x-mu_v(kk)*ones(size(zakr_x))).*normpdf(zakr_x,mu_v(kk),sig_v(kk))/(sig_v(kk)^2));
    end
    apr_dx=(zakr_x-mu_e*ones(size(zakr_x))).*normpdf(zakr_x,mu_e,sig_e)/(sig_e^2);

    if max(abs(apr_x-praw_x))/max(apr_x)<tol_s && max(abs(apr_dx-praw_dx))/max(apr_dx)<tol_d
        m_y_n=1;
    else
        m_y_n=0;
    end
end
%
%
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% mer_v
% auxiliary function
% merge
function [pp_e,mu_e,sig_e]=mer_v(pp_v,mu_v,sig_v)
pp_s=pp_v/sum(pp_v);
mu_e=sum(pp_s.*mu_v);
sig_e=sqrt(sum(pp_s.*(mu_v.^2+sig_v.^2))-mu_e^2);
pp_e=sum(pp_v);
