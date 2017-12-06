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
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using Spectre.Data.Datasets;

    /// <summary>
    /// Class with utilities for managing the regions of interest data.
    /// </summary>
    /// <seealso cref="Spectre.Data.RoiIo.IRoiUtilites" />
    public class RoiUtilities : IRoiUtilites
    {
        /// <summary>
        /// The path
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoiUtilities"/> class.
        /// </summary>
        public RoiUtilities()
        {
            _path = System.IO.Path.GetFullPath(@"..\..\..\");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoiUtilities" /> class.
        /// Used to pass exact path to methods. Default constructor sets project directory
        /// as current path.
        /// </summary>
        /// <param name="testpath">The testpath.</param>
        public RoiUtilities(string testpath)
        {
            _path = testpath;
        }

        /// <summary>
        /// Reads regions of interest from a png file.
        /// </summary>
        /// <returns>
        /// Returns list doubles.
        /// </returns>
        public RoiDataset RoiReader()
        {
            var clr = default(Color);
            var bitmap = new Bitmap(_path);
            var roidataset = new RoiDataset
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
                    clr = bitmap.GetPixel(width, height);
                    if (clr.B == 0)
                    {
                        roidataset.RoiPixels.Add(new RoiPixel(width, height));
                    }
                }
            }

            return roidataset;
        }

        /// <summary>
        /// Writes list of doubles into a png file.
        /// </summary>
        /// <param name="roidataset">The prototyp.</param>
        public void RoiWriter(RoiDataset roidataset)
        {
            var bitmap = new Bitmap(roidataset.Width, roidataset.Height);

            var graphicsobject = Graphics.FromImage(bitmap);
            graphicsobject.Clear(Color.White);

            for (int listiterator = 0; listiterator < roidataset.RoiPixels.Count; listiterator++)
            {
                bitmap.SetPixel(
                   roidataset.RoiPixels[listiterator].GetXCoord(),
                   roidataset.RoiPixels[listiterator].GetYCoord(),
                   Color.Black);
            }

            var writepath = Path.GetFullPath(Path.Combine(_path, roidataset.Name));

            bitmap.Save(writepath + ".png", ImageFormat.Png);
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
    }
}