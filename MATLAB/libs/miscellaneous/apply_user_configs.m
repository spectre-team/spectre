function settings = apply_user_configs(settings_gen,configs)
%APPLY_USER_CONFIGS Applies all of the features specified by the user.
%	settings = APPLY_USER_CONFIGS(settings_gen,configs)
	
	nvararg = length(configs);
	settings = settings_gen();
    
	for i = 1:nvararg
		if mod(i,2)==1
			property_name = configs{i};
        else
            if isfield(settings,property_name)
                settings.(property_name) = configs{i};
            else
                error(['Unknown option ' property_name '.']);
            end
		end
    end
	
end