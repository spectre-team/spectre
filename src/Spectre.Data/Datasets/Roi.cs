/*
 * Roi.cs
 * Class represents a region over an image.

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

namespace Spectre.Data.Datasets
{
    /// <summary>
    /// Dataset containing information about a single roi.
    /// </summary>
    public class Roi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Roi" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="roiPixels">The roipixel.</param>
        public Roi(string name, int width, int height, IList<RoiPixel> roiPixels)
        {
            Name = name;
            Width = width;
            Height = height;

            if (roiPixels.Any(r => r.XCoordinate > width) || roiPixels.Any(r => r.YCoordinate > height))
            {
                throw new ArgumentOutOfRangeException("Given roi pixels cannot exceed specified dimensions.");
            }
            else
            {
                RoiPixels = roiPixels;
            }
        }

        /// <summary>
        /// Gets or sets the roi pixels.
        /// </summary>
        /// <value>
        /// The roi pixels.
        /// </value>
        public IList<RoiPixel> RoiPixels { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; }
    }
}