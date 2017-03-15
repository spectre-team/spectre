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
using System.IO;
using Spectre.Data.Structures;

namespace Spectre.Data.Datasets
{
    public class BasicTextDataset : IDataset
    {

        //TODO: Because of not-so failsafe constructors, make the Datasets 
        //TODO: be eventually spitted out by some factory class!!!

        #region Fields

        /// <summary>
        /// Metadata of the dataset.
        /// </summary>
        private Metadata _metadata;

        /// <summary>
        /// Container for storing spacial coordinates for every loaded spectrum.
        /// </summary>
        private List<SpatialCoordinates> _spatialCoordinates;

        /// <summary>
        /// Array of m/z values for all the spectras.
        /// </summary>
        private double[] _mz;

        /// <summary>
        /// Container for storing intensity values for every loaded spectrum.
        /// </summary>
        private List<double[]> _intensity;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with file initialization.
        /// </summary>
        /// <param name="textFilePath">Path to the text file.</param>
        public BasicTextDataset(string textFilePath)
        {
            CreateFromFile(textFilePath);
        }
        /// <summary>
        /// Constructor with raw value initialization.
        /// </summary>
        /// <param name="mz">Array of m/z values.</param>
        /// <param name="data">Array of intensity values.</param>
        public BasicTextDataset(double[] mz, double[,] data)
        {
            CreateFromRawData(mz, data);
        }

        #endregion

