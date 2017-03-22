using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Mvvm.Base;

namespace Spectre.DivikWpfClient.ViewModel
{
    /// <summary>
    /// Serves as VM for main page.
    /// </summary>
    /// <seealso cref="Spectre.Mvvm.Base.PropertyChangedNotification" />
    public class MainPageVm: PropertyChangedNotification
    {
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

        //metric 

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
    }
}
