---
title: "DivikResultConverter Software Manual"
date: "08.11.2017"
author:
    - Grzegorz Mrukwa Grzegorz.Mrukwa@polsl.pl
institute: |
    The Spectre Team, Data Mining Group, Silesian University of Technology
toc: true
tags: [divik, maldi, msi, mass spectrometry, compression, clustering]
abstract: |
    This tool is intended to convert MATLAB DiviK algorithm result file into a data format to be used with Spectre software.
colorlinks: true
---

# Installation

## MATLAB MCR

In order to make DiviK software fully operational, a MCR (MATLAB Compiler
Runtime) has to be installed.

However, due to the [licensing by MathWorks](https://www.mathworks.com/matlabcentral/answers/25863-mcr-installer-is-there-a-license-file-whic#comment_57242),
it is not available through project website. Nevertheless, we are still eligible
to share it with you personally, so feel free to contact us by
[mail](mailto:Grzegorz.Mrukwa@polsl.pl?subject=[Spectre] MCR installer).

Please note, that it can be also downloaded through
[MathWorks website](https://www.mathworks.com/products/compiler/mcr.html) for
free, but this way has not been tested yet.

## Conversion software
No installation required. Once the files are unpacked from the `.zip ` archive,
software can be used without further delay.

# Usage

Usage is simplified to minimum. Just drag-and-drop your `mat` result file on the
converter application file, and the conversion process launches. Result will be named exactly the same, but with `json` extension.

# Parameters

Application can be also used from command line.

1. **Input path** - must point to the file with data to validate.

# Final notes

In case of any questions, do not hesitate to contact us by
[mail](mailto:Grzegorz.Mrukwa@polsl.pl?subject=[Spectre] Help request).

# References

This software is part of contribution made by
[Data Mining Group of Silesian University of Technology](http://www.zaed.polsl.pl/),
rest of which is published [here](https://github.com/ZAEDPolSl).

+ [Marczyk M, Polanska J, Polanski A: Comparison of Algorithms for Profile-Based
Alignment of Low Resolution MALDI-ToF Spectra. In Advances in Intelligent
Systems and Computing, Vol. 242 of Man-Machine Interactions 3, Gruca A,
Czachorski T, Kozielski S, editors. Springer Berlin Heidelberg 2014, p. 193-201
(ISBN: 978-3-319-02308-3), ICMMI 2013, 22-25.10.2013 Brenna,
Poland](http://link.springer.com/chapter/10.1007/978-3-319-02309-0_20)
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
imaging," Biochimica et Biophysica Acta (BBA)-Proteins and Proteomics,
2016](http://www.sciencedirect.com/science/article/pii/S1570963916302175)
+ [G. Mrukwa, G. Drazek, M. Pietrowska, P. Widlak and J. Polanska, "A Novel
Divisive iK-Means Algorithm with Region-Driven Feature Selection as a Tool for
Automated Detection of Tumour Heterogeneity in MALDI IMS Experiments," in
International Conference on Bioinformatics and Biomedical Engineering,
2016](http://link.springer.com/chapter/10.1007/978-3-319-31744-1_11)
+ [A. Polanski, M. Marczyk, M. Pietrowska, P. Widlak and J. Polanska, "Signal
partitioning algorithm for highly efficient Gaussian mixture modeling in mass
spectrometry," PloS one, vol. 10, no. 7, p. e0134256,
2015](http://journals.plos.org/plosone/article?id=10.1371/journal.pone.0134256)
