/*
 * DataPoint.cs
 * Contains representation for single measurement from MALDI
 * spectrometry for single XY coordinate.
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
    public class DataPoint
    {
        #region Fields
        /// <summary>
        /// Spacial coordinate X.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Spacial coordinate Y.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Spacial coordinate Z.
        /// </summary>
        public int Z { get; set; }
        /// <summary>
        /// Value of m/z.
        /// </summary>
        public double Mz { get; set; }
        /// <summary>
        /// Value of intensity
        /// </summary>
        public double Intensity { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DataPoint"/> class 
        /// with NaN values.
        /// </summary>
        public DataPoint()
        {
            X = -1;
            Y = -1;
            Z = -1;
            Mz = double.NaN;
            Intensity = double.NaN;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataPoint"/> class
        /// with user defined values of m/z and intensity.
        /// </summary>
        /// <param name="mz">Value of m/z.</param>
        /// <param name="intensity">Value of intensity.</param>
        public DataPoint(double mz, double intensity)
        {
            X = -1;
            Y = -1;
            Z = -1;
            Mz = mz;
            Intensity = intensity;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataPoint"/> class
        /// with user defined values of spacial data.
        /// </summary>
        /// <param name="x">Spatial coordinate X.</param>
        /// <param name="y">Spatial coordinate Y.</param>
        /// <param name="z">Spatial coordinate Z.</param>
        public DataPoint(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            Mz = double.NaN;
            Intensity = double.NaN;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataPoint"/> class
        /// with user defined values of m/z, intensity and spacial data.
        /// </summary>
        /// <param name="mz">Value of m/z.</param>
        /// <param name="intensity">Value of intensity.</param>
        /// <param name="x">Spatial coordinate X.</param>
        /// <param name="y">Spatial coordinate Y.</param>
        /// <param name="z">Spatial coordinate Z.</param>
        public DataPoint(double mz, double intensity, int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            Mz = mz;
            Intensity = intensity;
        }
        #endregion
    }
}
