function logged = capture_diary(f,fname,varargin)
%CAPTURE_DIARY Makes function capturing diary to file.
%	f = CAPTURE_DIARY(f,fname) returns another function which saves
%   its results to a file during running.
%   f = CAPTURE_DIARY( ___, 'Name', 'Value') allows to specify
%   additional Name-Value pairs of arguments:
%   - 'OutputsNumber' - in the case of function with unknown number of
%   output arguments, this option is mandatory.
    
    settings = apply_user_configs(@defaults,varargin);
    
    if nargout(f) == -1 && settings.OutputsNumber == -1
        error(['Number of outputs must be specified in the case of ', ...
            'function with unknown number of output arguments.']);
    elseif nargout(f) == -1
        nout = settings.OutputsNumber;
    else
        nout = nargout(f);
    end
    
    function varargout = f_logged(varargin)
        mkfiledir(fname);
        diary(fname);
        [varargout{1:nout}] = f(varargin{:});
        diary('off');
    end

    logged = @f_logged;

end

function settings = defaults()
    settings = struct();
    settings.OutputsNumber = -1;
end