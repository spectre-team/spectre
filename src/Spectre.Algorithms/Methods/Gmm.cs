/*
 * Gmm.cs
 * Contains .NET interface for GMM algorithms.
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
using Spectre.Algorithms.Results;

namespace Spectre.Algorithms.Methods
{
	public class Gmm: IDisposable
	{
		#region Fields
		private readonly MatlabAlgorithmsNative.Gmm _gmm;

		/// <summary>
		/// Indicates whether this instance has been disposed.
		/// </summary>
		private bool _disposed = false;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="Gmm"/> class.
		/// </summary>
		public Gmm()
		{
			_gmm = new MatlabAlgorithmsNative.Gmm();
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
			var applyResult = _gmm.apply_gmm(matlabModel, data, mz);
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
			var matlabModel = _gmm.estimate_gmm(mz, data, merge, remove);
			var model = new GmmModel(matlabModel);
			return model;
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
					this._gmm.Dispose();
				}
				_disposed = true;
			}
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="Algorithms"/> class.
		/// </summary>
		~Gmm()
		{
			Dispose(false);
		}
		#endregion
	}
}
