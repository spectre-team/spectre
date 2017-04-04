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
    /// <summary>
    /// Contains struct used for storing the spacial coordinates of a spectrum.
    /// </summary>
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
        /// <summary>
        /// Returns raw form of spatial coordintates in order X, Y, Z.
        /// </summary>
        /// <returns>Array of values representing spatial coordinates.</returns>
        public int[] GetRaw()
        {
            return new [] {X, Y, Z};
        } 

        /// <summary>
        /// Converts spatial coordinates into space-separated string of x, y and z values
        /// </summary>
        /// <returns>String of form "x y z"</returns>
        public override string ToString()
        {
            return X.ToString() + ' ' + Y.ToString() + ' ' + Z.ToString();
        }
    }
}
