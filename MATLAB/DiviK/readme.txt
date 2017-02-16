This directory contains demo of DiviK algorithm.

Content:

- 'libs' directory contains all libraries used for DiviK algorithm.
- 'sample_data.mat' is data file with data after GMM modelling of a single preparation (available here: https://1drv.ms/u/s!AvaKDFGHXrvCjPlfSAGzQuCxs5_NrA).
- 'demo.m' is the demo script file.

How to run:

- Copy whole directory onto your computer, into a writable location
- Open MATLAB in that location
- Run script 'demo.m'

What will be the result:

- file 'downmerged_mono.png' showing merged partitions from two levels of DiviK
- file 'primary_(...)_mono.png' showing partition at top level
- directory '1' with file 'primary_(...)_mono.png' showing partition of first subregion found
- directory '2' with file 'primary_(...)_mono.png' showing partition of second subregion found
- directory 'cache' which contains disk cache of functions used internally by DiviK.
  It speeds up repeated calculations by using partial results in the case analysis was interrupted
  and should be resumed. This feature can be disabled.
- directory 'kmeans_cache' with similar purpose as above, but designed separately for own k-means
  algorithm implementation.

How to apply DiviK to another data?

Implementation contains documentation comment, which describes all capabilities. It is sufficient
to add 'libs' directory to MATLAB path and then just write 'help divik' in MATLAB command line.
All input and output parameters are described, as well as named parameters which allow to apply
DiviK for segmentation or compression.

Included sample has been tested on MATLAB R2016a, R2016b.