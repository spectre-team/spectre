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
    /// Provides listing all available ROI's from directory and subdirectories.
    /// Provides reading a ROI from a directory.
    /// </summary>
    public class RoiReader
    {
        /// <summary>
        /// The path
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoiReader"/> class.
        /// </summary>
        public RoiReader()
        {
            _path = System.IO.Path.GetFullPath(@"..\..\..\");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoiReader" /> class.
        /// Used to pass exact path to methods. Default constructor sets project directory
        /// as current path.
        /// </summary>
        /// <param name="testpath">The testpath.</param>
        public RoiReader(string testpath)
        {
            _path = testpath;
        }

        /// <summary>
        /// Lists the rois from directory.
        /// </summary>
        /// <returns>
        /// Names of all roi files in the directory.
        /// </returns>
        /// <exception cref="FileNotFoundException">No *.png files found in directory or subdirectories</exception>
        public List<string> ListRoisFromDirectory()
        {
            var names = new List<string>();

            var allfiles = System.IO.Directory.GetFiles(_path, "*.png", SearchOption.AllDirectories);

            names = allfiles.ToList();

            if (names.Count == 0)
            {
                throw new FileNotFoundException("No *.png files found in directory or subdirectories");
            }

            return names;
        }

        /// <summary>
        /// Loads single ROI from specified folded.
        /// </summary>
        /// <returns>
        /// ROI dataset.
        /// </returns>
        public Roi RoiLoader()
        {
            var blackColor = 0;
            var color = default(Color);
            var bitmap = new Bitmap(_path);
            var roidataset = new Roi
            {
                RoiPixels = new List<RoiPixel>(),
                Name = Path.GetFileNameWithoutExtension(_path),
                Height = bitmap.Height,
                Width = bitmap.Width
            };

            for (int width = 0; width < bitmap.Width; width++)
            {
                for (int height = 0; height < bitmap.Height; height++)
                {
                    color = bitmap.GetPixel(width, height);
                    if (color.B == blackColor)
                    {
                        roidataset.RoiPixels.Add(new RoiPixel(width, height));
                    }
                }
            }

            return roidataset;
        }
    }
}
