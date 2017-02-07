function [partition, thresholds] = gmm_segmentation( data, varargin )
%GMM_SEGMENTATION Performs segmentation basing on GMM decomposition
%thresholds
%   partition = GMM_SEGMENTATION( data ) - features should be in rows,
%   observations in columns. For mxn data matrix returns mxn partition
%   matrix with partition of objects with respect to every feature (if any
%   thresholds may be obtained).
%	partition = GMM_SEGMENTATION( data, out_dir ) - saves all decomposition
%	plots into specified directory
%	[partition, thresholds] = GMM_SEGMENTATION( ___ ) - returns also
%	obtained thresholds
	
	[m,~] = size(data);
	if length(varargin)==1
		out_dir = varargin{1};
		if exist(out_dir,'dir')~=7
			mkdir(out_dir);
		end
% 		[m,n] = size(data);
% 		parfor i=1:m
% 			hist(data(i,:),sqrt(n));
% 			saveas(gcf,[out_dir '\hist_' num2str(i)],'png');
% 			saveas(gcf,[out_dir '\hist_' num2str(i)],'fig');
% 			close;
% 			hist(tukey_outlier_detection(data(i,:)),sqrt(n));
% 			saveas(gcf,[out_dir '\hist_tukey_' num2str(i)],'png');
% 			saveas(gcf,[out_dir '\hist_tukey_' num2str(i)],'fig');
% 			close;
% 		end

		%%outlier detection disabled
		%thresholds = fetch_thresholds(data',0,[out_dir '\score']);
		%%outlier detection enabled
		%thresholds = arrayfun(@(i) fetch_thresholds(tukey_outlier_detection(data(i,:))',0,[out_dir '\score_param_' num2str(i)]),1:m,'UniformOutput',false);
		thresholds = cell(m,1);
		for i = 1:m
			outlier_removed_data = cacher(@huberta_outlier_detection,{data(i,:)},'CachePath',[out_dir '\score_param_' num2str(i)]);
			thresholds{i} = cell2mat(fetch_thresholds(outlier_removed_data',0,[out_dir '\score_param_' num2str(i)]));
			i
		end
	else
		%%outlier detection disabled
		%thresholds = fetch_thresholds(data');
		%%outlier detection enabled
		%thresholds = arrayfun(@(i) fetch_thresholds(tukey_outlier_detection(data(i,:))'),1:m,'UniformOutput',false);
		thresholds = cell(m,1);
		for i = 1:m
			thresholds{i} = cell2mat(fetch_thresholds(huberta_outlier_detection(data(i,:))'));
			i
		end
	end
	
	partition = ones(size(data));
	
	parfor i = 1:length(thresholds)
		t = thresholds{i};
		if ~isempty(t)
			row = partition(i,:)*length(t);
			for j=1:length(t)
				row(data(i,:)>=t(j)) = length(t) - (j-1);
			end
			partition(i,:) = row;
		end
	end
	
end

