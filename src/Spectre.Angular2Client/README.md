# Spectre.Angular2Client

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
