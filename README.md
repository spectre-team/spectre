# Spectre

Spectre is a versatile tool used for analysis of MALDI-MSI data sets.

For the sake of simplicity, the toolset provided is available to be used
through interfacing with web application, which is currently a work-in-progress.

In order to build and run the application, please refer to the
[installation](#install) section.

## About

The project is currently in its early stage. However, it comprises the
implementation of our own spectra modelling based on Gaussian Mixture Models,
and Divisive IK-means algorithm for unsupervised segmentation, which can be
used for efficient dataset compression as well as for knowledge discovery.
Aformentioned algorithms have already been published and links refering
have been enclosed under [references](#references) section.

Also, several classification and clusterization methods will be provided soon,
along with supporting statistics.

## Install

### DiviK local client

Please refer to [this manual](docs/Spectre.DivikWpfClient.pdf), as MATLAB Common Runtime has to be installed.

### Spectre service & web application

It is all the time available [here](http://vaei-bit01.aei.polsl.pl/). If you still would like to host it yourself, please contact us by and [e-mail](mailto:Grzegorz.Mrukwa@polsl.pl). We will provide you exhaustive explanations.

## Exemplary usage

### DiviK local client

Please refer to [docs](docs/Spectre.DivikWpfClient.pdf).

### Spectre service & web application

The application is available online [here](http://vaei-bit01.aei.polsl.pl/).

Right now our web application allows only an interactive visualization of some of the data we were using in the research, along with Divisive Intelligent K-means algorithm results. More features will get documented, when they appear.

## How to contribute?

Please contact us by an [e-mail](mailto:Grzegorz.Mrukwa@polsl.pl). We will
answer you in details.

Short contribution guide is actively constructed on [the project's wiki](https://github.com/spectre-team/spectre/wiki) with the development progress.

## Environment

Please refer to [docs](docs/Spectre.DivikWpfClient.pdf).

## References

This software is part of contribution made by [Data Mining Group of Silesian University of Technology](http://www.zaed.polsl.pl/), rest of which is published [here](https://github.com/ZAEDPolSl).

+ [Marczyk M, Polanska J, Polanski A: Comparison of Algorithms for Profile-Based
Alignment of Low Resolution MALDI-ToF Spectra. In Advances in Intelligent
Systems and Computing, Vol. 242 of Man-Machine Interactions 3, Gruca A,
Czachorski T, Kozielski S, editors. Springer Berlin Heidelberg 2014, p. 193-201
(ISBN: 978-3-319-02308-3), ICMMI 2013, 22-25.10.2013 Brenna, Poland](http://link.springer.com/chapter/10.1007/978-3-319-02309-0_20)
+ [P. Widlak, G. Mrukwa, M. Kalinowska, M. Pietrowska, M. Chekan, J. Wierzgon, M.
Gawin, G. Drazek and J. Polanska, "Detection of molecular signatures of oral
squamous cell carcinoma and normal epithelium - application of a novel
methodology for unsupervised segmentation of imaging mass spectrometry data,"
Proteomics, vol. 16, no. 11-12, pp. 1613-21,
2016](http://onlinelibrary.wiley.com/doi/10.1002/pmic.201500458/pdf)
+ [M. Pietrowska, H. C. Diehl, G. Mrukwa, M. Kalinowska-Herok, M. Gawin, M.
Chekan, J. Elm, G. Drazek, A. Krawczyk, D. Lange, H. E. Meyer, J. Polanska, C.
Henkel, P. Widlak, "Molecular profiles of thyroid cancer subtypes:
Classification based on features of tissue revealed by mass spectrometry
imaging," Biochimica et Biophysica Acta (BBA)-Proteins and Proteomics, 2016](http://www.sciencedirect.com/science/article/pii/S1570963916302175)
+ [G. Mrukwa, G. Drazek, M. Pietrowska, P. Widlak and J. Polanska, "A Novel
Divisive iK-Means Algorithm with Region-Driven Feature Selection as a Tool for
Automated Detection of Tumour Heterogeneity in MALDI IMS Experiments," in
International Conference on Bioinformatics and Biomedical Engineering, 2016](http://link.springer.com/chapter/10.1007/978-3-319-31744-1_11)
+ [A. Polanski, M. Marczyk, M. Pietrowska, P. Widlak and J. Polanska, "Signal
partitioning algorithm for highly efficient Gaussian mixture modeling in mass
spectrometry," PloS one, vol. 10, no. 7, p. e0134256, 2015](http://journals.plos.org/plosone/article?id=10.1371/journal.pone.0134256)
