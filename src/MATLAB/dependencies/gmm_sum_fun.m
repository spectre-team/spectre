function [ f ] = gmm_sum_fun( mu, sig, amp )
%GMM_UBORDER_FUN Finds the function which represents GMM.
%   f = GMM_SUM_FUN(mu,sig,amp) - returns a handle to function (f)
%   which calculates value of sum operating on values of
%	distributions specified by its mean (mu), standard deviation (sig) and
%	amplitude (amp). Parameters: mu, sig, amp can be at max two dimensional.
	
	if sum(size(mu)==size(sig))<length(size(mu)) || sum(size(mu)==size(amp))<length(size(mu))
		error('Inconsistent sizes of input arguments');
	end
	
	[n,m] = size(mu);
	
	f = @(x) zeros(size(x));
	for i=1:n
		for j=1:m
			f = @(x) amp(i,j)*normpdf(x,mu(i,j),sig(i,j))+f(x);
		end
	end
	
end

