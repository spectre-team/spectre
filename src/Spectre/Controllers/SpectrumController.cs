/*
 * SpectrumController.cs
 * Class serving GET requests for spectrum.
 *
   Copyright 2017 Grzegorz Mrukwa, Michał Gallus, Daniel Babiak
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

using Spectre.Service.Configuration;
using Spectre.Service.Io;

namespace Spectre.Controllers
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Spectre.Data.Datasets;
    using Spectre.Models.Msi;

    /// <summary>
    /// Exhibits spectrum
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SpectrumController : ApiController
    {
        /// <summary>
        /// Gets single spectrum of a specified preparation by identifier.
        /// </summary>
        /// <param name="id">Preparation identifier.</param>
        /// <param name="spectrumId">Spectrum identifier.</param>
        /// <returns>Spectrum</returns>
        public Spectrum Get(int id, int spectrumId)
        {
            if (id != 1)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            DatasetLoader datasetLoader = new DatasetLoader(
                new DataRootConfig(
                    ConfigurationManager.AppSettings["LocalDataDirectory"],
                    ConfigurationManager.AppSettings["RemoteDataDirectory"]));
            IDataset dataset = datasetLoader.GetFromName("hnc1_tumor");

            var mz = dataset.GetRawMzArray();

            try
            {
                var intensities = dataset.GetRawIntensityArray(spectrumId);
                var coordinates = dataset.GetSpatialCoordinates(spectrumId);

                return new Spectrum()
                {
                    Id = spectrumId,
                    Intensities = intensities,
                    Mz = mz,
                    X = coordinates.X,
                    Y = coordinates.Y
                };
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Gets single spectrum of a specified preparation by spatial coordinates.
        /// </summary>
        /// <param name="id">Preparation identifier.</param>
        /// <param name="x">Spectrum's X coordinate.</param>
        /// <param name="y">Spectrum's Y coordinate.</param>
        /// <returns>Spectrum</returns>
        public Spectrum Get(int id, int x, int y)
        {
            if (id != 1)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            DatasetLoader datasetLoader = new DatasetLoader(
                new DataRootConfig(
                    ConfigurationManager.AppSettings["LocalDataDirectory"],
                    ConfigurationManager.AppSettings["RemoteDataDirectory"]));
            IDataset dataset = datasetLoader.GetFromName("hnc1_tumor");

            var spectrumId = dataset.SpatialCoordinates.ToList()
                .FindIndex(sc => sc.X == x && sc.Y == y);

            if (spectrumId == -1)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var mz = dataset.GetRawMzArray();
            var intensities = dataset.GetRawIntensityArray(spectrumId);
            var coordinates = dataset.GetSpatialCoordinates(spectrumId);

            return new Spectrum()
            {
                Id = spectrumId,
                Intensities = intensities,
                Mz = mz,
                X = x,
                Y = y
            };
        }
    }
}
