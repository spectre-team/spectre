/*
 * GmmFiltering.cs
 * Performs filtering using Gaussian Mixture Models.
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

namespace Spectre.Algorithms.Methods
{
    public class GmmFiltering: IDisposable
    {
        #region Fields
        /// <summary>
        /// MATLAB GMM filtering engine.
        /// </summary>
        private readonly MatlabAlgorithmsNative.GmmFiltering _gmm;

        /// <summary>
        /// Indicates whether this instance has been disposed.
        /// </summary>
        private bool _disposed;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="GmmFiltering"/> class.
        /// </summary>
        public GmmFiltering()
        {
            _gmm = new MatlabAlgorithmsNative.GmmFiltering();
        }
        #endregion

        #region MATLAB calls
        /// <summary>
        /// Estimates the thresholds from the decomposition of values.
        /// </summary>
        /// <param name="values">The values to be thresholded.</param>
        /// <returns>Thresholds in ascending order.</returns>
        public double[] EstimateThresholds(IEnumerable<double> values)
        {
            var valuesArray = values as double[] ?? values.ToArray();
            var values2D = new double[valuesArray.Count(), 1];
            var i = 0;
            foreach (var value in valuesArray)
                values2D[i++, 0] = value;
            var estimatedThresholdsCell = ((MWCellArray)_gmm.fetch_thresholds(values2D));
            var thresholds = (double[,])estimatedThresholdsCell[1];
            return thresholds.Cast<double>().ToArray();
        }
        #endregion

        #region IDisposable
        /// <summary>
        /// Validates the dispose state. If this instance has been disposed, throws an exception.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">thrown if this object has been disposed.</exception>
        private void ValidateDispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(GmmFiltering));
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
            if (!_disposed)
            {
                if (disposing)
                {
                    _gmm.Dispose();
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Algorithms"/> class.
        /// </summary>
        ~GmmFiltering()
        {
            Dispose(false);
        }
        #endregion
    }
}