        #region IDataset

        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public Metadata Metadata
        {
            get { return _metadata; }
            private set { _metadata = value; }
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public IEnumerable<SpatialCoordinates> SpacialCoordinates
        {
            get { return _spatialCoordinates; }
            private set { _spatialCoordinates = value as List<SpatialCoordinates>; }
        }
        /// <summary>
        /// Method for creating new dataset from text file, overwriting current data.
        /// </summary>
        /// <param name="filePath">Path to a text file.</param>
        /// <exception cref="Exception">Thrown where there is a problem with file loading.</exception>
        public void CreateFromFile(string filePath)
        {
            try
            {
                StreamReader sr = new StreamReader(filePath);
                var metadata = sr.ReadLine(); // global metadata
                var mzValues = sr.ReadLine()?.Split(null);
                _mz = new double[mzValues.Length];
                for (int i = 0; i < _mz.Length; i++)
                    if (!double.TryParse(mzValues[i], out _mz[i]))
                        _mz[i] = double.NaN;
            }
            catch (Exception e)
            {
                throw new Exception("File " + filePath + " could not be read.", e);
            }

            _intensity = new List<double[]>();
            _spatialCoordinates = new List<SpatialCoordinates>();
            AppendFromFile(filePath);
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        /// <exception cref="InvalidDataException">Throws when the data is null or 
        /// the length of data is not matching the dataset length.</exception>
        public void CreateFromRawData(double[] mz, double[,] data)
        {
            if (mz == null || data == null)
                throw new InvalidDataException("The input data is null.");
            if (mz.Length != data.GetLength(1))
                throw new InvalidDataException("Length of the data must be equal to length of m/z values.");

            _intensity = new List<double[]>();
            _spatialCoordinates = new List<SpatialCoordinates>();
            _mz = mz;
            AppendFromRawData(data);
        }

        /// <summary>
        /// Method parsing the whole text file and initializing the array
        /// with found data.
        /// </summary>
        /// <param name="filePath">Path to the text file.</param>
        /// <exception cref="InvalidDataException">Thrown where there is a problem with file parsing.</exception>
        public void AppendFromFile(string filePath)
        {
            //TODO: Specifying formalized format of data in text files.
            //TODO: Safety check, format check
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    sr.ReadLine(); // omit global metadata
                    sr.ReadLine(); // omit m/z values
                    while (sr.Peek() > -1)
                    {
                        var metadata = sr.ReadLine()?.Split(null);
                        var intensities = sr.ReadLine()?.Split(null);
                        if (metadata == null || intensities == null)
                            continue;
                        if (intensities.Length != _mz.Length)
                            throw new InvalidDataException("Length of the data must be equal to length of m/z values.");

                        int x, y, z;

                        if (!int.TryParse(metadata[0], out x))
                            x = -1;
                        if (!int.TryParse(metadata[1], out y))
                            y = -1;
                        if (!int.TryParse(metadata[2], out z))
                            z = -1;

                        _spatialCoordinates.Add(new SpatialCoordinates(x, y, z));
                        _intensity.Add(new double[_mz.Length]);

                        int backIdx = _intensity.Count - 1;
                        for (int i = 0; i < intensities.Length; i++)
                            if (!double.TryParse(intensities[i], out _intensity[backIdx][i]))
                                _intensity[backIdx][i] = double.NaN;
                            
                    }
                }
            }
            catch (Exception e)
            {
                throw new InvalidDataException("Error while parsing " + filePath + " file.", e);
            }
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        /// <exception cref="InvalidDataException">Throws when the data is null or 
        /// the length of data is not matching the dataset length.</exception>
        public void AppendFromRawData(double[,] data)
        {
            if(data == null)
                throw new InvalidDataException("The input data is null.");
            if(_mz.Length != data.GetLength(1))
                throw new InvalidDataException("The length of input data does not match the length of present data.");
            for (int i = 0; i < data.GetLength(0); i++)
            {
                _intensity.Add(new double[data.GetLength(1)]);
                _spatialCoordinates.Add(new SpatialCoordinates(-1, -1, -1));
                int backIdx = _intensity.Count - 1;
                for (int j = 0; j < data.GetLength(1); j++)
                    _intensity[backIdx][j] = data[i, j];
            }
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public DataPoint GetDataPoint(int spectrumIdx, int valueIdx)
        {
            return new DataPoint(_mz[valueIdx], _intensity[spectrumIdx][valueIdx]);
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public DataPoint[] GetDataPoints(int spectrumIdx, int valueIdxFrom, int valueIdxTo)
        {
            if (valueIdxFrom >= valueIdxTo)
                throw new IndexOutOfRangeException();
            int valueCnt = valueIdxTo - valueIdxFrom;

            DataPoint[] result = new DataPoint[valueCnt];
            for (int i = 0; i < valueCnt; i++)
                result[i] = new DataPoint(_mz[valueIdxFrom + i], _intensity[spectrumIdx][valueIdxFrom + i]);
            return result;
        }

        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public int GetSpectrumLength()
        {
            return _mz.Length;
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public int GetSpectrumCount()
        {
            return _intensity.Count;
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public double[] GetRawMzArray()
        {
            return _mz;
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public double GetRawMzValue(int index)
        {
            return _mz[index];
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public double GetRawIntensityValue(int spectrumIdx, int valueIdx)
        {
            return _intensity[spectrumIdx][valueIdx];
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public double[] GetRawIntensityArray(int spectrumIdx)
        {
            return _intensity[spectrumIdx];
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public double[] GetRawIntensityRow(int valueIdx)
        {
            double[] result = new double[_intensity.Count];
            for (int i = 0; i < _intensity.Count; i++)
                result[i] = _intensity[i][valueIdx];
            return result;
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public double[,] GetRawIntensityRange(int spectrumIdxFrom, int spectrumIdxTo, int valueIdxFrom, int valueIdxTo)
        {
            if (spectrumIdxFrom >= spectrumIdxTo || valueIdxFrom >= valueIdxTo)
                throw new IndexOutOfRangeException();
            int spectrumCnt = spectrumIdxTo - spectrumIdxFrom;
            int valueCnt = valueIdxTo - valueIdxFrom;

            double[,] result = new double[spectrumCnt,valueCnt];

            for (int i = 0; i < spectrumCnt; i++)
                for (int j = 0; j < valueCnt; j++)
                    result[i, j] = _intensity[spectrumIdxFrom + i][valueIdxFrom + j];

            return result;
        }
        #endregion
    }
}
