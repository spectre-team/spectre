using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Mvvm.Base;
using Spectre.Algorithms.Parameterization;

namespace Spectre.DivikWpfClient.ViewModel
{
    /// <summary>
    /// Serves as VM for main page.
    /// </summary>
    /// <seealso cref="Spectre.Mvvm.Base.PropertyChangedNotification" />
    public class MainPageVm: PropertyChangedNotification
    {
        
        #region Constructors

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
            Verbose = false;
            KmeansMaxIters = 100;
            IsProgressBarVisible = false;
            ProgressBarLabel = "";
        }

        #endregion

        #region DivikProperties

        public string InputPath
        {
            get { return GetValue(() => InputPath); }
            set { SetValue(() => InputPath, value); }
        }

        public int MaxK
        {
            get { return GetValue(() => MaxK); }
            set { SetValue(() => MaxK, value); }
        }

        public int Level
        {
            get { return GetValue(() => Level); }
            set { SetValue(() => Level, value); }
        }

        public bool UsingLevels
        {
            get { return GetValue(() => UsingLevels); }
            set { SetValue(() => UsingLevels, value); }
        }

        public bool UsingAmplitudeFiltration
        {
            get { return GetValue(() => UsingAmplitudeFiltration); }
            set { SetValue(() => UsingAmplitudeFiltration, value); }
        }

        public bool UsingVarianceFiltration
        {
            get { return GetValue(() => UsingVarianceFiltration); }
            set { SetValue(() => UsingVarianceFiltration, value); }
        }

        public double PercentSizeLimit
        {
            get { return GetValue(() => PercentSizeLimit); }
            set { SetValue(() => PercentSizeLimit, value); }
        }

        public double FeaturePreservationLimit
        {
            get { return GetValue(() => FeaturePreservationLimit); }
            set { SetValue(() => FeaturePreservationLimit, value); }
        }

        public Metric Metric
        {
            get { return GetValue(() => Metric); }
            set { SetValue(() => Metric, value); }
        }

        public bool PlottingPartitions
        {
            get { return GetValue(() => PlottingPartitions); }
            set { SetValue(() => PlottingPartitions, value); }
        }

        public bool PlottingRecursively
        {
            get { return GetValue(() => PlottingRecursively); }
            set { SetValue(() => PlottingRecursively, value); }
        }

        public bool PlottingDecomposition
        {
            get { return GetValue(() => PlottingDecomposition); }
            set { SetValue(() => PlottingDecomposition, value); }
        }

        public bool PlottingDecompositionRecursively
        {
            get { return GetValue(() => PlottingDecompositionRecursively); }
            set { SetValue(() => PlottingDecompositionRecursively, value); }
        }

        public int MaxComponentsForDecomposition
        {
            get { return GetValue(() => MaxComponentsForDecomposition); }
            set { SetValue(() => MaxComponentsForDecomposition, value); }
        }

        public string OutputPath
        {
            get { return GetValue(() => OutputPath); }
            set { SetValue(() => OutputPath, value); }
        }

        public string CachePath
        {
            get { return GetValue(() => CachePath); }
            set { SetValue(() => CachePath, value); }
        }

        public bool Caching
        {
            get { return GetValue(() => Caching); }
            set { SetValue(() => Caching, value); }
        }

        public bool Verbose
        {
            get { return GetValue(() => Verbose); }
            set { SetValue(() => Verbose, value); }
        }

        public int KmeansMaxIters
        {
            get { return GetValue(() => KmeansMaxIters); }
            set { SetValue(() => KmeansMaxIters, value); }
        }

        #endregion

        #region DisplayProperties

        public bool IsProgressBarVisible
        {
            get { return GetValue(() => IsProgressBarVisible); }
            set { SetValue(() => IsProgressBarVisible, value); }
        }

        public string ProgressBarLabel
        {
            get { return GetValue(() => ProgressBarLabel); }
            set { SetValue(() => ProgressBarLabel, value); }
        }

        public IEnumerable<Metric> Metrics
        {
            get { return Enum.GetValues(typeof(Metric)).Cast<Metric>(); }
        }

        #endregion

        #region Commands


        //choose file
        private bool _ChooseFileButtonCanExecute()
        {
            return true;
        }

        private void _ChooseFileButtonExecute() {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog()
            {
                DefaultExt = ".txt",
                Filter = "Txt Files (*.txt)|*.txt"
            };

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                InputPath = filename;
            }
        }

        public RelayCommand ChooseFileButtonExecute => new RelayCommand(
                    execute: () => _ChooseFileButtonExecute(),
                    canExecute: () => _ChooseFileButtonCanExecute()
                    );

        //start divik
        private bool _StartDivikButtonCanExecute()
        {
            return true;
        }

        private void _StartDivikButtonExecute()
        {
            IsProgressBarVisible = !IsProgressBarVisible;
            ProgressBarLabel = ProgressBarLabel == "" ? "Divik running..." : "";
        }

        public RelayCommand StartDivikButtonExecute => new RelayCommand(
                    execute: () => _StartDivikButtonExecute(),
                    canExecute: () => _StartDivikButtonCanExecute()
                    );

        #endregion
    }
}
