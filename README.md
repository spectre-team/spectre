# Spectre

![Spectre](https://user-images.githubusercontent.com/1897842/31115297-0fe2c3aa-a822-11e7-90e6-92ceccf76137.jpg)

Spectre is a versatile tool used for analysis of MALDI-MSI data sets.

For the sake of simplicity, the toolset provided is available to be used
through web application interface, which is currently a work-in-progress.

In order to build and run the application, please refer to the
[installation](#install) section.

## About

The project is currently in its early stage. However, it comprises the
implementation of our own spectra modelling based on Gaussian Mixture Models,
and Divisive IK-means algorithm for unsupervised segmentation, which can be
used for efficient dataset compression, as well as for knowledge discovery.
Aformentioned algorithms have already been published and refering links
have been enclosed under [references](#references) section.

Also, several classification and clusterization methods will be provided soon,
along with supporting statistics.

## Install

### Spectre service & web application

The Web service is all the time available [here](http://vaei-bit01.aei.polsl.pl/).
If you still would like to host it yourself, please contact us by
[e-mail](mailto:Grzegorz.Mrukwa@polsl.pl). We will provide you with exhaustive
explanation.

## Exemplary usage

### Spectre service & web application

The application is available online [here](http://vaei-bit01.aei.polsl.pl/).

Right now, our web application allows only for an interactive visualization
of some of the data we were using in the research, along with Divisive
Intelligent K-means algorithm results. More features will get documented
when they appear.

## How to contribute?

Please contact us by [e-mail](mailto:Grzegorz.Mrukwa@polsl.pl). We will answer
you in details.

Short contribution guide is actively constructed on
[the project's wiki](https://github.com/spectre-team/spectre/wiki)
as the development progresses.

# Angular

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 1.0.0-rc.2.

## Installation
In order to install Angular CLI, please run `scripts/InstallNodeDev.ps1` using Windows Powershell.
If the script doesn't have permission to run, please execute `Set-ExecutionPolicy Unrestricted` command to enable running scripts from Powershell, run `InstallNodeDev.ps1`, and finish with `Set-ExecutionPolicy Restricted` (default value) if you will.

Troubleshooting:
- If nodejs.msi couldn't have been successfully downloaded and installed, please install it manually and the rest of the script should run just fine.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

If calling `ng serve` causes errors regarding uninstalled packages, please delete `src\Spectre.Angular2Client\node_modules` directory, enter `src\Spectre.Angular2Client\` and run `npm install`.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive/pipe/service/class/module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `-prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).
Before running the tests make sure you are serving the app via `ng serve`.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).

# References

This software is part of contribution made by [Data Mining Group of Silesian
University of Technology](http://www.zaed.polsl.pl/), rest of which is
published [here](https://github.com/ZAEDPolSl).

+ [Marczyk M, Polanska J, Polanski A: Comparison of Algorithms for Profile-Based
Alignment of Low Resolution MALDI-ToF Spectra. In Advances in Intelligent
Systems and Computing, Vol. 242 of Man-Machine Interactions 3, Gruca A,
Czachorski T, Kozielski S, editors. Springer Berlin Heidelberg 2014, p. 193-201
(ISBN: 978-3-319-02308-3), ICMMI 2013, 22-25.10.2013 Brenna, Poland][1]
+ [P. Widlak, G. Mrukwa, M. Kalinowska, M. Pietrowska, M. Chekan, J. Wierzgon, M.
Gawin, G. Drazek and J. Polanska, "Detection of molecular signatures of oral
squamous cell carcinoma and normal epithelium - application of a novel
methodology for unsupervised segmentation of imaging mass spectrometry data,"
Proteomics, vol. 16, no. 11-12, pp. 1613-21, 2016][2]
+ [M. Pietrowska, H. C. Diehl, G. Mrukwa, M. Kalinowska-Herok, M. Gawin, M.
Chekan, J. Elm, G. Drazek, A. Krawczyk, D. Lange, H. E. Meyer, J. Polanska, C.
Henkel, P. Widlak, "Molecular profiles of thyroid cancer subtypes:
Classification based on features of tissue revealed by mass spectrometry
imaging," Biochimica et Biophysica Acta (BBA)-Proteins and Proteomics, 2016][3]
+ [G. Mrukwa, G. Drazek, M. Pietrowska, P. Widlak and J. Polanska, "A Novel
Divisive iK-Means Algorithm with Region-Driven Feature Selection as a Tool for
Automated Detection of Tumour Heterogeneity in MALDI IMS Experiments," in
International Conference on Bioinformatics and Biomedical Engineering, 2016][4]
+ [A. Polanski, M. Marczyk, M. Pietrowska, P. Widlak and J. Polanska, "Signal
partitioning algorithm for highly efficient Gaussian mixture modeling in mass
spectrometry," PloS one, vol. 10, no. 7, p. e0134256, 2015][5]

[1]: http://link.springer.com/chapter/10.1007/978-3-319-02309-0_20
[2]: http://onlinelibrary.wiley.com/doi/10.1002/pmic.201500458/pdf
[3]: http://www.sciencedirect.com/science/article/pii/S1570963916302175
[4]: http://link.springer.com/chapter/10.1007/978-3-319-31744-1_11
[5]: http://journals.plos.org/plosone/article?id=10.1371/journal.pone.0134256
