function partition_plotter( xy, partition, index, out_path, filename, title_on, varargin )
%PARTITION_PLOTTER Plots the partition using xy positions
%   PARTITION_PLOTTER( xy, partition, index, out_path, filename, title_on )
%   PARTITION_PLOTTER( xy, partition, index, out_path, filename, title_on, coloring_enabled )
%   PARTITION_PLOTTER( xy, partition, index, out_path, filename, title_on, colors )
	
	if exist(out_path,'dir')~=7
		mkdir(out_path);
	end
	
	%plotting initialization
	[~,objects_number] = size(xy);
	left = min(xy(1,:));
	right = max(xy(1,:));
	top = max(xy(2,:));
	down = min(xy(2,:));
	moved_xy = [ xy(1,:)-left+1; xy(2,:)-down+1];
	width = max(moved_xy(1,:))+1;
	height = max(moved_xy(2,:))+1;
	
	%mono plotting
	A = ones(height,width);
	part_num = max(partition);
	for j=1:objects_number
		A(moved_xy(2,j),moved_xy(1,j)) = 1-partition(j)/part_num;
	end
	if title_on
		filename = [filename '__k_' num2str(part_num) '__dunn_' num2str(index) '_'];
	end
	imwrite(resize(A,5),[out_path '\' filename '_mono.png']);
	
	%color plotting
	if ~isempty(varargin) && islogical(varargin{1}) && varargin{1}
		%% COLORING DISABLED
		C = color_mono_drawing([out_path '\' filename '_mono.png']);
		imwrite(C,[out_path '\' filename '_color.png'])
		%% COLORING DISABLED END
	elseif ~isempty(varargin) && isfloat(varargin{1})
		C = cat(3, A, A, A);
		colors = varargin{1};
		for j=1:objects_number
			C(moved_xy(2,j),moved_xy(1,j),:) = colors(partition(j),:);
		end
		imwrite(C,[out_path '\' filename '_color.png'])
	end
	
end

