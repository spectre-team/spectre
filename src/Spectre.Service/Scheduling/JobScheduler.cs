/*
 * JobScheduler.cs
 * Hangfire-based implementation of computational job scheduling service.
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
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Spectre.Algorithms.Parameterization;
using Spectre.Service.Abstract;

namespace Spectre.Service.Scheduling
{
    /// <summary>
    /// Hangfire-based job scheduler.
    /// </summary>
    public class JobScheduler : IJobScheduler
    {
        private readonly IDivikService _divikService;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobScheduler"/> class.
        /// </summary>
        /// <param name="divikService">Handle to DiviK computation service.</param>
        public JobScheduler(IDivikService divikService)
        {
            _divikService = divikService;
        }

        /// <summary>
        /// Handle to DiviK calculation service.
        /// </summary>
        private IDivikService DivikService { get; }

        /// <inheritdoc/>
        public string ScheduleDivikJob(string datasetName, DivikOptions options)
        {
            var identifier = BackgroundJob.Enqueue(() => Console.WriteLine("Works!!"));
            return identifier;
        }
    }
}
