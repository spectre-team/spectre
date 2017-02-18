function [ merged, st_trans_table, nd_trans_table ] = merge_partitions( primary, secondary, replaced )
%MERGE_PARTITIONS Merges the vectors containing data partition and
%subpartition of one part.
%   merged = MERGE_PARTITIONS(primary,secondary,repl) - returns the vector
%   containing cluster specified by repl from vector primary split
%   according to vector secondary.
%	[merged,st,nd] = MERGE_PARTITIONS(primary,secondary,repl) - also
%	describes to which indexes old indexes are binded.
%	Works only for integer indexes.
	
	merged = NaN*primary;
	
	min_primary = min(primary);
	max_primary = max(primary);
	min_secondary = min(secondary);
	max_secondary = max(secondary);
	
	st_trans_table = NaN*zeros(max_primary-min_primary+1,1);
	st_trans_table(1:(replaced - min_primary),1) = transpose(1:(replaced - min_primary));
	st_trans_table((replaced - min_primary + 2):end,1) = transpose((replaced - min_primary + 2):(max_primary-min_primary+1)) + max_secondary - min_secondary;
	nd_trans_table = NaN*zeros(max_secondary-min_secondary+1,1);
	nd_trans_table(1:(max_secondary-min_secondary+1),1) = transpose(1:(max_secondary-min_secondary+1)) + replaced - 1;
	
	primary = primary - min_primary + 1;
	secondary = secondary - min_secondary + 1;
	replaced = replaced - min_primary + 1;
	
	merged(primary>replaced) = primary(primary>replaced) + max_secondary - min_secondary;
	merged(primary==replaced) = secondary + replaced - 1;
	
end

