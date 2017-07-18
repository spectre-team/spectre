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
    }
}
