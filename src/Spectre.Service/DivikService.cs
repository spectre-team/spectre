/*
 * DivikService.cs
 * Contains definition of service for calculating Divik.
 *
   Copyright 2017 Michał Wolny

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
using Spectre.Algorithms.Parameterization;
using Spectre.Algorithms.Results;
using Spectre.AlgorithmsCli.Methods;
using Spectre.Data.Datasets;
using Spectre.Service.Abstract;

namespace Spectre.Service
{
    /// <summary>
    ///     Class for calaculating divik.
    /// </summary>
    internal class DivikService : IDivikService, IDisposable
    {
        /// <summary>
        ///     Private field for holding <see cref="Segmentation" /> for the service.
        /// </summary>
        private readonly Segmentation _segmentation;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DivikService" /> class.
        ///     Creates <see cref="Segmentation" /> (MCR) instance for the service.
        /// </summary>
        public DivikService()
        {
            _segmentation = new Segmentation();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _segmentation.Dispose();
        }

        /// <summary>
        ///     Service method for calculating divik.
        /// </summary>
        /// <param name="dataset">The source dataset being passed to the target.</param>
        /// <param name="options">The options passed to the divik algorithm.</param>
        /// <returns><see cref="DivikResult" /> of a given divik calculation.</returns>
        public DivikResult CalculateDivik(IDataset dataset, DivikOptions options) => _segmentation.Divik(
            dataset,
            options);
    }
}
