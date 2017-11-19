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
    /// <seealso cref="IRoiUtilites" />
    public class RoiUtilities : IRoiUtilites
    {
        /// <summary>
        /// Reads regions of interest from a png file.
        /// </summary>
        /// <param name="path">Path for the rois directory, either Spectre.Data or test_files director.</param>
        /// <returns>
        /// Returns list doubles.
        /// </returns>
        public RoiDataset RoiReader(string path)
        {
            Color clr = default(Color);
            Bitmap bitmap = new Bitmap(path);
            RoiDataset roidataset = new RoiDataset();

            roidataset.RoiPixels = new List<RoiPixel>();
            roidataset.Name = Path.GetFileNameWithoutExtension(path);
            roidataset.Height = bitmap.Height;
            roidataset.Width = bitmap.Width;

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
        /// <param name="prototyp">The prototyp.</param>
        /// <param name="path">The path.</param>
        public void RoiWriter(RoiDataset prototyp, string path)
        {
            Bitmap bitmap = new Bitmap(prototyp.Width, prototyp.Height);

            Graphics graphicsobject = Graphics.FromImage(bitmap);
            graphicsobject.Clear(Color.White);

            for (int listiterator = 0; listiterator < prototyp.RoiPixels.Count; listiterator++)
            {
                bitmap.SetPixel(
                   prototyp.RoiPixels[listiterator].GetXCoord(),
                   prototyp.RoiPixels[listiterator].GetYCoord(),
                   Color.Black);
            }

            var writepath = path + "\\" + prototyp.Name + ".png";
            bitmap.Save(writepath, ImageFormat.Png);
        }

        /// <summary>
        /// Lists the rois from directory.
        /// </summary>
        /// <param name="path">Path for the rois directory, either Spectre.Data or test_files director.</param>
        /// <returns>
        /// Names of all roi files in the directory.
        /// </returns>
        public List<string> ListRoisFromDirectory(string path)
        {
            List<string> names = new List<string>();

            string[] allfiles = System.IO.Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);

            names = allfiles.ToList();

            if (names.Count == 0)
            {
                throw new FileNotFoundException("No *.png files found in directory or subdirectories");
            }

            return names;
        }
    }
}