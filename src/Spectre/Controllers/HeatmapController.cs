/*
 * HeatmapController.cs
 * Class serving GET requests for heatmap.
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
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Spectre.Data.Datasets;
    using Spectre.Models.Msi;

    /// <summary>
    /// Controller for the needs of Heatmap providing
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HeatmapController : ApiController
    {
        /// <summary>
        /// Gets single heatmap of a specified preparation based on provided mz.
        /// </summary>
        /// <param name="id">Preparation identifier.</param>
        /// <param name="channelId">Identifier of channel.</param>
        /// <param name="flag">Does nothing but allows to define this function.</param>
        /// <returns>Heatmap</returns>
        /// <exception cref="ArgumentException">Thrown when provided mz is lower
        /// than zero, or is invalid for a given dataset</exception>
        public Heatmap Get(int id, int channelId, bool flag)
        {
            if (channelId < 0)
            {
                throw new ArgumentException(message: nameof(channelId));
            }

            if (id != 1)
            {
                return null;
            }

            DatasetLoader datasetLoader = new DatasetLoader(
                new DataRootConfig(
                    ConfigurationManager.AppSettings["LocalDataDirectory"],
                    ConfigurationManager.AppSettings["RemoteDataDirectory"]));
            IDataset dataset = datasetLoader.GetFromName("hnc1_tumor");

            var mz = dataset.GetRawMzValue(channelId);
            var intensities = dataset.GetRawIntensityRow(channelId);
            var coordinates = dataset.GetRawSpacialCoordinates(is2D: true);

#pragma warning disable SA1305 // Field names must not use Hungarian notation
            var xCoordinates = new int[intensities.Length];
            var yCoordinates = new int[intensities.Length];
#pragma warning restore SA1305 // Field names must not use Hungarian notation

            for (var i = 0; i < intensities.Length; i++)
            {
                xCoordinates[i] = coordinates[i, 0];
                yCoordinates[i] = coordinates[i, 1];
            }

            return new Heatmap() { Mz = mz, Intensities = intensities, X = xCoordinates, Y = yCoordinates };
        }
    }
}
