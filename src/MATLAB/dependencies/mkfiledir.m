function res = mkfiledir(fname)
%MKFILEDIR Makes directory for a filename specified, if non existing.
%   res = MKFILEDIR(fname)

    last = find(fname=='\' | fname=='/',1,'last');
    dir_fname = fname(1:last-1);
    if exist(dir_fname,'dir')~=7
        res = mkdir(dir_fname);
    else
        res = 1;
    end

end
