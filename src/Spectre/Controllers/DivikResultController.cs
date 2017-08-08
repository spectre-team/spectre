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
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Spectre.Data.Datasets;
using Spectre.Models.Msi;

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
            int[] x = new int[1];
            int[] y = new int[1];
            int[] data = new int[1];
            return new DivikResult() { X = x, Y = y, Data = data };
        }
    }
}
