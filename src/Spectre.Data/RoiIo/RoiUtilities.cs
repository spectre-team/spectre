/*
 * RoiUtilities.cs
 * Class with utilities for managing the regions of interest data.

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

namespace Spectre.Data.RoiIo
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;
    using Spectre.Data.Datasets;

    /// <summary>
    /// Class with utilities for managing the regions of interest data.
    /// </summary>
    /// <seealso cref="IRoiUtilites" />
    public class RoiUtilities : IRoiUtilites
    {
        /// <summary>
        /// Reads regions of interest from a png file.
        /// </summary>
        /// <returns>
        /// Returns list doubles.
        /// </returns>
        public Roi RoiReader()
        {
            Roi prototyp = new Roi();

            return prototyp;
        }

        /// <summary>
        /// Writes list of doubles into a png file.
        /// </summary>
        /// <param name="prototyp">The prototyp.</param>
        /// <returns>
        /// Returns true if operation was succeded.
        /// </returns>
        public bool RoiWriter(Roi prototyp)
        {
            // write from list to png file.
            return false;
        }

        /// <summary>
        /// Lists the rois from directory.
        /// </summary>
        /// <returns>
        /// Names of all roi files in the directory.
        /// </returns>
        public List<string> ListRoisFromDirectory()
        {
            List<string> names = new List<string>();

            string[] allfiles = System.IO.Directory.GetFiles(
                Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                "*.png",
                SearchOption.AllDirectories);

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            names = allfiles.ToList();

            return names;
        }
    }
}