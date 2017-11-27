function similarity_classes = approximate_region_binding(name, data, partition, approximated_region, varargin)
%APPROXIMATE_REGION_BINDING Finds region approximation
%   similarity_classes = APPROXIMATE_REGION(name, divik_source, ...
%   divik_result, approximated_region_names) - approximating
%   function with consistent interface
%   Supported approximators:
%   - exROI (exroi function)
%   - greedy (approximate_by_dice function)
%   ___ = APPROXIMATE_REGION( ___, Name, Value ) - passes optional
%   parameters to the underlying extractor.

    assert(ischar(name));
    params = {data, partition, approximated_region, varargin{:}}; %#ok<CCAT> - empty optionals case

    if strcmp(name, 'exROI')
        
        similarity_classes = wrapped_exroi_binding(params{:});
        
    elseif strcmp(name, 'greedy')
        
        similarity_classes = wrapped_dice_approximation_binding(params{:});
        
    else
        
        msgID = 'classification:training_set_selection:approximate_region_binding';
        msg = ['Unknown approximator: ' name];
        throw(MException(msgID, msg));
        
    end

end