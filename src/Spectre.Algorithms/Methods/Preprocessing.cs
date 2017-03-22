/*
 * Preprocessing.cs
 * Contains .NET interface for preprocessing algorithms.
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
using Spectre.Data.Datasets;

namespace Spectre.Algorithms.Methods
{
	public class Preprocessing: IDisposable
	{
		#region Fields
		private readonly MatlabAlgorithmsNative.Preprocessing _preprocessing;

		/// <summary>
		/// Indicates whether this instance has been disposed.
		/// </summary>
		private bool _disposed = false;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="Algorithms"/> class.
		/// </summary>
		public Preprocessing()
		{
			_preprocessing = new MatlabAlgorithmsNative.Preprocessing();
		}
		#endregion

		#region MATLAB calls

	    /// <summary>
	    /// Perform FFT-based peak alignment.
	    /// </summary>
	    /// <param name="dataset">Input dataset.</param>
	    /// <returns>Aligned dataset.</returns>
	    /// <exception cref="System.ObjectDisposedException">thrown if this object has been disposed.</exception>
	    public IDataset AlignPeaksFft(IDataset dataset)
		{
			ValidateDispose();
			var pafftResult = _preprocessing.pafft(dataset.GetRawMzArray(), dataset.GetRawIntensities());
            return new BasicTextDataset(dataset.GetRawMzArray(), (double[,])pafftResult, dataset.GetRawSpacialCoordinates(true));
		}

	    /// <summary>
	    /// Removes the baseline.
	    /// </summary>
	    /// <param name="dataset">Input dataset.</param>
	    /// <returns>Dataset without baseline.</returns>
	    /// <exception cref="System.ObjectDisposedException">thrown if this object has been disposed.</exception>
	    public IDataset RemoveBaseline(IDataset dataset)
		{
			ValidateDispose();
			var baselineRemovalResult = _preprocessing.remove_baseline(dataset.GetRawMzArray(), dataset.GetRawIntensities());
            return new BasicTextDataset(dataset.GetRawMzArray(), (double[,])baselineRemovalResult, dataset.GetRawSpacialCoordinates(true));
		}

	    /// <summary>
	    /// Normalizes dataset by TIC-based method.
	    /// </summary>
	    /// <param name="dataset">Input dataset.</param>
	    /// <returns>Normalized dataset.</returns>
	    /// <exception cref="System.ObjectDisposedException">thrown if this object has been disposed.</exception>
	    public IDataset NormalizeByTic(IDataset dataset)
		{
			ValidateDispose();
			var normalizationResult = _preprocessing.ticnorm(dataset.GetRawIntensities());
            return new BasicTextDataset(dataset.GetRawMzArray(), (double[,])normalizationResult, dataset.GetRawSpacialCoordinates(true));
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
					this._preprocessing.Dispose();
				}
				_disposed = true;
			}
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="Algorithms"/> class.
		/// </summary>
		~Preprocessing()
		{
			Dispose(false);
		}
		#endregion
	}
}
