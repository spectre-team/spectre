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
    /// <summary>
    /// Contains representation for single measurement from MALDI spectrometry for single XY coordinate.
    /// </summary>
    public class DataPoint
    {
        #region Fields

        /// <summary>
        /// Value of m/z.
        /// </summary>
        public double Mz { get; }
        /// <summary>
        /// Value of intensity.
        /// </summary>
        public double Intensity { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DataPoint"/> class
        /// with user defined values of m/z and intensity.
        /// </summary>
        /// <param name="mz">Value of m/z.</param>
        /// <param name="intensity">Value of intensity.</param>
        public DataPoint(double mz, double intensity)
        {
            Mz = mz;
            Intensity = intensity;
        }
        #endregion
    }
}
