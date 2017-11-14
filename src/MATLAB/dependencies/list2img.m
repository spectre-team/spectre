function [img, xy] = list2img(xy, vals, default)
%LIST2IMG Converts coordinates list to displayable image
%   img = LIST2IMG(xy) returns an image where coordinates in xy are marked
%   in black. Matrix xy should consist of two columns and arbitrary number
%   of rows.
%   img = LIST2IMG(xy, vals) allows to specify gray level for particular
%   observation. Matrix vals should have the same length as the number of
%   rows in xy.
%   img = LIST2IMG(xy, vals, default) allows to specify background color
%   for the image. 'default' should be a single number.
%   [img, xy] = LIST2IMG( ___ ) returns also a translated matrix of
%   coordinates

    if nargin < 2
        vals = zeros(size(xy, 1), 1);
    elseif size(vals,1) ~= size(xy,1)
        error('Improper length of ''vals''');
    end
    if nargin < 3
        if size(vals,2) == 1
            default = 255;
        else
            default = [255, 255, 255];
        end
    elseif length(default) ~= 1 && length(default) ~= 3
        error('''default'' should be a single number');
    end
    if nargin > 3 || nargin == 0
        error('Wrong number of arguments provided');
    end
    
    xy = translate_min_to_zero(xy) + 1;
    xy = invert_y(xy);
    
    img = [];
    
    for i = 1:size(vals, 2)
        tmp_img = default(i) * uint8(ones(max(xy)))';
        idx = sub2ind(size(tmp_img), xy(:,2), xy(:,1));
        tmp_img(idx) = vals(:,i);
        img = cat(3, img, tmp_img);
    end
    
end

function xy = invert_y(xy)
    
    biggest = max(xy(:,2));
    xy(:,2) = 1 + biggest - xy(:,2);
    
end