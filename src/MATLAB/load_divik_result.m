function divik_result = load_divik_result(path)
%LOAD_DIVIK_RESULT Loads result tree of DiviK algorithm
%   divik_result = LOAD_DIVIK_RESULT(path)
    
    m = matfile(path);
    divik_result = m.res_struct;
    
end