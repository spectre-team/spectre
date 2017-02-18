function [centroids, identification] = fetch_centroids_from_partition( data, partition )
%FETCH_CENTROIDS_FROM_PARTITION Calculates cluster centroids
%   [centroids, identification] = FETCH_CENTROIDS_FROM_PARTITION( data, partition )

	%n = max(partition);
	%m = min(partition);
	centroids = [];
	identification = [];

	for i=unique(partition)
		%if sum(partition==i)>0
			centroids = [centroids mean(data(:,partition==i),2)];
			identification = [identification i];
		%end
	end

end
