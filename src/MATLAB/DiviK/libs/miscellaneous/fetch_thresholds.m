function [ thresholds ] = fetch_thresholds( params, varargin )
%FETCH_THRESHOLDS Decomposes parameters into Gaussian peaks and finds
%crossings
%   thresholds = FETCH_THRESHOLDS(params) - returns cell containing all
%   thresholds for every column of parameters. If no crossings are present,
%   no threshold will be returned for specific parameter. Parameter params
%   should be column vector with values of parameter investigates or matrix
%   consisting of such column vectors. For matrix m x n a cell 1 x n will
%   be returned where every field contains all available thresholds as
%   column vector.
%	Additional parameters:
%	- 'MaxComponents' - (default 10)
%	- 'CachePath' - (default '.')
%	- 'Cache' - default false - no cache is created.
%	- 'FigurePath' - decomposition plot is saved (default empty - no figure
%	is plotted)
%	- 'Threshold' - returns only thresholds associated with components of
%	higher amplitude (default 0)

	settings = apply_user_configs(varargin);

	nvarargs = length(varargin);

	%parameter in column with values in the rows
	[n,m] = size(params);

	max_components_number = settings.MaxComponents;

	thresholds = cell(1,m);

	%for every parameter considered
	for i=1:m

		a = cell(1,max_components_number);
		mu = cell(1,max_components_number);
		sig = cell(1,max_components_number);
		TIC = cell(1,max_components_number);
		BIC = Inf*ones(1,max_components_number);
		param = params(:,i);
		parfor compnum=1:max_components_number
% 		for compnum=1:max_components_number
			try
				[a{compnum},mu{compnum},sig{compnum},TIC{compnum},l_lik]=cacher(@gaussian_mixture_simple,{param,ones(n,1),compnum},'CachePath',settings.CachePath,'Enabled',settings.Cache);
				BIC(compnum) = -2*l_lik+(3*compnum-1)*log(n);
            catch
				warning(['Failed to decompose ' num2str(i) '. parameter into ' num2str(compnum) ' components.']);
			end
		end

		best_compnum = find(BIC == min(BIC),1);
		best_mu = mu{best_compnum};
		best_sig = sig{best_compnum};
		best_a = a{best_compnum};
		%FIX
		TIC = TIC{best_compnum};

		f = gmm_uborder_fun(best_mu, best_sig, best_a);

		if ~isempty(settings.FigurePath)

			summing = gmm_sum_fun(best_mu,best_sig,best_a);
			fig = figure;
			fig.CurrentAxes = axes('Position' , [0.1 0.1 0.8 0.8]);
			ax = fig.CurrentAxes;
			set(fig,'Visible','off');
			hold on
			hist(ax,param,floor(sqrt(length(param))));
			[nn,xx] = hist(ax,param,floor(sqrt(length(param))));
			TIC = (xx(2)-xx(1))*n;
			xvec=linspace(min(param),max(param),1000);
			for jj=1:best_compnum,
				h(1)=plot(ax,xvec,TIC*best_a(jj)*normpdf(xvec,best_mu(jj),best_sig(jj)),'g');
				set(h(1),'Linewidth',2)
			end
			yvec = summing(xvec)*TIC;
			h(2)=plot(ax,xvec,yvec,'r');
			set(h(2),'Linewidth',3)
			xlabel(ax,['Parameter ' num2str(i)])
			legend(ax,h,{'GMM components';'Final model'})
			title(ax,['Gaussian Mixture Model, KS = ',num2str(best_compnum)]);
			hold off
			if m>1
				mkfiledir([settings.FigurePath '_param_' num2str(i)]);
				saveas(fig,[settings.FigurePath '_param_' num2str(i)]);
			else
				mkfiledir(settings.FigurePath);
				saveas(fig,settings.FigurePath);
			end
			close gcf


		end

		%if there are few components
		if best_compnum>1
			%they are picked pairwise
			for j=1:(best_compnum-1)
				for k=(j+1):best_compnum

					weight_condition = TIC*best_a(j)*normpdf(best_mu(j),best_mu(j),best_sig(j)) > settings.Threshold && ...
                            TIC*best_a(k)*normpdf(best_mu(k),best_mu(k),best_sig(k)) > settings.Threshold;

					%their upper bound
					g = gmm_uborder_fun(best_mu([j,k]), best_sig([j,k]), best_a([j,k]));
					%the disjunction condition & weight condition is checked
					if g(best_mu(j))==best_a(j)*normpdf(best_mu(j),best_mu(j),best_sig(j)) && ...
							g(best_mu(k))==best_a(k)*normpdf(best_mu(k),best_mu(k),best_sig(k)) && ...
							weight_condition
						%their crossing is found
						crossing = fminbnd(g,min(best_mu([j,k])),max((best_mu([j,k]))));
						%and it is checked to be on the upper border
						if f(crossing)==g(crossing)
							%if it truly is, then it is saved
							thresholds{i} = [thresholds{i}; crossing];
						end
					end
				end
			end
			thresholds{i} = sort(thresholds{i},'ascend');
		end

	end
end

function settings = set_defaults()
	settings.MaxComponents = 10;
    settings.Cache = false;
	settings.CachePath = '.';
	settings.FigurePath = '';
	settings.Threshold = 0;
end


function settings = apply_user_configs(configs)
%APPLY_USER_CONFIGS Applies all of the features specified by the user.
%	settings = APPLY_USER_CONFIGS(configs)

	settings = set_defaults();

	nvararg = length(configs);

	for i = 1:nvararg
		if mod(i,2)==1
			property_name = configs{i};
		else
			settings.(property_name) = configs{i};
		end
	end

end
