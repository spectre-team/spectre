function found = rows_in(to_find, reference)
%ROWS_IN checks which rows are present in control matrix
%   found = ROWS_IN(to_find, reference)
    
    found = ismember(to_find, reference, 'rows');
    
end