function fig = invisible_figure()
%INVISIBLE AXES Creates new invisible figure
%   figure = invisible_figure()

    fig = figure;
	set(fig,'Visible','off');

end