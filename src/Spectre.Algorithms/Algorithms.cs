/*
 * Algorithms.cs
 * Contains .NET interface for implemented algorithms.
 * 
   Copyright 2017 Wilgierz Wojciech, Michal Gallus, Grzegorz Mrukwa

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
using MatlabAlgorithmsNative;
using Spectre.Algorithms.Results;

namespace Spectre.Algorithms
{
	/// <summary>
	/// Exhibits all MSI-oriented algorithms.
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public class Algorithms: IDisposable
    {
		#region Fields
		private readonly Gmm _gaussianMixtureModel;
        private readonly Preprocessing _preprocessing;
        private readonly Segmentation _segmentation;

		/// <summary>
		/// Indicates whether this instance has been disposed.
		/// </summary>
		private bool _disposed = false;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="Algorithms"/> class.
		/// </summary>
		public Algorithms()
        {
            _gaussianMixtureModel = new Gmm();
            _preprocessing = new Preprocessing();
            _segmentation = new Segmentation();
        }
		#endregion

		#region MATLAB calls
		/// <summary>
		/// Applies the GMM model onto data.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="data">The data.</param>
		/// <param name="mz">The mz axis ticks.</param>
		/// <returns>Convolved data.</returns>
		public double[,] ApplyGmm(GmmModel model, double[,] data, double[] mz)
        {
			ValidateDispose();
	        var matlabModel = model.MatlabStruct;
	        var applyResult = _gaussianMixtureModel.apply_gmm(matlabModel, data, mz);
			return (double[,])applyResult;
            
        }

		/// <summary>
		/// Estimates the GMM model from the data set.
		/// </summary>
		/// <param name="mz">The mz axis ticks.</param>
		/// <param name="data">The data.</param>
		/// <param name="merge">if set to <c>true</c> merges components.</param>
		/// <param name="remove">if set to <c>true</c> removes shaping components.</param>
		/// <returns>Estimated model</returns>
		public GmmModel EstimateGmm(object mz, double[,] data, bool merge, bool remove)
        {
			ValidateDispose();
			var matlabModel = _gaussianMixtureModel.estimate_gmm(mz, data, merge, remove);
			var model = new GmmModel(matlabModel);
	        return model;
        }

		/// <summary>
		/// Perform FFT-based peak alignment.
		/// </summary>
		/// <param name="mz">The mz axis ticks.</param>
		/// <param name="data">The data.</param>
		/// <returns>Aligned data.</returns>
		public double[,] AlignPeaksFft(object mz, object data)
        {
			ValidateDispose();
	        var pafftResult = _preprocessing.pafft(mz, data);
			return (double[,])pafftResult;
        }

		/// <summary>
		/// Removes the baseline.
		/// </summary>
		/// <param name="mz">The mz axis ticks.</param>
		/// <param name="data">The data.</param>
		/// <returns>Data set without baseline.</returns>
		public double[,] RemoveBaseline(double[] mz, double[,] data)
        {
			ValidateDispose();
	        var baselineRemovalResult = _preprocessing.remove_baseline(mz, data);
			return (double[,])baselineRemovalResult;
        }

		/// <summary>
		/// Normalizes dataset by TIC-based method.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns>Normalized data set.</returns>
		public double[,] NormalizeByTic(double[,] data)
        {
			ValidateDispose();
	        var normalizationResult = _preprocessing.ticnorm(data);
            return (double[,])normalizationResult;
        }

		/// <summary>
		/// Performs DiviK clustering on the specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="coordinates">Spatial coordinates.</param>
		/// <param name="varargin">Configuration.</param>
		/// <returns>Segmentation result.</returns>
		public DivikResult Divik(double[,] data, int[,] coordinates, object[] varargin)
        {
			ValidateDispose();
			//this is needed to not to make MCR go wild
			const int numberOfOutputArgs = 2;
			double[,] coords = new double[coordinates.GetLength(0),coordinates.GetLength(1)];
			for(int i = 0; i<coordinates.GetLength(0); ++i)
				for (int j = 0; j < coordinates.GetLength(1); ++j)
					coords[i, j] = coordinates[i, j];

			var tmp = _segmentation.divik(numberOfOutputArgs, data, coordinates, varargin);
			var result = new DivikResult(tmp);
            return result;
        }
		#endregion

		#region IDisposable
		/// <summary>
		/// Validates the dispose state. If this instance has been disposed, throws an exception.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">thrown if this object has been disposed.</exception>
		private void ValidateDispose()
	    {
		    if (this._disposed)
		    {
			    throw new ObjectDisposedException(nameof(Algorithms));
		    }
	    }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._gaussianMixtureModel.Dispose();
                    this._preprocessing.Dispose();
                    this._segmentation.Dispose();
                }
                _disposed = true;
            }
        }

		/// <summary>
		/// Finalizes an instance of the <see cref="Algorithms"/> class.
		/// </summary>
		~Algorithms()
	    {
		    Dispose(false);
	    }
		#endregion
	}
}
