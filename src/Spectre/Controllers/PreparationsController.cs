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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Spectre.Data.Datasets;
using Spectre.Data.Structures;
using Spectre.Models.Msi;

namespace Spectre.Controllers
{
    //[Authorize] // should be enabled when authorization is ready
    /// <summary>
    /// Allows to read preparation data.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PreparationsController : ApiController
    {
        /// <summary>
        /// GET for the list of preparations.
        /// </summary>
        /// <returns>List of preparations.</returns>
        public IEnumerable<Preparation> Get()
        {
            return new[] { new Preparation("Head & neck cancer, patient 1, tumor region only", 1) };
        }


        /// <summary>
        /// GET for single preparation data.
        /// </summary>
        /// <param name="id">Preparation ID.</param>
        /// <returns>Data of preparation.</returns>
        public Preparation Get(int id)
        {
            return id == 1 ? new Preparation("Head & neck cancer, patient 1, tumor region only", 1) : null;
        }


        /// <summary>
        /// Gets single spectrum of a specified preparation.
        /// </summary>
        /// <param name="id">Preparation identifier.</param>
        /// <param name="spectrumId">Spectrum identifier.</param>
        /// <returns>Spectrum</returns>
        public Spectrum Get(int id, int spectrumId)
        {
            if (id != 1)
                return null;

            var dataset = new BasicTextDataset("C:\\spectre_data\\hnc1_tumor.txt");

            var mz = dataset.GetRawMzArray();

            var intensities = dataset.GetRawIntensityArray(spectrumId);
            var coordinates = dataset.GetSpatialCoordinates(spectrumId);

            return new Spectrum() { Id = spectrumId, Intensities = intensities, Mz = mz, X = coordinates.X, Y = coordinates.Y };
        }

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
                throw new ArgumentException(nameof(channelId));

            if (id != 1)
                return null;

            IDataset dataset = new BasicTextDataset("C:\\spectre_data\\hnc1_tumor.txt");

            var mz = dataset.GetRawMzValue(channelId);
            var intensities = dataset.GetRawIntensityRow(channelId);
            var coordinates = dataset.GetRawSpacialCoordinates(true);

            int[] xCoordinates = new int[intensities.Length];
            int[] yCoordinates = new int[intensities.Length];

            for (int i = 0; i < intensities.Length; i++)
            {
                xCoordinates[i] = coordinates[i, 0];
                yCoordinates[i] = coordinates[i, 1];
            }

            return new Heatmap() {Mz = mz, Intensities = intensities, X = xCoordinates, Y = yCoordinates};
        }
    }
}
