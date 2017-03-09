/*
 * BasicTextDataset.cs
 * Class representing dataset created from streaming an ordinary text 
 * file containing formatted data.
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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Spectre.Data.Structures;

namespace Spectre.Data.Datasets
{
    public class BasicTextDataset : IDataset
    {
        #region Fields
        /// <summary>
        /// Reader used for parsing the file.
        /// </summary>
        private StreamReader _streamReader;
        /// <summary>
        /// Array containing the dataset.
        /// </summary>
        private DataPoint[] _data;
        /// <summary>
        /// Metadata of the dataset.
        /// </summary>
        private Metadata _metadata;
        #endregion

        #region Constructors
        /// <summary>
        /// Base constructor, setting default metadata.
        /// </summary>
        public BasicTextDataset()
        {
            Metadata = Metadata.Default();
        }
        /// <summary>
        /// Constructor with file initialization.
        /// </summary>
        /// <param name="textFilePath">Path to the text file.</param>
        public BasicTextDataset(string textFilePath) : this()
        {
            LoadFromFile(textFilePath);
        }
        #endregion

        #region IDataset
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public Metadata Metadata
        {
            get
            {
                return _metadata;
            }
            set
            {
                _metadata = value;
                
            }
        }
        /// <summary>
        /// Method parsing the whole text file and initializing the array
        /// with found data. For now the data is assumed to be formatted in 
        /// following manner: "[mz] [intensity]" per single line of text.
        /// </summary>
        /// <param name="filePath">Path to the text file.</param>
        public void LoadFromFile(string filePath)
        {
            _streamReader = new StreamReader(filePath);

            //TODO: Setting metadata
            _metadata.X = 1;
            _metadata.Y = 1;
            _metadata.Z = 1;

            List<DataPoint> dataList = new List<DataPoint>();
            
            //TODO: Specifying formalized format of data in text files.
            string line;
            while ((line = _streamReader.ReadLine()) != null)
            {
                var strings = line.Split(null);
                try
                {
                    dataList.Add(new DataPoint(float.Parse(strings[0], CultureInfo.InvariantCulture),
                    float.Parse(strings[1], CultureInfo.InvariantCulture)));
                }
                catch (Exception)
                {
                    dataList.Add(new DataPoint());
                }
            }
            
            _data = dataList.ToArray();

        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mz"></param>
        public void LoadFromRawData(double[,] data, double[] mz)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public DataPoint this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }
        /// <summary>
        /// Method returning sub-array of loaded array of data.
        /// </summary>
        /// <param name="indexFrom">Starting index.</param>
        /// <param name="indexTo">Ending index.</param>
        /// <returns></returns>
        public DataPoint[] GetSub(uint indexFrom, uint indexTo)
        {
            if (indexFrom >= indexTo || indexTo >= _data.Length)
                throw new System.IndexOutOfRangeException();
            DataPoint[] subArray = new DataPoint[indexTo - indexTo];
            Array.Copy(_data, indexFrom, subArray, 0, indexTo - indexTo);
            return subArray;
        }
        #endregion
    }
}
