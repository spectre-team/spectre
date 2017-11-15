function unimodal = is_unimodal(data)
%IS_UNIMODAL
%   unimodal = IS_UNIMODAL(data)

    counts = histcounts(data);
    
    if length(counts) < 2

        unimodal = true;
        
    else
    
        fake_spacing = 1:length(counts);

        peaklist = mspeaks(fake_spacing, counts, ...
            'Denoising', false);

        unimodal = size(peaklist, 1) == 1;

    end
    
end