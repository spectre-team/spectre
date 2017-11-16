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
using System.IO;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Spectre.Algorithms.Parameterization;
using Spectre.Data.Datasets;
using Spectre.Models.Msi;
using Spectre.Providers;

namespace Spectre.Controllers
{
    /// <summary>
    ///     Allows to read divik result.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DivikResultController : ApiController
    {
        private readonly CachingDatasetProvider _datasetProvider = new CachingDatasetProvider();
        private readonly AccessPathProvider _pathProvider = new AccessPathProvider();

        /// <summary>
        ///     Gets single divik result of specified preparation.
        /// </summary>
        /// <param name="id">Preparation identifier.</param>
        /// <param name="divikId">Identifier of divik.</param>
        /// <param name="level">Divik level.</param>
        /// <returns>DivikResult</returns>
        public DivikResult Get(int id, int divikId, int level)
        {
            if ((divikId < 0) || (level < 0))
            {
                throw new ArgumentException(message: nameof(divikId));
            }

            var path = _pathProvider.GetPath<Algorithms.Results.DivikResult>(id);
            var jsonText = File.ReadAllText(path);
            var divikResult = JsonConvert.DeserializeObject<Algorithms.Results.DivikResult>(jsonText);

            var datasetPath = _pathProvider.GetPath<IDataset>(id);
            var dataset = _datasetProvider.Read(datasetPath);

            var coordinates = dataset.GetRawSpacialCoordinates(is2D: true);

            var length = divikResult.Partition.Length;
#pragma warning disable SA1305 // Field names must not use Hungarian notation
            var xCoordinates = new int[length];
            var yCoordinates = new int[length];
#pragma warning restore SA1305 // Field names must not use Hungarian notation
            var data = new int[length];

            for (var i = 0; i < length; i++)
            {
                xCoordinates[i] = coordinates[i, 0];
                yCoordinates[i] = coordinates[i, 1];
                data[i] = divikResult.Partition[i] + 1;
            }

            return new DivikResult { X = xCoordinates, Y = yCoordinates, Data = data };
        }

        /// <summary>
        ///     Gets divik config of divik result.
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

            var path = _pathProvider.GetPath<DivikOptions>(id);
            var jsonText = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<DivikOptions>(jsonText);
        }
    }
}
