/*
 * PreparationsController.cs
 * Class serving GET requests for preparations.
 *
   Copyright 2017 Grzegorz Mrukwa, Michał Gallus
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

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Spectre.Data.Datasets;
using Spectre.Models.Msi;
using Spectre.Providers;

namespace Spectre.Controllers
{
    /// <summary>
    ///     Allows to read preparation data.
    ///     [Authorize] // should be enabled when authorization is ready
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PreparationsController : ApiController
    {
        private readonly CachingDatasetProvider _datasetProvider = new CachingDatasetProvider();
        private readonly AccessPathProvider _pathProvider = new AccessPathProvider();

        /// <summary>
        ///     GET for the list of preparations.
        /// </summary>
        /// <returns>List of preparations.</returns>
        public IEnumerable<Preparation> Get()
        {
            return _pathProvider.GetAvailableDatasets()
                .Select(
                    selector: name =>
                    {
                        var dataset = _datasetProvider.Read(
                            path: _pathProvider.GetPath<IDataset>(id: name.GetHashCode()));
#pragma warning disable SA1305 // Field names must not use Hungarian notation
                        var xRange = new Range(
#pragma warning restore SA1305 // Field names must not use Hungarian notation
                            min: dataset.SpatialCoordinates.Min(selector: c => c.X),
                            max: dataset.SpatialCoordinates.Max(selector: c => c.X));
#pragma warning disable SA1305 // Field names must not use Hungarian notation
                        var yRange = new Range(
#pragma warning restore SA1305 // Field names must not use Hungarian notation
                            min: dataset.SpatialCoordinates.Min(selector: c => c.Y),
                            max: dataset.SpatialCoordinates.Max(selector: c => c.Y));
                        return new Preparation(
                            id: name.GetHashCode(),
                            name: name,
                            spectraNumber: dataset.SpectrumCount,
                            xRange: xRange,
                            yRange: yRange);
                    });
        }

        /// <summary>
        ///     GET for single preparation data.
        /// </summary>
        /// <param name="id">Preparation ID.</param>
        /// <returns>Data of preparation.</returns>
        public Preparation Get(int id)
        {
            return Get()
                .FirstOrDefault(predicate: preparation => preparation.Id == id);
        }
    }
}
