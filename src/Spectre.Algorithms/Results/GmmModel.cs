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

namespace Spectre.Algorithms.Results
{
	/// <summary>
	/// Provides wrapper for GMM model, independent on implementation.
	/// </summary>
	public class GmmModel
	{
        public bool IsMerged { get; }
        public bool IsNoiseReduced { get; }
	    public double MzMergingThreshold { get; }
        public double[] OriginalMz { get; }
        public double[] OriginalMeanSpectrum { get; }


		/// <summary>
		/// Gets the matlab GMM structure.
		/// </summary>
		/// <value>
		/// The matlab structure.
		/// </value>
		internal object MatlabStruct { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="GmmModel"/> class.
		/// </summary>
		/// <param name="matlabGmm">The matlab GMM model struct.</param>
		internal GmmModel(object matlabGmm)
		{
			MatlabStruct = matlabGmm;
		}
	}
}
