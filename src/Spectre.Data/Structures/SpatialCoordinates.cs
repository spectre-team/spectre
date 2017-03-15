/*
 * SpatialCoordinates.cs
 * Struct used for storing the spacial coordinates of a spectrum.
 * 
   Copyright 2017 Dariusz Kuchta

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

namespace Spectre.Data.Structures
{
    public struct SpatialCoordinates
    {
        public int X;
        public int Y;
        public int Z;

        /// <summary>
        /// Initializes a new instance of <see cref="SpatialCoordinates"/> struct with
        /// user defined values of spatial coordinates.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public SpatialCoordinates(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
