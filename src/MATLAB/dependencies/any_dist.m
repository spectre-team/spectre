function dist = any_dist( metric_name, v1, v2 )
%ANY_DIST Calculates distance
%	dist = ANY_DIST( metric_name, v1, v2 )
%   Calculates distance between two specified vectors using specified
%   metric.

	if strcmp(metric_name,'pearson')
		dist = 1-corr(v1,v2,'type','Pearson');
	elseif strcmp(metric_name,'pearson_squared')
		dist = 1-power(corr(v1,v2,'type','Pearson'),2);
	elseif strcmp(metric_name,'spearman')
		 dist = 1-corr(v1,v2,'type','Spearman');
	elseif strcmp(metric_name,'canberra')
		dist_f = @(v1,v2) sum(abs(v1-v2)./(abs(v1)+abs(v2)),1);
		dist = array_result(dist_f,v1,v2);
	elseif strcmp(metric_name,'euclidean')
        dist = pdist2(v1',v2',metric_name);
        return;
		dist_f = @(v1,v2) sqrt(sum(power(v1-v2,2),1));
		dist = array_result(dist_f,v1,v2);
	else
		dist = squareform(pdist([v1 v2]',metric_name));
		[~,nv1]=size(v1);
		dist = dist(1:nv1,(nv1+1):end);
	end

end

function dist = array_result(fun,v1,v2)
%ARRAY_RESULT Applies specified distance metric to deal with arrays.

	[~,nv1] = size(v1);
	[~,nv2] = size(v2);
	dist = NaN(nv1,nv2);
	for i = 1:nv1
		v = v1(:,i);
		parfor j=1:nv2
			dist(i,j) = fun(v,v2(:,j)); %#ok<PFBNS>
		end
	end

end