/*
 * IJobScheduler.cs
 * Contains definition of interface for computational job scheduling service.
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
using Spectre.Algorithms.Parameterization;
using Spectre.Data.Datasets;

namespace Spectre.Service.Abstract
{
    /// <summary>
    /// Interface for computational job scheduling service.
    /// </summary>
    public interface IJobScheduler
    {
        /// <summary>
        /// Schedule DiviK calculation.
        /// </summary>
        /// <param name="datasetName">The source dataset name to be opened.</param>
        /// <param name="options">The options passed to the divik algorithm.</param>
        /// <returns>Job idenifier.</returns>
        string ScheduleDivikJob(string datasetName, DivikOptions options);
    }
}
