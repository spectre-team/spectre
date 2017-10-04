/*
 * DivikOptions.cs
 * Configuration struct for DiviK.
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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Spectre.Algorithms.Parameterization
{
    /// <summary>
    /// Options and parameters of DiviK algorithm.
    /// </summary>
    public struct DivikOptions
    {
        #region Fields

        /// <summary>
        /// The maximum number of clusters considered.
        /// </summary>
        public int MaxK;

        /// <summary>
        /// Allowed level of recursion.
        /// </summary>
        public int Level;

        /// <summary>
        /// If true, level of recursion is used as stop condition.
        /// </summary>
        public bool UsingLevels;

        /// <summary>
        /// If true, using amplitude filtration.
        /// </summary>
        public bool UsingAmplitudeFiltration;

        /// <summary>
        /// If true, using variance filtration.
        /// </summary>
        public bool UsingVarianceFiltration;

        /// <summary>
        /// Ratio of subregion to total size used as stop condition if UsingLevels is false.
        /// </summary>
        public double PercentSizeLimit;

        /// <summary>
        /// The feature preservation limit as a rate of initial features number.
        /// </summary>
        public double FeaturePreservationLimit;

        /// <summary>
        /// The metric used during clustering.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Metric Metric;

        /// <summary>
        /// If true, partitions are plotted.
        /// </summary>
        public bool PlottingPartitions;

        /// <summary>
        /// If true, partitions are plotted recursively.
        /// </summary>
        public bool PlottingRecursively;

        /// <summary>
        /// If true, decomposition plots are created.
        /// </summary>
        public bool PlottingDecomposition;

        /// <summary>
        /// If true, decomposition plots are created recursively.
        /// </summary>
        public bool PlottingDecompositionRecursively;

        /// <summary>
        /// The maximum number of components allowed for decomposition.
        /// </summary>
        public int MaxComponentsForDecomposition;

        /// <summary>
        /// The output path for images.
        /// </summary>
        public string OutputPath;

        /// <summary>
        /// The output path for partial results.
        /// </summary>
        public string CachePath;

        /// <summary>
        /// If true, partial results are stored on disk.
        /// </summary>
        public bool Caching;

        /// <summary>
        /// If true, additional informations are printed to stdout.
        /// </summary>
        public bool Verbose;

        /// <summary>
        /// Limit of iterations in K-means algorithm.
        /// </summary>
        public int KmeansMaxIters;

        #endregion

        #region Factories

        /// <summary>
        /// Produces default config used to find groups in MSI data sets.
        /// </summary>
        /// <returns>Default config.</returns>
        public static DivikOptions Default()
        {
            DivikOptions options;
            options.MaxK = 10;
            options.Level = 3;
            options.UsingLevels = true;
            options.UsingAmplitudeFiltration = true;
            options.UsingVarianceFiltration = true;
            options.PercentSizeLimit = 0.001;
            options.FeaturePreservationLimit = 0.05;
            options.Metric = Metric.Pearson;
            options.PlottingPartitions = false;
            options.PlottingRecursively = false;
            options.PlottingDecomposition = false;
            options.PlottingDecompositionRecursively = false;
            options.MaxComponentsForDecomposition = 10;
            options.OutputPath = ".";
            options.CachePath = ".";
            options.Caching = false;
            options.Verbose = false;
            options.KmeansMaxIters = 100;
            return options;
        }

        /// <summary>
        /// Produces default config for recursion level stop condition.
        /// </summary>
        /// <param name="levels">The number levels.</param>
        /// <returns>Adjusted config.</returns>
        public static DivikOptions ForLevels(int levels)
        {
            var opts = DivikOptions.Default();
            opts.Level = levels;
            return opts;
        }

        /// <summary>
        /// Produces default config for size ratio stop condition.
        /// </summary>
        /// <param name="sizeRatioLimit">The size ratio limit.</param>
        /// <returns>Adjusted config.</returns>
        public static DivikOptions ForSize(double sizeRatioLimit)
        {
            var opts = DivikOptions.Default();
            opts.UsingLevels = false;
            opts.PercentSizeLimit = sizeRatioLimit;
            return opts;
        }

        #endregion

        #region MATLAB interface

        /// <summary>
        /// Dumps config to varargin readable by MATLAB.
        /// </summary>
        /// <returns>MATLAB varargin (cell).</returns>
        public object[] ToVarargin() // made public only for migration purposes!
        {
            var varargin = new List<object>();
            Action<string, object> addParam = (s, o) => varargin.AddRange(collection: new[] { s, o });
            addParam(arg1: "MaxK", arg2: (double)MaxK);
            addParam(arg1: "Level", arg2: Level);
            addParam(arg1: "UseLevels", arg2: UsingLevels);
            addParam(arg1: "AmplitudeFiltration", arg2: UsingAmplitudeFiltration);
            addParam(arg1: "VarianceFiltration", arg2: UsingVarianceFiltration);
            addParam(arg1: "PercentSizeLimit", arg2: PercentSizeLimit);
            addParam(arg1: "FeaturePreservationLimit", arg2: FeaturePreservationLimit);
            addParam(arg1: "Metric", arg2: Metric.ToString().ToLower());
            addParam(arg1: "PlotPartitions", arg2: PlottingPartitions);
            addParam(arg1: "PlotRecursively", arg2: PlottingRecursively);
            addParam(arg1: "DecompositionPlots", arg2: PlottingDecomposition);
            addParam(arg1: "DecompositionPlotsRecursively", arg2: PlottingDecompositionRecursively);
            addParam(arg1: "MaxComponentsForDecomposition", arg2: (double)MaxComponentsForDecomposition);
            addParam(arg1: "OutPath", arg2: OutputPath);
            addParam(arg1: "CachePath", arg2: CachePath);
            addParam(arg1: "Cache", arg2: Caching);
            addParam(arg1: "Verbose", arg2: Verbose);
            addParam(arg1: "KmeansMaxIters", arg2: (double)KmeansMaxIters);
            return varargin.ToArray();
        }

        #endregion
    }
}
