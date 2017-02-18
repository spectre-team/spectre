load sample_data.mat

addpath(genpath('./libs'));

% data has observations in rows and gmm components in columns
% xy has observations in rows, x in first column, y in second
partition = divik(data, xy, 'Level', 2, ...
    'PlotPartitions', true, ...
    'PlotRecursively', true);