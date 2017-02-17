function mdl = components_merging(mdl,thr_mu,thr_sig,thr_pp)
%Function for merging components in GMM using method of moments.

pp_est = mdl.w;
mu_est = mdl.mu;
sig_est = mdl.sig;

i=1;
while i<length(mu_est)
    match = (abs(mu_est(i+1) - mu_est(i)) < 0.01*thr_mu*mu_est(i)) && (max(sig_est(i+1)^2,sig_est(i)^2)/min(sig_est(i+1)^2,sig_est(i)^2) < thr_sig);
    s = i;
    while match == 1
        i = i+1;
        if i<length(mu_est)
            match = (abs(mu_est(i+1) - mu_est(s)) < 0.01*thr_mu*mu_est(s)) && (max(sig_est(i+1)^2,sig_est(s)^2)/min(sig_est(i+1)^2,sig_est(s)^2) < thr_sig);
        else
            break
        end
    end
    e = i;
    len = e-s;
    if len > 0
        %method of moments from Richardson and Green weighted with w
        pp_temp = sum(pp_est(s:e));
        mu_temp = sum(pp_est(s:e).*mu_est(s:e))/pp_temp;
        sig_est(s) = sqrt(sum(pp_est(s:e).*(mu_est(s:e).^2 + sig_est(s:e).^2))/pp_temp - mu_temp^2);
        mu_est(s) = mu_temp; mu_est(s+1:e) = NaN; pp_est(s) = pp_temp;
    end
    i = e + 1;    
end

del_mu = isnan(mu_est);
pp_est(del_mu) = [];
sig_est(del_mu) = [];
mu_est(del_mu) = [];

del_pp = pp_est <= thr_pp;
mdl.w = pp_est(~del_pp);
mdl.mu = mu_est(~del_pp);
mdl.sig = sig_est(~del_pp);
mdl.KS = length(pp_est);