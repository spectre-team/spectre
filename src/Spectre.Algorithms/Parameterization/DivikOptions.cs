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
			var opts = Default();
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
			var opts = Default();
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
		internal object[] ToVarargin()
		{
			var varargin = new List<object>();
			Action<string, object> addParam = (s, o) =>
				varargin.AddRange(new [] {s, o});
			addParam("MaxK", (double)MaxK);
			addParam("Level", Level);
			addParam("UseLevels", UsingLevels);
			addParam("AmplitudeFiltration", UsingAmplitudeFiltration);
			addParam("VarianceFiltration", UsingVarianceFiltration);
			addParam("PercentSizeLimit", PercentSizeLimit);
			addParam("FeaturePreservationLimit", FeaturePreservationLimit);
			addParam("Metric", Metric.ToString().ToLower());
			addParam("PlotPartitions", PlottingPartitions);
			addParam("PlotRecursively", PlottingRecursively);
			addParam("DecompositionPlots", PlottingDecomposition);
			addParam("DecompositionPlotsRecursively", PlottingDecompositionRecursively);
			addParam("MaxComponentsForDecomposition", (double)MaxComponentsForDecomposition);
			addParam("OutPath", OutputPath);
			addParam("CachePath", CachePath);
			addParam("Cache", Caching);
			addParam("Verbose", Verbose);
			addParam("KmeansMaxIters", (double)KmeansMaxIters);
			return varargin.ToArray();
		}
		#endregion
	}
}
