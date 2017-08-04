/*
 * MainPageVm.cs
 * Contains ViewModel for MainWindow of WPF Divik client.
 *
   Copyright 2017 Michal Wolny, Grzegorz Mrukwa

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Spectre.Algorithms.Parameterization;
using Spectre.Algorithms.Results;
using Spectre.Data.Datasets;
using Spectre.DivikWpfClient;
using Spectre.Mvvm.Base;
using Spectre.Service;
using Spectre.Service.Abstract;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Spectre.DivikWpfClient.ViewModel
{
    /// <summary>
    ///     Serves as VM for main page.
    /// </summary>
    /// <seealso cref="Spectre.Mvvm.Base.PropertyChangedNotification" />
    public class MainPageVm : PropertyChangedNotification
    {
        #region Fields

        /// <summary>
        ///     The handle for ChooseOutputDirectoryCommand.
        /// </summary>
        private RelayCommand _chooseOutputDirectoryCommand;

        /// <summary>
        ///     Handle for <see cref="MainWindow.ChooseFileButton" />.
        /// </summary>
        private RelayCommand _chooseFileButtonHandle;

        /// <summary>
        ///     The command choosing cache directory
        /// </summary>
        private RelayCommand _chooseCacheDirectoryCommand;

        /// <summary>
        ///     Handle for <see cref="MainWindow.StartDivikButton" />.
        /// </summary>
        private RelayCommand _startDivikButtonHandle;

        /// <summary>
        ///     Handle for MainWindow.Closing
        /// </summary>
        private RelayCommand _windowCloseHandle;

        /// <summary>
        ///     Private singleton of <see cref="IDivikService" />.
        /// </summary>
        private IDivikService _divikService;

        /// <summary>
        ///     Cancellation token source for divik calculation task.
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        ///     The command handling exception
        /// </summary>
        private RelayCommand _handleExceptionCommand;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPageVm" /> class
        ///     with default properties set.
        /// </summary>
        public MainPageVm()
        {
            InputPath = null;
            MaxK = 10;
            Level = 3;
            UsingLevels = true;
            UsingAmplitudeFiltration = true;
            UsingVarianceFiltration = true;
            PercentSizeLimit = 0.001;
            FeaturePreservationLimit = 0.05;
            Metric = Metric.Pearson;
            PlottingPartitions = false;
            PlottingRecursively = false;
            PlottingDecomposition = false;
            PlottingDecompositionRecursively = false;
            MaxComponentsForDecomposition = 10;
            OutputPath = ".";
            CachePath = ".";
            Caching = false;
            KmeansMaxIters = 100;
            IsProgressBarVisible = false;
            ProgressBarLabel = string.Empty;
            StartDivikLabel = "Start Divik";
            PrettyPrint = true;
            _cancellationTokenSource = new CancellationTokenSource();
            EnableStartDivik = true;
            Log = string.Empty;
        }

        #endregion

        #region DivikProperties

        /// <summary>
        ///     ViewModel property for path of input file.
        /// </summary>
        public string InputPath
        {
            get { return GetValue(propertySelector: () => InputPath); }
            set { SetValue(propertySelector: () => InputPath, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.MaxK" />.
        /// </summary>
        public int MaxK
        {
            get { return GetValue(propertySelector: () => MaxK); }
            set { SetValue(propertySelector: () => MaxK, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.Level" />.
        /// </summary>
        public int Level
        {
            get { return GetValue(propertySelector: () => Level); }
            set { SetValue(propertySelector: () => Level, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.UsingLevels" />.
        /// </summary>
        public bool UsingLevels
        {
            get { return GetValue(propertySelector: () => UsingLevels); }
            set { SetValue(propertySelector: () => UsingLevels, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.UsingAmplitudeFiltration" />.
        /// </summary>
        public bool UsingAmplitudeFiltration
        {
            get { return GetValue(propertySelector: () => UsingAmplitudeFiltration); }
            set { SetValue(propertySelector: () => UsingAmplitudeFiltration, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.UsingVarianceFiltration" />.
        /// </summary>
        public bool UsingVarianceFiltration
        {
            get { return GetValue(propertySelector: () => UsingVarianceFiltration); }
            set { SetValue(propertySelector: () => UsingVarianceFiltration, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.PercentSizeLimit" />.
        /// </summary>
        public double PercentSizeLimit
        {
            get { return GetValue(propertySelector: () => PercentSizeLimit); }
            set { SetValue(propertySelector: () => PercentSizeLimit, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.FeaturePreservationLimit" />.
        /// </summary>
        public double FeaturePreservationLimit
        {
            get { return GetValue(propertySelector: () => FeaturePreservationLimit); }
            set { SetValue(propertySelector: () => FeaturePreservationLimit, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.Metric" />.
        /// </summary>
        public Metric Metric
        {
            get { return GetValue(propertySelector: () => Metric); }
            set { SetValue(propertySelector: () => Metric, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.PlottingPartitions" />.
        /// </summary>
        public bool PlottingPartitions
        {
            get { return GetValue(propertySelector: () => PlottingPartitions); }
            set { SetValue(propertySelector: () => PlottingPartitions, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.PlottingRecursively" />.
        /// </summary>
        public bool PlottingRecursively
        {
            get { return GetValue(propertySelector: () => PlottingRecursively); }
            set { SetValue(propertySelector: () => PlottingRecursively, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.PlottingDecomposition" />.
        /// </summary>
        public bool PlottingDecomposition
        {
            get { return GetValue(propertySelector: () => PlottingDecomposition); }
            set { SetValue(propertySelector: () => PlottingDecomposition, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.PlottingDecompositionRecursively" />.
        /// </summary>
        public bool PlottingDecompositionRecursively
        {
            get { return GetValue(propertySelector: () => PlottingDecompositionRecursively); }
            set { SetValue(propertySelector: () => PlottingDecompositionRecursively, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.MaxComponentsForDecomposition" />.
        /// </summary>
        public int MaxComponentsForDecomposition
        {
            get { return GetValue(propertySelector: () => MaxComponentsForDecomposition); }
            set { SetValue(propertySelector: () => MaxComponentsForDecomposition, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.OutputPath" />.
        /// </summary>
        public string OutputPath
        {
            get { return GetValue(propertySelector: () => OutputPath); }
            set { SetValue(propertySelector: () => OutputPath, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.CachePath" />.
        /// </summary>
        public string CachePath
        {
            get { return GetValue(propertySelector: () => CachePath); }
            set { SetValue(propertySelector: () => CachePath, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.Caching" />.
        /// </summary>
        public bool Caching
        {
            get { return GetValue(propertySelector: () => Caching); }
            set { SetValue(propertySelector: () => Caching, value: value); }
        }

        /// <summary>
        ///     ViewModel property for <see cref="DivikOptions.KmeansMaxIters" />.
        /// </summary>
        public int KmeansMaxIters
        {
            get { return GetValue(propertySelector: () => KmeansMaxIters); }
            set { SetValue(propertySelector: () => KmeansMaxIters, value: value); }
        }

        /// <summary>
        ///     <see cref="IServiceFactory" /> for getting divik service.
        /// </summary>
        public IServiceFactory ServiceFactory { get; set; }

        #endregion

        #region DisplayProperties

        /// <summary>
        ///     ViewModel property for setting visibility of <see cref="MainWindow.DivikProgress" />.
        /// </summary>
        public bool IsProgressBarVisible
        {
            get { return GetValue(propertySelector: () => IsProgressBarVisible); }
            set { SetValue(propertySelector: () => IsProgressBarVisible, value: value); }
        }

        /// <summary>
        ///     ViewModel property for setting text in <see cref="MainWindow.DivikProgressLabel" />.
        /// </summary>
        public string ProgressBarLabel
        {
            get { return GetValue(propertySelector: () => ProgressBarLabel); }
            set { SetValue(propertySelector: () => ProgressBarLabel, value: value); }
        }

        /// <summary>
        ///     ViewModel property for populating <see cref="MainWindow.MetricComboBox" />.
        /// </summary>
        public IEnumerable<Metric> Metrics
        {
            get
            {
                return Enum.GetValues(enumType: typeof(Metric))
                    .Cast<Metric>();
            }
        }

        /// <summary>
        ///     ViewModel property for setting text in <see cref="MainWindow.StartDivikButton" />.
        /// </summary>
        public string StartDivikLabel
        {
            get { return GetValue(propertySelector: () => StartDivikLabel); }
            set { SetValue(propertySelector: () => StartDivikLabel, value: value); }
        }

        /// <summary>
        ///     ViewModel property for setting indentation of <see cref="DivikResult.Save" /> method.
        /// </summary>
        public bool PrettyPrint
        {
            get { return GetValue(propertySelector: () => PrettyPrint); }
            set { SetValue(propertySelector: () => PrettyPrint, value: value); }
        }

        /// <summary>
        ///     ViewModel property for enabling <see cref="MainWindow.StartDivikButton" />.
        /// </summary>
        public bool EnableStartDivik
        {
            get { return GetValue(propertySelector: () => EnableStartDivik); }
            set { SetValue(propertySelector: () => EnableStartDivik, value: value); }
        }

        /// <summary>
        ///     Gets the MATLAB log.
        /// </summary>
        /// <value>
        ///     The output log.
        /// </value>
        public string Log
        {
            get { return GetValue(propertySelector: () => Log); }
            private set { SetValue(propertySelector: () => Log, value: value); }
        }

        #endregion

        #region Commands

        #region ChooseFileButton

        /// <summary>
        ///     Command for <see cref="MainWindow.ChooseFileButton" />.
        ///     Executes <see cref="ChooseFileButtonExecute" />.
        /// </summary>
        public RelayCommand ChooseFile
        {
            get
            {
                return _chooseFileButtonHandle
                       ?? (_chooseFileButtonHandle =
                           new RelayCommand(ChooseFileButtonExecute));
            }
        }

        #endregion

        #region ChooseOutDirButton

        /// <summary>
        ///     Gets the command.
        /// </summary>
        /// <value>
        ///     The command for choosing directory.
        /// </value>
        public RelayCommand ChooseOutputDirectoryCommand
        {
            get
            {
                return _chooseOutputDirectoryCommand
                       ?? (
                           _chooseOutputDirectoryCommand =
                               new RelayCommand(
                                   execute: () =>
                                       OutputPath =
                                           ChooseDirectory() ?? OutputPath));
            }
        }

        #endregion

        #region ChooseCacheDirButton

        /// <summary>
        ///     Gets the command choosing cache directory.
        /// </summary>
        /// <value>
        ///     The command.
        /// </value>
        public RelayCommand ChooseCacheDirectoryCommand
        {
            get
            {
                return _chooseCacheDirectoryCommand
                       ?? (
                           _chooseCacheDirectoryCommand =
                               new RelayCommand(
                                   execute: () => CachePath =
                                       ChooseDirectory() ?? CachePath));
            }
        }

        #endregion

        #region StartDivik

        /// <summary>
        ///     Command for <see cref="MainWindow.StartDivikButton" />.
        ///     Executes <see cref="StartDivikButtonExecute" />.
        /// </summary>
        public RelayCommand StartDivik
        {
            get
            {
                return _startDivikButtonHandle
                       ?? (_startDivikButtonHandle = new RelayCommand(execute: () => StartDivikButtonExecute()));
            }
        }

        #endregion

        #region WindowClose

        /// <summary>
        ///     Command for MainWindow.Closing
        ///     Executes <see cref="WindowCloseExecute" />.
        /// </summary>
        public RelayCommand WindowCloseExecute
        {
            get
            {
                return _windowCloseHandle
                       ?? (_windowCloseHandle =
                           new RelayCommand(execute: () => (_divikService as IDisposable)?.Dispose()));
            }
        }

        #endregion

        #region HandleExceptionCommand

        /// <summary>
        ///     Gets the command handling exception .
        /// </summary>
        /// <value>
        ///     The command.
        /// </value>
        public RelayCommand HandleExceptionCommand
        {
            get
            {
                return _handleExceptionCommand
                       ?? (_handleExceptionCommand = new RelayCommand(execute: s => Log += (string)s));
            }
        }

        #endregion

        #region DiviK starter

        /// <summary>
        ///     Function for starting divik calculation.
        /// </summary>
        private void StartDivikButtonExecute()
        {
            if (StartDivikLabel == "Start Divik")
            {
                ThreadPool.QueueUserWorkItem(
                    callBack: s =>
                    {
                        ToggleDivikProgressDisplay();
                        var token = (CancellationToken)s;
                        if (token.IsCancellationRequested)
                        {
                            EnableStartDivik = true;
                            ToggleDivikProgressDisplay();
                            MessageBox.Show(text: "Divik was successfully cancelled.", caption: "Cancelled!");
                            return;
                        }
                        _divikService = _divikService ?? new Service.ServiceFactory().GetDivikService();
                        var fileName = "\\divik-result-"
                                       + DateTime.Now.ToString(format: "yyyy-MM-dd-HH-mm-ss")
                                       + ".json";
                        if (token.IsCancellationRequested)
                        {
                            EnableStartDivik = true;
                            ToggleDivikProgressDisplay();
                            MessageBox.Show(text: "Divik was successfully cancelled.", caption: "Cancelled!");
                            return;
                        }
                        using (var consoleCapture = new ConsoleCaptureService(updateInterval: 1000))
                        {
                            Log = string.Empty;
                            consoleCapture.Written += (sender, appended) => Log += appended;
                            _divikService.CalculateDivik(
                                    dataset: GetDatasetFromVm(),
                                    options: GetDivikOptionsFromVm())
                                .Save(path: OutputPath + fileName, indentation: PrettyPrint);
                        }
                        ToggleDivikProgressDisplay();
                        MessageBox.Show(
                            text: "Divik was successfully calculated. The result file was saved as: "
                                  + OutputPath
                                  + fileName,
                            caption: "Success!");
                    },
                    state: _cancellationTokenSource.Token);
            }
            else
            {
                EnableStartDivik = false;
                ProgressBarLabel = "Cancelling divik...";
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                (_divikService as IDisposable)?.Dispose();
                _divikService = null;
            }
        }

        #endregion

        #region ChooseDirectory

        /// <summary>
        ///     Chooses the directory.
        /// </summary>
        /// <returns>A directory</returns>
        private string ChooseDirectory()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                var result = dialog.ShowDialog();

                if ((result == DialogResult.OK) && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    return dialog.SelectedPath;
                }
            }
            return null;
        }

        #endregion

        #region File chooser

        /// <summary>
        ///     Function for picking InputPath with a file picker.
        /// </summary>
        private void ChooseFileButtonExecute()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Txt Files (*.txt)|*.txt"
            };

            var result = dlg.ShowDialog();

            if (result == true)
            {
                InputPath = dlg.FileName;
            }
        }

        #endregion

        #region Progress bar display

        /// <summary>
        ///     Toggles start button label, divik progress bar visibility and divik label.
        /// </summary>
        private void ToggleDivikProgressDisplay()
        {
            StartDivikLabel = StartDivikLabel == "Start Divik" ? "Cancel" : "Start Divik";
            IsProgressBarVisible = !IsProgressBarVisible;
            ProgressBarLabel = ProgressBarLabel == string.Empty ? "Divik running..." : string.Empty;
        }

        #endregion

        #region Privates

        /// <summary>
        ///     Returns <see cref="IDataset" /> created from file under the input path.
        /// </summary>
        /// <returns>Dataset</returns>
        private IDataset GetDatasetFromVm()
        {
            return new BasicTextDataset(InputPath);
        }

        /// <summary>
        ///     Returns <see cref="DivikOptions" /> created from vm properties.
        /// </summary>
        /// <returns>Options for DiviK algorithm</returns>
        private DivikOptions GetDivikOptionsFromVm()
        {
            return new DivikOptions
            {
                MaxK = MaxK,
                Level = Level,
                CachePath = CachePath,
                Caching = Caching,
                FeaturePreservationLimit = FeaturePreservationLimit,
                KmeansMaxIters = KmeansMaxIters,
                MaxComponentsForDecomposition = MaxComponentsForDecomposition,
                Metric = Metric,
                OutputPath = OutputPath,
                PercentSizeLimit = PercentSizeLimit,
                PlottingDecomposition = PlottingDecomposition,
                PlottingDecompositionRecursively = PlottingDecompositionRecursively,
                PlottingPartitions = PlottingPartitions,
                PlottingRecursively = PlottingRecursively,
                UsingAmplitudeFiltration = UsingAmplitudeFiltration,
                UsingLevels = UsingLevels,
                UsingVarianceFiltration = UsingVarianceFiltration,
                Verbose = false
            };
        }

        #endregion

        #endregion
    }
}
