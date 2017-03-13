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
    public interface IDataset
    {
        //TODO: Somehow divide the available functionalities to the
        //TODO: external world (DataPoint accessible from higher
        //TODO: levels of abstraction, raw data for algorithms).
        //TODO: ...
        //TODO: Derive a sub-interface?


        #region Metadata
        /// <summary>
        /// Property containing metadata of the dataset.
        /// </summary>
        Metadata Metadata
        {
            get;
        }

        IEnumerable<SpatialCoordinates> SpacialCoordinates
        {
            get;
        }
        #endregion

        #region Data creation
        void CreateFromFile(string filePath);
        void CreateFromRawData(double[] mz, double[,] data);
        /// <summary>
        /// Abstract method for loading the dataset from a file.
        /// </summary>
        /// <param name="filePath">Path to a file.</param>
        void AppendFromFile(string filePath);
        /// <summary>
        /// Abstract method for loading the dataset from raw data arrays.
        /// </summary>
        /// <param name="data">Data representing intensities.</param>
        /// <param name="mz">Data reprezenting m/z values.</param>
        void AppendFromRawData(double[,] data);
        #endregion

        #region Data access

        DataPoint GetDataPoint(int spectrumIdx, int valueIdx);
        DataPoint[] GetDataPoints(int spectrumIdx, int valueIdxFrom, int valueIdxTo);
        int GetSpectrumLength();
        int GetSpectrumCount();
        #endregion

        #region Raw data access
        double[] GetRawMzArray();
        double GetRawMzValue(int index);
        double GetRawIntensityValue(int spectrumIdx, int valueIdx);
        double[] GetRawIntensityArray(int spectrumIdx);
        double[] GetRawIntensityRow(int valueIdx);
        double[,] GetRawIntensityRange(int spectrumIdxFrom, int spectrumIdxTo, int valueIdxFrom, int valueIdxTo);
        #endregion
    }
}
