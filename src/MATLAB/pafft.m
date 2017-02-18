function aligned = pafft(mz, data)
%PAFFT Wraps adaptive_PAFFT in easy manner.
%   aligned = PAFFT(mz, data) returns a matrix with aligned spectra. M/z
%   are in columns, observations are in rows, the same as in matrix input
%   data.

    reference = nanmean(data,2);
    aligned =  adaptive_PAFFT(mz,data', reference, 0.7, 0.1)';

end