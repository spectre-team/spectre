/*
 * ComputationController.cs
 * Controller that handles computational job requests.
 *
   Copyright 2017 Maciej Gamrat

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
using Spectre.Algorithms.Parameterization;
using Spectre.Service.Abstract;
using Spectre.Service.Loaders;

namespace Spectre.Controllers
{
    /// <summary>
    /// Allows to start computation jobs.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ComputationController : ApiController
    {
        private IJobScheduler _jobScheduler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComputationController"/> class.
        /// </summary>
        /// <param name="jobScheduler">Handle to job scheduling service.</param>
        public ComputationController(IJobScheduler jobScheduler)
        {
            _jobScheduler = jobScheduler;
        }

        /// <summary>
        /// Schedule DiviK computation.
        /// </summary>
        /// <param name="datasetName">Dataset name.</param>
        /// <param name="divikOptions">Options for DiviK algorithm.</param>
        /// <returns>HTTP action result</returns>
        [HttpPost]
        public string Post(string datasetName, [FromBody] DivikOptions divikOptions)
        {
            var identifier = _jobScheduler.ScheduleDivikJob(datasetName, divikOptions);
            return identifier;
        }
    }
}
