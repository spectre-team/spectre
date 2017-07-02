/*
 * IDataset.cs
 * Contains interface class for basic functionalities of dataset 
 * representing measurements from single sample point
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

using System.Collections.Generic;
using Spectre.Data.Structures;

namespace Spectre.Data.Datasets
{
    /// <summary>
    /// Contains interface for basic functionalities of dataset representing measurements from single sample point.
    /// </summary>
    public interface IDataset
    {
        #region Metadata
        /// <summary>
        /// Property containing metadata of the dataset.
        /// </summary>
        Metadata Metadata
        {
            get;
        }

        /// <summary>
        /// Property for storing spacial coordinates for spectras in dataset.
        /// </summary>
        IEnumerable<SpatialCoordinates> SpatialCoordinates
        {
            get;
        }

        /// <summary>
        /// Property containing number of values in a single spectrum.
        /// </summary>
        int SpectrumLength { get; }

        /// <summary>
        /// Property containing amount of spectra existing in dataset.
        /// </summary>
        int SpectrumCount { get; }
        #endregion

        #region Data creation
        /// <summary>
        /// Method for creating new dataset from file, overwriting current data.
        /// </summary>
        /// <param name="filePath">Path to a file.</param>
        void CreateFromFile(string filePath);

        /// <summary>
        /// Method for creating new dataset from raw data, overwriting current data.
        /// </summary>
        /// <param name="mz">Array of m/z values.</param>
        /// <param name="data">Multidimensional array of intensity values.</param>
        /// <param name="coordinates">Spatial coordinates of input spectra.</param>
        void CreateFromRawData(double[] mz, double[,] data, int[,] coordinates);
        /// <summary>
        /// Method for appending new data from file.
        /// </summary>
        /// <param name="filePath">Path to a file.</param>
        void AppendFromFile(string filePath);

        /// <summary>
        /// Method for appending new data from raw arrays.
        /// </summary>
        /// <param name="data">Multidimensional array of intensity values.</param>
        /// <param name="coordinates">Spatial coordinates of input spectra.</param>
        void AppendFromRawData(double[,] data, int[,] coordinates);
        #endregion

        #region Data access
        /// <summary>
        /// Method creating <see cref="DataPoint"/> structure from dataset.
        /// </summary>
        /// <param name="spectrumIdx">Index of spectrum.</param>
        /// <param name="valueIdx">Index of value.</param>
        /// <returns>Created <see cref="DataPoint"/> structure.</returns>
        DataPoint GetDataPoint(int spectrumIdx, int valueIdx);
        /// <summary>
        /// Method creating array of <see cref="DataPoint"/> structures from dataset.
        /// </summary>
        /// <param name="spectrumIdx">Index of spectrum.</param>
        /// <param name="valueIdxFrom">Beginning value index.</param>
        /// <param name="valueIdxTo">Ending value index.</param>
        /// <returns>Created <see cref="DataPoint"/> array.</returns>
        DataPoint[] GetDataPoints(int spectrumIdx, int valueIdxFrom, int valueIdxTo);
        /// <summary>
        /// Method returning spatial coordinates of given spectrum.
        /// </summary>
        /// <param name="spectrumIdx">Index of spectrum.</param>
        /// <returns>Spatial coordinates of given spectrum.</returns>
        SpatialCoordinates GetSpatialCoordinates(int spectrumIdx);
        #endregion

        #region Raw data access
        /// <summary>
        /// Getter for whole array of raw m/z values used in dataset.
        /// </summary>
        /// <returns>Array of all m/z values.</returns>
        double[] GetRawMzArray();
        /// <summary>
        /// Getter for single raw m/z value used in dataset.
        /// </summary>
        /// <param name="index">Index of value.</param>
        /// <returns>Value of m/z at specified index.</returns>
        double GetRawMzValue(int index);
        /// <summary>
        /// Getter for single raw intensity value present in dataset.
        /// </summary>
        /// <param name="spectrumIdx">Index of spectrum.</param>
        /// <param name="valueIdx">Index of value.</param>
        /// <returns>Value of intensity in specified spectrum at specified position.</returns>
        double GetRawIntensityValue(int spectrumIdx, int valueIdx);
        /// <summary>
        /// Getter for whole array of raw intensity values for given spectrum.
        /// </summary>
        /// <param name="spectrumIdx">Index of spectrum.</param>
        /// <returns>Array of intensities in specified spectrum.</returns>
        double[] GetRawIntensityArray(int spectrumIdx);
        /// <summary>
        /// Getter for row of intensities at given value position from all spectra present in dataset.
        /// </summary>
        /// <param name="valueIdx">Index of value.</param>
        /// <returns>Array of intensity values from all spectra at given value position.</returns>
        double[] GetRawIntensityRow(int valueIdx);
        /// <summary>
        /// Getter for arbitrary range of intensities picked from inside specified boundaries.
        /// </summary>
        /// <param name="spectrumIdxFrom">Starting spectrum index.</param>
        /// <param name="spectrumIdxTo">Ending spectrum index.</param>
        /// <param name="valueIdxFrom">Starting value index.</param>
        /// <param name="valueIdxTo">Ending value index.</param>
        /// <returns>Multidimensional array of intensities.</returns>
        double[,] GetRawIntensityRange(int spectrumIdxFrom, int spectrumIdxTo, int valueIdxFrom, int valueIdxTo);
        /// <summary>
        /// Getter for all the intensity values from all spectra from dataset.
        /// </summary>
        /// <returns>Multidimensional array of all the intensities present in dataset.</returns>
        double[,] GetRawIntensities();
        /// <summary>
        /// Getter for spatial coordinates of all spectra present in dataset.
        /// </summary>
        /// <param name="is2D"></param>
        /// <returns>Multidimensional array of spatial coordinates.</returns>
        int[,] GetRawSpacialCoordinates(bool is2D);
        #endregion

        #region File storage
        /// <summary>
        /// Saves dataset to file using the default format.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        void SaveToFile(string path);
        #endregion
    }
}
