/*
 * DivikResultController.cs
 * Class serving GET requests for divik result.
 *
   Copyright 2017 Grzegorz Mrukwa, Sebastian Pustelnik, Daniel Babiak

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
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Spectre.Algorithms.Parameterization;
using Spectre.Data.Datasets;
using Spectre.Models.Msi;
using Spectre.Service.Configuration;
using Spectre.Service.Io;

namespace Spectre.Controllers
{
    /// <summary>
    /// Allows to read divik result.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DivikResultController : ApiController
    {
        /// <summary>
        /// Gets single divik result of specified preparation.
        /// </summary>
        /// <param name="id">Preparation identifier.</param>
        /// <param name="divikId">Identifier of divik.</param>
        /// <param name="level">Divik level.</param>
        /// <returns>DivikResult</returns>
        public DivikResult Get(int id, int divikId, int level)
        {
            if (divikId < 0 || level < 0)
            {
                throw new ArgumentException(message: nameof(divikId));
            }

            if (id != 1)
            {
                return null;
            }

            var jsonText = File.ReadAllText("C:\\spectre_data\\expected_divik_results\\hnc1_tumor\\euclidean\\divik-result.json");
            Algorithms.Results.DivikResult divikResult = JsonConvert.DeserializeObject<Algorithms.Results.DivikResult>(jsonText);

            DatasetLoader datasetLoader = new DatasetLoader(
                new DataRootConfig(
                    ConfigurationManager.AppSettings["LocalDataDirectory"],
                    ConfigurationManager.AppSettings["RemoteDataDirectory"]));
            IDataset dataset = datasetLoader.GetFromName("hnc1_tumor");

            var coordinates = dataset.GetRawSpacialCoordinates(is2D: true);

            int length = divikResult.Partition.Length;
            var x_coordinates = new int[length];
            var y_coordinates = new int[length];
            var data = new int[length];

            for (var i = 0; i < length; i++)
            {
                x_coordinates[i] = coordinates[i, 0];
                y_coordinates[i] = coordinates[i, 1];
                data[i] = divikResult.Partition[i] + 1;
            }

            return new DivikResult() { X = x_coordinates, Y = y_coordinates, Data = data };
        }

        /// <summary>
        /// Gets divik config of divik result.
        /// </summary>
        /// <param name="id">Preparation identifier.</param>
        /// <param name="divikId">Identifier of divik.</param>
        /// <returns>DivikConfig</returns>
        public DivikOptions GetConfig(int id, int divikId)
        {
            if (divikId < 0)
            {
                throw new ArgumentException(message: nameof(divikId));
            }

            if (id != 1)
            {
                return DivikOptions.Default();
            }
            var jsonText = File.ReadAllText("C:\\spectre_data\\expected_divik_results\\hnc1_tumor\\euclidean\\config.json");
            return JsonConvert.DeserializeObject<DivikOptions>(jsonText);
        }
    }
}
