/*
 * RoiReader.cs
 * Class with utilities for listing ROI from specified folder
 * and reading the regions of interest data from specified file.

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
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using Spectre.Data.Datasets;

    /// <summary>
    /// Provides listing all available ROI's from directory.
    /// Provides reading a ROI from a directory.
    /// </summary>
    public class RoiReader
    {
        /// <summary>
        /// Lists the rois from directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// All ROIs in specified directory.
        /// </returns>
        /// <exception cref="FileNotFoundException">No *.png files found in directory or subdirectories</exception>
        public List<Roi> GetAllRoisFromDirectory(string path)
        {
            var names = new List<string>();
            var rois = new List<Roi>();
            var allfiles = System.IO.Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);

            names = allfiles.ToList();

            if (names.Count == 0)
            {
                throw new FileNotFoundException("No *.png files found in directory.");
            }

            var roiReader = new RoiReader();

            for (var listIterator = 0; listIterator < names.Count; listIterator++)
            {
                rois.Add(roiReader.RoiDownloader(Path.Combine(path, names[listIterator])));
            }

            return rois;
        }

        /// <summary>
        /// Loads single ROI from specified folded.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// ROI dataset.
        /// </returns>
        public Roi RoiDownloader(string path)
        {
            var roiConverter = new RoiConverter();
            var bitmap = new Bitmap(path);

            var roidataset = roiConverter.BitmapToRoi(bitmap, Path.GetFileNameWithoutExtension(path));

            return roidataset;
        }
    }
}
