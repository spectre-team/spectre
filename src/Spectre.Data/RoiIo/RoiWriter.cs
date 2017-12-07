/*
 * RoiWriter.cs
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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Data.Datasets;

namespace Spectre.Data.RoiIo
{
    /// <summary>
    /// Writes list of doubles into a png file.
    /// </summary>
    public class RoiWriter
    {
        /// <summary>
        /// The path
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoiWriter"/> class.
        /// </summary>
        public RoiWriter()
        {
            _path = System.IO.Path.GetFullPath(@"..\..\..\");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoiWriter" /> class.
        /// Used to pass exact path to methods. Default constructor sets project directory
        /// as current path.
        /// </summary>
        /// <param name="testpath">The testpath.</param>
        public RoiWriter(string testpath)
        {
            _path = testpath;
        }

        /// <summary>
        /// Writes list of doubles into a png file.
        /// </summary>
        /// <param name="roidataset">The prototyp.</param>
        public void RoiUploader(Roi roidataset)
        {
            var bitmap = new Bitmap(roidataset.Width, roidataset.Height);

            var graphicsobject = Graphics.FromImage(bitmap);
            graphicsobject.Clear(Color.White);

            for (int listiterator = 0; listiterator < roidataset.RoiPixels.Count; listiterator++)
            {
                bitmap.SetPixel(
                    roidataset.RoiPixels[listiterator].XCoordinate,
                    roidataset.RoiPixels[listiterator].YCoordinate,
                    Color.Black);
            }

            var writepath = Path.GetFullPath(Path.Combine(_path, roidataset.Name));

            bitmap.Save(writepath + ".png", ImageFormat.Png);
        }
    }
}
