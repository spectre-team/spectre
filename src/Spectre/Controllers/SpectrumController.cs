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

using System.Collections.Generic;

namespace Spectre.Controllers
{
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
        /// Gets single spectrum of a specified preparation.
        /// </summary>
        /// <param name="id">Preparation identifier.</param>
        /// <param name="spectrumId">Spectrum identifier.</param>
        /// <returns>Spectrum</returns>
        public Spectrum Get(int id, int spectrumId)
        {
            if (id != 1)
            {
                return null;
            }

            var dataset = new BasicTextDataset(textFilePath: "C:\\spectre_data\\hnc1_tumor.txt");

            var mz = dataset.GetRawMzArray();

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

        /// <summary>
        /// Gets all specta.
        /// </summary>
        /// <param name="id">Preparation identifier.</param>
        /// <returns>Spectra</returns>
        public Spectrum[] GetAllSpectra(int id)
        {
            Spectrum[] spectra = new Spectrum[10];
            /* from where should I take number of spectra?*/
            for (int i = 1; i <= 10; i++)
            {
                spectra[i - 1] = Get(id, i);
            }
            return spectra;
        }
    }
}
