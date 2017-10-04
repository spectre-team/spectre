/*
 * GmmModelling.cs
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
using System.Linq;
using MathWorks.MATLAB.NET.Arrays.native;
using Spectre.Algorithms.Results;
using Spectre.Data.Datasets;

namespace Spectre.Algorithms.Methods
{
    /// <summary>
    /// Contains interface for calling matlab GMM algorithms.
    /// </summary>
    public class GmmModelling : IDisposable
    {
        #region Fields

        /// <summary>
        /// MATLAB GMM modelling engine.
        /// </summary>
        private readonly MatlabAlgorithmsNative.GmmModelling _gmm;

        /// <summary>
        /// Indicates whether this instance has been disposed.
        /// </summary>
        private bool _disposed;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GmmModelling"/> class.
        /// </summary>
        public GmmModelling()
        {
            _gmm = new MatlabAlgorithmsNative.GmmModelling();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="GmmModelling"/> class.
        /// </summary>
        ~GmmModelling()
        {
            Dispose(disposing: false);
        }

        #endregion

        #region MATLAB calls

        /// <summary>
        /// Applies the GMM model onto data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="dataset">Input dataset.</param>
        /// <returns>Convolved data.</returns>
        /// <exception cref="System.ObjectDisposedException">thrown if this object has been disposed.</exception>
        /// <exception cref="InvalidOperationException">Applying model build on different m/z axis.</exception>
        public IDataset ApplyGmm(GmmModel model, IDataset dataset)
        {
            ValidateDispose();
            if (!dataset.GetRawMzArray()
                .SequenceEqual(model.OriginalMz))
            {
                throw new InvalidOperationException(message: "Applying model built on different m/z axis.");
            }
            var matlabModel = model.MatlabStruct;
            var applyResult = _gmm.apply_gmm(matlabModel, data: dataset.GetRawIntensities(), mz: dataset.GetRawMzArray());
            var data = (double[,])((MWStructArray)model.MatlabStruct).GetField(fieldName: "mu");
            var mz = new double[data.GetLength(dimension: 0)];
            Buffer.BlockCopy(data, srcOffset: 0, dst: mz, dstOffset: 0, count: data.GetLength(dimension: 0));
            return new BasicTextDataset(mz, data: (double[,])applyResult, coordinates: dataset.GetRawSpacialCoordinates(is2D: true));
        }

        /// <summary>
        /// Estimates the GMM model from the data set.
        /// </summary>
        /// <param name="dataset">Input dataset.</param>
        /// <returns>Estimated model</returns>
        /// <exception cref="System.ObjectDisposedException">thrown if this object has been disposed.</exception>
        public GmmModel EstimateGmm(IDataset dataset)
        {
            ValidateDispose();
            var originalMz = dataset.GetRawMzArray();
            var matlabModel = _gmm.estimate_gmm(originalMz, data: dataset.GetRawIntensities());
            var model = new GmmModel(matlabModel, originalMz);
            return model;
        }

        /// <summary>
        /// Reduces the model filtering by component area.
        /// </summary>
        /// <param name="model">The reduced model.</param>
        /// <returns>Reduced model</returns>
        /// <exception cref="System.ObjectDisposedException">thrown if this object has been disposed.</exception>
        /// <exception cref="NotImplementedException">in any other case, as status of filtering threshold specification is unknown</exception>
        public GmmModel ReduceModelByComponentArea(GmmModel model)
        {
            ValidateDispose();
            throw new NotImplementedException(message: nameof(GmmModelling.ReduceModelByComponentArea)
                                                       + " is not implemented.");

            // var matlabModel = model.MatlabStruct;
            //// @gmrukwa: this has an issue with opts.thr -> it must be defined, but no one knows how
            // var reduced = _gmm.reduce_gmm_by_component_area(matlabModel, model.OriginalMz, model.OriginalMeanSpectrum);
            // return new GmmModel(reduced, model) { IsNoiseReduced = true };
        }

        /// <summary>
        /// Reduces the model filtering by height of the component.
        /// </summary>
        /// <param name="model">The reduced model.</param>
        /// <returns>Reduced model</returns>
        /// <exception cref="System.ObjectDisposedException">thrown if this object has been disposed.</exception>
        /// <exception cref="NotImplementedException">in any other case, as status of filtering threshold specification is unknown</exception>
        public GmmModel ReduceModelByComponentHeight(GmmModel model)
        {
            ValidateDispose();
            throw new NotImplementedException(message: nameof(GmmModelling.ReduceModelByComponentHeight)
                                                       + " is not implemented.");

            // var matlabModel = model.MatlabStruct;
            //// @gmrukwa: this has an issue with opts.thr -> it must be defined, but no one knows how
            // var reduced = _gmm.reduce_gmm_by_component_height(matlabModel, model.OriginalMz, model.OriginalMeanSpectrum);
            // return new GmmModel(reduced, model) { IsNoiseReduced = true };
        }

        /// <summary>
        /// Merges the components supposed to correspond to the same compound.
        /// </summary>
        /// <param name="model">The merged model.</param>
        /// <param name="mzThreshold">The mz threshold used for components matching.</param>
        /// <returns>Merged model</returns>
        /// <exception cref="System.InvalidOperationException">Applying merging on merged model.</exception>
        /// <exception cref="System.ObjectDisposedException">thrown if this object has been disposed.</exception>
        public GmmModel MergeComponents(GmmModel model, double mzThreshold = 0.3)
        {
            ValidateDispose();
            if (model.IsMerged)
            {
                throw new InvalidOperationException(message: "Applying merging on merged model.");
            }
            var matlabModel = model.MatlabStruct;
            var merged = _gmm.merge_gmm_model_components(
                matlabModel,
                model.OriginalMz,
                model.OriginalMeanSpectrum,
                mzThreshold);
            return new GmmModel(merged, model) { IsMerged = true, MzMergingThreshold = mzThreshold };
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
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
        /// Validates the dispose state. If this instance has been disposed, throws an exception.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">thrown if this object has been disposed.</exception>
        private void ValidateDispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(objectName: nameof(GmmModelling));
            }
        }

        #endregion
    }
}
