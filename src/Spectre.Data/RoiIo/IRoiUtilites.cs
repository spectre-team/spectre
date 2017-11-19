/*
 * IRoiUtilities.cs
 * Interface for RoiUtilities.

   Copyright 2017 Roman Lisak

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
using Spectre.Data.Datasets;

namespace Spectre.Data.RoiIo
{
    /// <summary>
    /// Interface for RoiUtilities.
    /// </summary>
    public interface IRoiUtilites
    {
        /// <summary>
        /// Reads regions of interest from a png file.
        /// </summary>
        /// <param name="path">Path for the rois directory, either Spectre.Data or test_files director.</param>
        /// <returns>
        /// Returns list doubles.
        /// </returns>
        RoiDataset RoiReader(string path);

        /// <summary>
        /// Writes roi dataset into a png file.
        /// </summary>
        /// <param name="prototyp">The prototyp.</param>
        /// <returns>
        /// Returns true if operation was successfull.
        /// </returns>
        bool RoiWriter(RoiDataset prototyp);

        /// <summary>
        /// Lists the rois from directory.
        /// </summary>
        /// <param name="path">Path for the rois directory, either Spectre.Data or test_files director.</param>
        /// <returns>
        /// Returns names of all Roi files in the directory.
        /// </returns>
        List<string> ListRoisFromDirectory(string path);
    }
}
