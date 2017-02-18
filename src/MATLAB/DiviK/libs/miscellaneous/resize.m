function A = resize( a, multiplier )
%RESIZE Scales matrix copying objects.
%   A = RESIZE(a,multiplier) returns matrix A which consists of square
%   block matrices of size multiplier and values the same for matrix on
%   i,j-th position in A as for value on i,j-th position in a.
	
	if length(size(a))==3
		[h,w,z] = size(a);
		A = NaN*zeros(h*multiplier,w*multiplier,z);

		if isa(a,'uint8')
			for i=1:h
				for j=1:w
					for k=1:z
						A(multiplier*(i-1)+1:multiplier*i,multiplier*(j-1)+1:multiplier*j,k) = uint8(ones(multiplier))*a(i,j,k);
					end
				end
			end
		else
			for i=1:h
				for j=1:w
					for k=1:z
						A(multiplier*(i-1)+1:multiplier*i,multiplier*(j-1)+1:multiplier*j,k) = ones(multiplier)*a(i,j,k);
					end
				end
			end
		end
	else
		A = NaN*zeros(size(a)*multiplier);

		[h,w] = size(a);
		for i=1:h
			for j=1:w
				A(multiplier*(i-1)+1:multiplier*i,multiplier*(j-1)+1:multiplier*j) = ones(multiplier)*a(i,j);
			end
		end
	end

end

