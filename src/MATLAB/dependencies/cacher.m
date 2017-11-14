function varargout = cacher( fun, fun_in, varargin )
%CACHER For specified function handle and parameters caches result
%   Simple result caching function.
%	output = CACHER( fun, params ) - returns the same result as function
%	fun for the arguments specified in params cell, but caches the result
%	to avoid unnecessary calculations overheating
%	output = CACHER( ___, 'Name', 'Value') allows to specify additional
%	options:
%	- 'CachePath' - specifies folder, where cache has to be established
%	(default current working directory)
%	- 'DropOnHashError' - specifies whether error in hashing stops the
%	execution and the exception is thrown or if the execution continues
%	with a warning (default false - warning is thrown).
%	- 'OutputsNumber' - in the case of function with unknown number of
%	output arguments, this option is mandatory.
%	- 'Enabled' - allows to disable caching without making significant
%	changes to code. (default true)
%
%	Please note, that whole function output will be cached (what matters
%	especially in case of big data, where enormous storages can be occupied
%	on hard disk by this cacher). To avoid that, one can wrap one function
%	into another, returning much smaller data (e.g., using lambda functions
%	or any other way).
%
%	Author: Grzegorz Mrukwa

	%%APPLY SETTINGS
    settings = apply_user_configs(@get_defaults,varargin);

    if nargout(fun) == -1 && settings.OutputsNumber == -1
        error(['Number of outputs must be specified in the case of ', ...
            'function with unknown number of output arguments.']);
    elseif nargout(fun) == -1
        nout = settings.OutputsNumber;
    else
        nout = nargout(fun);
    end

    if ~settings.Enabled
        [varargout{1:nout}] = fun(fun_in{:});
        return;
    end

	try
		fun_hash = DataHash(fun);
		fun_in_hash = DataHash(fun_in);
	catch hash_error
		if settings.DropOnHashError
			rethrow(hash_error);
		else
			warning([sprintf('Hash calculation failed. Skipping partial result check and save. Error report:\n') getReport(hash_error)]);
			try
				%%CALCULATE NONCACHED RESULT
				[varargout{1:nout}] = fun(fun_in{:});
				return;
			catch e
				rethrow(e);
			end
		end
	end

	%%CHECK CACHE
	[was_cached, varargout] = check_result(fun_hash,fun_in_hash,settings);
	if was_cached
		return;
	end

	%%CALCULATE NONCACHED RESULT
    dname = diary_fname(fun_hash,fun_in_hash,settings);
    logged = capture_diary(fun,dname,'OutputsNumber',nout);
	[varargout{1:nout}] = logged(fun_in{:});
	%%SAVE NONCACHED RESULT
	save_result(fun_hash,fun_in_hash,varargout,settings);
end

function settings = get_defaults()
	settings.CachePath = '.';
	settings.DropOnHashError = false;
    settings.OutputsNumber = -1;
    settings.Enabled = true;
% 	settings.OneFile = true;
% 	settings.OneFile = false;
end

% function dictionary_cache = get_cache(settings)
% 	persistent cache;
% 	if isempty(cache)
% 		cache = containers.Map();
% 		if exist([settings.CachePath '\cache.mat'],'file')==2
% 			load([settings.CachePath '\cache.mat']);
% 		end
% 	end
% 	dictionary_cache = cache;
% end

function [was_cached, result] = check_result(fun_hash,fun_in_hash,settings)
	slash = get_dir_char();
% 	if ~settings.OneFile
		path = [settings.CachePath slash 'cache' slash fun_hash slash fun_in_hash '.mat'];
		if exist(path,'file')~=2
			was_cached = false;
			result = {};
			return;
		end
		load(path);
        path = [settings.CachePath slash 'cache' slash fun_hash slash fun_in_hash '.txt'];
        if exist(path,'file')==2
            type(path);
        else
            warning('Report missing.');
        end
		was_cached = true;
% 	else
% 		dictionary = get_cache(settings);
% 		if ~isKey(dictionary,fun_hash) || ~isKey(dictionary(fun_hash),fun_in_hash)
% 			was_cached = false;
% 			result = {};
% 			return;
% 		end
% 		results = dictionary(fun_hash);
% 		result = results(fun_in_hash);
% 		was_cached = true;
% 	end
end

function save_result(fun_hash,fun_in_hash,result,settings) %#ok<INUSL>
	slash = get_dir_char();
% 	if ~settings.OneFile
		path = [settings.CachePath slash 'cache' slash fun_hash];
		if exist(path,'dir')~=7
			mkdir(path);
		end
		save([path slash fun_in_hash '.mat'],'result');
% 	else
% 		cache = get_cache(settings);
% 		if ~isKey(cache,fun_hash)
% 			cache(fun_hash) = containers.Map();
% 		end
% 		results = cache(fun_hash);
% 		results(fun_in_hash) = result; %#ok<NASGU>
% 		save([settings.CachePath slash 'cache.mat'],'cache');
% 	end
end

function fname = diary_fname(fun_hash,fun_in_hash,settings)
    slash = get_dir_char();
    fname = [settings.CachePath slash 'cache' slash fun_hash slash ...
        fun_in_hash '.txt'];
end

function dir_char = get_dir_char()

	os = getenv('OS');
	if(strcmp(os,'Windows_NT'))
		dir_char = '\';
	else
		dir_char = '/';
	end

end
