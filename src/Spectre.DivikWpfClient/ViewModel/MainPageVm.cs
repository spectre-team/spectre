/*
 * MainPageVm.cs
 * Contains ViewModel for MainWindow of WPF Divik client.
 * 
   Copyright 2017 Grzegorz Mrukwa

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
using System.Windows.Forms;
using Spectre.Mvvm.Base;
using Spectre.Algorithms.Parameterization;
using Spectre.Service.Abstract;
using Spectre.Data.Datasets;
using System.Threading;

namespace Spectre.DivikWpfClient.ViewModel
{
    /// <summary>
    /// Serves as VM for main page.
    /// </summary>
    /// <seealso cref="Spectre.Mvvm.Base.PropertyChangedNotification" />
    public class MainPageVm: PropertyChangedNotification
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageVm"/> class
        /// with default properties set.
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
            MaxComponentsForDecomposition = 3;
            OutputPath = ".";
            CachePath = ".";
            Caching = false;
            KmeansMaxIters = 100;
            IsProgressBarVisible = false;
            ProgressBarLabel = "";
            StartDivikLabel = "Start Divik";
            PrettyPrint = true;
            _CancellationTokenSource = new CancellationTokenSource();
            EnableStartDivik = true;
        }

        #endregion

        #region DivikProperties

        /// <summary>
		/// ViewModel property for path of input file.
		/// </summary>
        public string InputPath
        {
            get { return GetValue(() => InputPath); }
            set { SetValue(() => InputPath, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.MaxK"/>.
		/// </summary>
        public int MaxK
        {
            get { return GetValue(() => MaxK); }
            set { SetValue(() => MaxK, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.Level"/>.
		/// </summary>
        public int Level
        {
            get { return GetValue(() => Level); }
            set { SetValue(() => Level, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.UsingLevels"/>.
		/// </summary>
        public bool UsingLevels
        {
            get { return GetValue(() => UsingLevels); }
            set { SetValue(() => UsingLevels, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.UsingAmplitudeFiltration"/>.
		/// </summary>
        public bool UsingAmplitudeFiltration
        {
            get { return GetValue(() => UsingAmplitudeFiltration); }
            set { SetValue(() => UsingAmplitudeFiltration, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.UsingVarianceFiltration"/>.
		/// </summary>
        public bool UsingVarianceFiltration
        {
            get { return GetValue(() => UsingVarianceFiltration); }
            set { SetValue(() => UsingVarianceFiltration, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.PercentSizeLimit"/>.
		/// </summary>
        public double PercentSizeLimit
        {
            get { return GetValue(() => PercentSizeLimit); }
            set { SetValue(() => PercentSizeLimit, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.FeaturePreservationLimit"/>.
		/// </summary>
        public double FeaturePreservationLimit
        {
            get { return GetValue(() => FeaturePreservationLimit); }
            set { SetValue(() => FeaturePreservationLimit, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.Metric"/>.
		/// </summary>
        public Metric Metric
        {
            get { return GetValue(() => Metric); }
            set { SetValue(() => Metric, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.PlottingPartitions"/>.
		/// </summary>
        public bool PlottingPartitions
        {
            get { return GetValue(() => PlottingPartitions); }
            set { SetValue(() => PlottingPartitions, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.PlottingRecursively"/>.
		/// </summary>
        public bool PlottingRecursively
        {
            get { return GetValue(() => PlottingRecursively); }
            set { SetValue(() => PlottingRecursively, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.PlottingDecomposition"/>.
		/// </summary>
        public bool PlottingDecomposition
        {
            get { return GetValue(() => PlottingDecomposition); }
            set { SetValue(() => PlottingDecomposition, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.PlottingDecompositionRecursively"/>.
		/// </summary>
        public bool PlottingDecompositionRecursively
        {
            get { return GetValue(() => PlottingDecompositionRecursively); }
            set { SetValue(() => PlottingDecompositionRecursively, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.MaxComponentsForDecomposition"/>.
		/// </summary>
        public int MaxComponentsForDecomposition
        {
            get { return GetValue(() => MaxComponentsForDecomposition); }
            set { SetValue(() => MaxComponentsForDecomposition, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.OutputPath"/>.
		/// </summary>
        public string OutputPath
        {
            get { return GetValue(() => OutputPath); }
            set { SetValue(() => OutputPath, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.CachePath"/>.
		/// </summary>
        public string CachePath
        {
            get { return GetValue(() => CachePath); }
            set { SetValue(() => CachePath, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.Caching"/>.
		/// </summary>
        public bool Caching
        {
            get { return GetValue(() => Caching); }
            set { SetValue(() => Caching, value); }
        }

        /// <summary>
		/// ViewModel property for <see cref="DivikOptions.KmeansMaxIters"/>.
		/// </summary>
        public int KmeansMaxIters
        {
            get { return GetValue(() => KmeansMaxIters); }
            set { SetValue(() => KmeansMaxIters, value); }
        }

        /// <summary>
		/// <see cref="IServiceFactory"/> for getting divik service.
		/// </summary>
        public IServiceFactory ServiceFactory { get; set; }

        /// <summary>
		/// Private singleton of <see cref="IDivikService"/>.
		/// </summary>
        private IDivikService _DivikService;

        #endregion

        #region DisplayProperties

        /// <summary>
		/// ViewModel property for setting visibility of <see cref="MainWindow.DivikProgress"/>.
		/// </summary>
        public bool IsProgressBarVisible
        {
            get { return GetValue(() => IsProgressBarVisible); }
            set { SetValue(() => IsProgressBarVisible, value); }
        }

        /// <summary>
		/// ViewModel property for setting text in <see cref="MainWindow.DivikProgressLabel"/>.
		/// </summary>
        public string ProgressBarLabel
        {
            get { return GetValue(() => ProgressBarLabel); }
            set { SetValue(() => ProgressBarLabel, value); }
        }

        /// <summary>
		/// ViewModel property for populating <see cref="MainWindow.MetricComboBox"/>.
		/// </summary>
        public IEnumerable<Metric> Metrics
        {
            get { return Enum.GetValues(typeof(Metric)).Cast<Metric>(); }
        }

        /// <summary>
		/// ViewModel property for setting text in <see cref="MainWindow.StartDivikButton"/>.
		/// </summary>
        public string StartDivikLabel
        {
            get { return GetValue(() => StartDivikLabel); }
            set { SetValue(() => StartDivikLabel, value); }
        }

        /// <summary>
		/// ViewModel property for setting indentation of <see cref="DivikResult.Save"/> method.
		/// </summary>
        public bool PrettyPrint
        {
            get { return GetValue(() => PrettyPrint); }
            set { SetValue(() => PrettyPrint, value); }
        }

        /// <summary>
		/// ViewModel property for enabling <see cref="MainWindow.StartDivikButton"/>.
		/// </summary>
        public bool EnableStartDivik
        {
            get { return GetValue(() => EnableStartDivik); }
            set { SetValue(() => EnableStartDivik, value); }
        }

        #endregion

        #region Privates

        /// <summary>
		/// Returns <see cref="IDataset"/> created from file under the input path.
		/// </summary>
        private IDataset _GetDatasetFromVm()
        {
            return new BasicTextDataset(InputPath);
        }

        /// <summary>
		/// Returns <see cref="DivikOptions"/> created from vm properties.
		/// </summary>
        private DivikOptions _GetDivikOptionsFromVm()
        {
            return new DivikOptions {
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

        /// <summary>
		/// Toggles start button label, divik progress bar visibility and divik label.
		/// </summary>
        private void _ToggleDivikProgressDisplay()
        {
            StartDivikLabel = StartDivikLabel == "Start Divik" ? "Cancel" : "Start Divik";
            IsProgressBarVisible = !IsProgressBarVisible;
            ProgressBarLabel = ProgressBarLabel == "" ? "Divik running..." : "";
        }

        /// <summary>
		/// Cancellation token source for divik calculation task.
		/// </summary>
        private CancellationTokenSource _CancellationTokenSource;

        #endregion

        #region Commands

        #region ChooseFileButton
        /// <summary>
        /// Function for picking InputPath with a file picker.
        /// </summary>
        private void _ChooseFileButtonExecute()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog()
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

        /// <summary>
		/// Handle for <see cref="MainWindow.ChooseFileButton"/>.
		/// </summary>
        private RelayCommand _ChooseFileButtonHandle;

        /// <summary>
		/// Command for <see cref="MainWindow.ChooseFileButton"/>.
        /// Executes <see cref="MainPageVm._ChooseFileButtonExecute"/>.
		/// </summary>
        public RelayCommand ChooseFileButtonExecute
        {
            get
            {
                return _ChooseFileButtonHandle ?? (_ChooseFileButtonHandle = new RelayCommand(execute: _ChooseFileButtonExecute));
            }
        }
        #endregion

        #region ChooseOutDirButton

        /// <summary>
        /// The handle for ChooseOutputDirectoryCommand.
        /// </summary>
        private RelayCommand _chooseOutputDirectoryCommand;

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>
        /// The command for choosing directory.
        /// </value>
        public RelayCommand ChooseOutputDirectoryCommand => _chooseOutputDirectoryCommand ?? (
                                                                _chooseOutputDirectoryCommand =
                                                                    new RelayCommand(
                                                                        () =>
                                                                            OutputPath =
                                                                                _ChooseDirectory() ?? OutputPath));
        #endregion

        #region ChooseCacheDirButton
        /// <summary>
        /// The command choosing cache directory 
        /// </summary>
        private RelayCommand _chooseCacheDirectoryCommand;

        /// <summary>
        /// Gets the command choosing cache directory.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public RelayCommand ChooseCacheDirectoryCommand => _chooseCacheDirectoryCommand ?? (
                                                               _chooseCacheDirectoryCommand =
                                                                   new RelayCommand(
                                                                       () => CachePath = _ChooseDirectory() ?? CachePath));
        #endregion

        #region ChooseDirectory
        /// <summary>
        /// Chooses the directory.
        /// </summary>
        private string _ChooseDirectory()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                var result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    return dialog.SelectedPath;
                }
            }
            return null;
        }
        #endregion

        #region StartDivik
        /// <summary>
        /// Function for starting divik calculation.
        /// </summary>
        private void _StartDivikButtonExecute()
        {
            if (StartDivikLabel == "Start Divik")
            {
                ThreadPool.QueueUserWorkItem(s =>
                {
                    _ToggleDivikProgressDisplay();
                    CancellationToken token = (CancellationToken) s;
                    if (token.IsCancellationRequested)
                    {
                        EnableStartDivik = true;
                        _ToggleDivikProgressDisplay();
                        MessageBox.Show("Divik was successfully cancelled.", "Cancelled!");
                        return;
                    }  
                    _DivikService = _DivikService ?? ServiceFactory.GetDivikService;
                    var fileName = "\\divik-result-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json";
                    if (token.IsCancellationRequested)
                    {
                        EnableStartDivik = true;
                        _ToggleDivikProgressDisplay();
                        MessageBox.Show("Divik was successfully cancelled.", "Cancelled!");
                        return;
                    }
                    _DivikService.CalculateDivik(_GetDatasetFromVm(), _GetDivikOptionsFromVm()).Save(OutputPath + fileName, PrettyPrint);
                    _ToggleDivikProgressDisplay();
                    MessageBox.Show("Divik was successfully calculated. The result file was saved as: " + OutputPath + fileName, "Success!");

                }, _CancellationTokenSource.Token);
            } else
            {
                EnableStartDivik = false;
                ProgressBarLabel = "Cancelling divik...";
                _CancellationTokenSource.Cancel();
            }
        }

        /// <summary>
		/// Handle for <see cref="MainWindow.StartDivikButton"/>.
		/// </summary>
        private RelayCommand _StartDivikButtonHandle;

        /// <summary>
		/// Command for <see cref="MainWindow.StartDivikButton"/>.
        /// Executes <see cref="MainPageVm._StartDivikButtonExecute"/>.
		/// </summary>
        public RelayCommand StartDivikButtonExecute
        {
            get
            {
                return _StartDivikButtonHandle ?? (_StartDivikButtonHandle = new RelayCommand(execute: () => _StartDivikButtonExecute()));
            }
        }
        #endregion

        #endregion
    }
}
