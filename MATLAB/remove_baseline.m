function grounded = remove_baseline(mz, data)
%REMOVE_BASELINE removes baseline from the data
%   grounded = REMOVE_BASELINE(mz, data) returns data set where baseline is
%   removed, with subsequent m/z in columns and observations in rows.
%   Observations order from data matrix is preserved. Matrix data should
%   have m/z in columns and observations in rows as well.

	grounded = NaN(size(data));
    for spec_no = 1:size(data, 1)
        grounded(spec_no, :) = msbackadj(mz, data(spec_no, :));
    end
    grounded(grounded < 0) = 0;

end