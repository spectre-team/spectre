/*
 * GmmModel.cs
 * Wrapper for MATLAB result struct from GMM estimation.
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
using MathWorks.MATLAB.NET.Arrays.native;

namespace Spectre.Algorithms.Results
{
	/// <summary>
	/// Provides wrapper for GMM model, independent on implementation.
	/// </summary>
	public class GmmModel
	{
        #region Fields
        /// <summary>
        /// Gets a value indicating whether this instance is merged.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is merged; otherwise, <c>false</c>.
        /// </value>
        public bool IsMerged { get; internal set; }
        /// <summary>
        /// Gets a value indicating whether this instance is noise components reduced.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is noise reduced; otherwise, <c>false</c>.
        /// </value>
        public bool IsNoiseReduced { get; internal set; }
        /// <summary>
        /// Gets the m/z threshold used in components merging.
        /// </summary>
        /// <value>
        /// The m/z merging threshold.
        /// </value>
        public double? MzMergingThreshold { get; internal set; }
        /// <summary>
        /// Gets the original m/z axis.
        /// </summary>
        /// <value>
        /// The original m/z axis.
        /// </value>
        public IEnumerable<double> OriginalMz { get; }
        /// <summary>
        /// Gets the original mean spectrum.
        /// </summary>
        /// <value>
        /// The original mean spectrum.
        /// </value>
        public IEnumerable<double> OriginalMeanSpectrum { get; private set; }
        /// <summary>
        /// Gets the peak locations expressed as means of distributions.
        /// </summary>
        /// <value>
        /// The peak locations.
        /// </value>
        public IEnumerable<double> PeakLocations { get; private set; }
        /// <summary>
        /// Gets the peak widths expressed as peak variance.
        /// </summary>
        /// <value>
        /// The peak widths.
        /// </value>
        public IEnumerable<double> PeakWidths { get; private set; }
        /// <summary>
        /// Gets the peak height multipliers.
        /// </summary>
        /// <value>
        /// The peak height multipliers.
        /// </value>
        public IEnumerable<double> PeakHeightMultipliers { get; private set; }


        /// <summary>
        /// Gets the matlab GMM structure.
        /// </summary>
        /// <value>
        /// The matlab structure.
        /// </value>
        internal object MatlabStruct { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GmmModel"/> class.
        /// </summary>
        /// <param name="matlabGmm">The matlab GMM model struct.</param>
        /// <param name="originalMz">m/z axis from modelled data.</param>
        internal GmmModel(object matlabGmm, IEnumerable<double> originalMz)
		{
			MatlabStruct = matlabGmm;
		    OriginalMz = originalMz;
            ParseMatlabStruct(matlabGmm);
		}

        /// <summary>
		/// Initializes a new instance of the <see cref="GmmModel"/> class.
		/// </summary>
		/// <param name="matlabGmm">The matlab GMM model struct.</param>
		/// <param name="model">Parent model from which new one was created.</param>
		internal GmmModel(object matlabGmm, GmmModel model)
        {
            MatlabStruct = matlabGmm;
            OriginalMz = model.OriginalMz;
            OriginalMeanSpectrum = model.OriginalMeanSpectrum;
            IsMerged = model.IsMerged;
            IsNoiseReduced = model.IsNoiseReduced;
            MzMergingThreshold = model.MzMergingThreshold;
            ParseMatlabStruct(matlabGmm);
        }
        #endregion

        #region MATLAB parsing
        /// <summary>
        /// Parses the matlab structure.
        /// </summary>
        /// <param name="matlabModel">The model.</param>
        private void ParseMatlabStruct(object matlabModel)
        {
            var model = (MWStructArray) matlabModel;
            Func<double[,], double[]> flatten = t => t.Cast<double>().ToArray();
            OriginalMeanSpectrum = flatten((double[,]) model.GetField("meanspec"));
            PeakLocations = flatten((double[,]) model.GetField("mu"));
            PeakWidths = flatten((double[,]) model.GetField("sig"));
            PeakHeightMultipliers = flatten((double[,]) model.GetField("w"));
        }
        #endregion
    }
}
