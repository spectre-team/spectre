function normalized = ticnorm(data)
%TICNORM performs TIC normalization on dataset
%   normalized = TICNORM(data) returns TIC normalized dataset. Columns
%   correspond to m/z, rows to observations, same as in matrix data.

    spect = data';
    
    tic1 = NaN(1,size(spect,2));
    tic2 = NaN(1,size(spect,2));
    %1 i 2
    for i=1:size(spect,2)
        tic1(i) = sum(spect(:,i));
        tic2(i) = tic1(i)/size(spect,1);
    end
    %3
    tic3 = nanmean(tic2);
    %4
    tic4 = tic2/tic3;
    %5
    normalized = NaN(size(spect));
    for i=1:size(spect,2)
        normalized(:,i) = spect(:,i)*1/tic4(i);
    end
    normalized = normalized';

end