function [ merged ] = filthy_merge( primary, secondary, replaced )
%FILTHY_MERGE Merges the vectors containing data partition and
%subpartition of one part leaving empty clusters.
%   merged = FILTHY_MERGE(primary,secondary,repl) - returns the vector
%   containing cluster specified by repl from vector primary split
%   according to vector secondary.
	
	if isempty(secondary)
		merged = primary;
		return;
	end

	merged = primary;
	
	%min_primary = min(primary);
	max_primary = max(primary);
	min_secondary = min(secondary);
	%max_secondary = max(secondary);
	
	secondary = secondary + max_primary - min_secondary + 1;
	
	merged(primary==replaced) = secondary;% + replaced - 1;
	
end