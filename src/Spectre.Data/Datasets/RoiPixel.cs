/*
 * Roi.cs
 * Class represeting dataset with data of regions of interest.

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

namespace Spectre.Data.Datasets
{
    /// <summary>
    /// Dataset for regions of interest.
    /// </summary>
    public class RoiPixel
    {
        /// <summary>
        /// The x coordinate
        /// </summary>
        private readonly int _xcoordinate;

        /// <summary>
        /// The ycoordinate
        /// </summary>
        private readonly int _ycoordinate;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoiPixel" /> class.
        /// </summary>
        /// <param name="xcoord">The xcoord.</param>
        /// <param name="ycoord">The ycoord.</param>
        public RoiPixel(int xcoord, int ycoord)
        {
            _xcoordinate = xcoord;
            _ycoordinate = ycoord;
        }

        /// <summary>
        /// Gets the x coord.
        /// </summary>
        /// <returns>X coordinate of the object</returns>
        public int GetXCoord() => _xcoordinate;

        /// <summary>
        /// Gets the y coord.
        /// </summary>
        /// <returns>Y Coordinate of the object</returns>
        public int GetYCoord() => _ycoordinate;
        }
}