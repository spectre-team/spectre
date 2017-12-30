/*
 * IRoiConverter.cs
 * Interface for RoiConverter.

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
    using System.Drawing;
    using Spectre.Data.Datasets;

    /// <summary>
    /// Interface for RoiConverter.
    /// </summary>
    public interface IRoiConverter
    {
        /// <summary>
        /// Bitmap to roi converter.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="name">The name.</param>
        /// <returns>
        /// Roi dataset.
        /// </returns>
        Roi BitmapToRoi(Bitmap bitmap, string name);

        /// <summary>
        /// ROIs to bitmap converter.
        /// </summary>
        /// <param name="roidataset">The roidataset.</param>
        /// <returns>
        /// Bitmap.
        /// </returns>
        Bitmap RoiToBitmap(Roi roidataset);
    }
}
