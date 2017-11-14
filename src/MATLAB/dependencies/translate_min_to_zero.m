function normalized = translate_min_to_zero(data)
%TRANSLATE_MIN_TO_ZERO subtracts min of columns
%   normalized = TRANSLATE_MIN_TO_ZERO(data)s

    mins = min(data,[],1);
    normalized = data - repmat(mins, size(data, 1), 1);
    
end