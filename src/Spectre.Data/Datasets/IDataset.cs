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

using Spectre.Data.Structures;

namespace Spectre.Data.Datasets
{
    public interface IDataset
    {
        #region Metadata
        /// <summary>
        /// Property containing metadata of the dataset.
        /// </summary>
        Metadata Metadata
        {
            get;
            set;
        }
        #endregion

        #region Data creation
        /// <summary>
        /// Abstract method for loading the dataset from a file.
        /// </summary>
        /// <param name="filePath">Path to a file.</param>
        void LoadFromFile(string filePath);
        /// <summary>
        /// Abstract method for loading the dataset from raw data arrays.
        /// </summary>
        /// <param name="data">Data representing intensities.</param>
        /// <param name="mz">Data reprezenting m/z values.</param>
        void LoadFromRawData(double[,] data, double[] mz);
        #endregion

        #region Data access
        /// <summary>
        /// Returns measurement at given index.
        /// </summary>
        /// <param name="index">Input index.</param>
        /// <returns></returns>
        DataPoint this[int index]
        {
            get;
            set;
        }
        /// <summary>
        /// Returns array of measurements at given range.
        /// </summary>
        /// <param name="indexFrom"></param>
        /// <param name="indexTo"></param>
        /// <returns></returns>
        DataPoint[] GetSub(uint indexFrom, uint indexTo);
        #endregion

    }
}
