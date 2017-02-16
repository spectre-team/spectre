function cell_settings = copy_settings(settings)
	fields = fieldnames(settings);
	cell_settings = cell(1,2*numel(fields));
	for i=1:numel(fields)
		cell_settings{2*i-1} = fields{i};
		cell_settings{2*i} = settings.(fields{i});
	end
end