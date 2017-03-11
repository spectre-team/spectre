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

        //TODO: Because of not-so failsafe constructors, make the Datasets be eventually spitted out by some factory class.

        #region Fields

        /// <summary>
        /// Metadata of the dataset.
        /// </summary>
        private Metadata _metadata;

        /// <summary>
        /// Container for storing spacial coordinates for every loaded spectrum.
        /// </summary>
        private List<SpacialCoordinates> _spatialCoordinates;

        public List<SpacialCoordinates> SpacialCoordinates
        {
            get { return _spatialCoordinates; }
            set { _spatialCoordinates = value; }
        }
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
            set { _metadata = value; }
        }

        public void CreateFromFile(string filePath)
        {
            try
            {
                StreamReader sr = new StreamReader(filePath);
                var metadata = sr.ReadLine(); // global metadata
                var mzValues = sr.ReadLine()?.Split(null);
                _mz = new double[mzValues.Length];
                for (int i = 0; i < _mz.Length; i++)
                {
                    try
                    {
                        _mz[i] = double.Parse(mzValues[i], CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        _mz[i] = double.NaN;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("File " + filePath + " could not be read.", e);
            }

            _intensity = new List<double[]>();
            _spatialCoordinates = new List<SpacialCoordinates>();
            AppendFromFile(filePath);
        }

        public void CreateFromRawData(double[] mz, double[,] data)
        {
            if (mz == null || data == null)
                throw new InvalidDataException("The input data is null.");
            if (mz.Length != data.GetLength(1))
                throw new InvalidDataException("Length of the data must be equal to length of m/z values.");

            _intensity = new List<double[]>();
            _spatialCoordinates = new List<SpacialCoordinates>();
            _mz = mz;
            AppendFromRawData(data);
        }

        /// <summary>
        /// Method parsing the whole text file and initializing the array
        /// with found data.
        /// </summary>
        /// <param name="filePath">Path to the text file.</param>
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

                        try { x = int.Parse(metadata[0], CultureInfo.InvariantCulture); }
                        catch { x = -1; }
                        try { y = int.Parse(metadata[0], CultureInfo.InvariantCulture); }
                        catch { y = -1; }
                        try { z = int.Parse(metadata[0], CultureInfo.InvariantCulture); }
                        catch { z = -1; }

                        _spatialCoordinates.Add(new SpacialCoordinates(x, y, z));
                        _intensity.Add(new double[_mz.Length]);

                        int backIdx = _intensity.Count - 1;
                        for (int i = 0; i < intensities.Length; i++)
                        {
                            try
                            {
                                _intensity[backIdx][i] = double.Parse(intensities[i], CultureInfo.InvariantCulture);
                            }
                            catch (Exception)
                            {
                                _intensity[backIdx][i] = double.NaN;
                            }
                        }
                            
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
        /// <param name="data"></param>
        /// <param name="mz"></param>
        public void AppendFromRawData(double[,] data)
        {
            if(data == null)
                throw new InvalidDataException("The input data is null.");
            if(_mz.Length != data.GetLength(1))
                throw new InvalidDataException("The length of input data does not match the length of present data.");
            for (int i = 0; i < data.GetLength(0); i++)
            {
                _intensity.Add(new double[data.GetLength(1)]);
                _spatialCoordinates.Add(new SpacialCoordinates(-1, -1, -1));
                int backIdx = _intensity.Count - 1;
                for (int j = 0; j < data.GetLength(1); j++)
                    _intensity[backIdx][j] = data[i, j];
            }
        }

        public DataPoint this[int index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public DataPoint[] GetSub(uint indexFrom, uint indexTo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns length of single spectrum.
        /// </summary>
        /// <returns>Length of spectrum.</returns>
        public int GetSpectrumLength()
        {
            return _mz.Length;
        }

        public int GetSpectrumCount()
        {
            return _intensity.Count;
        }

        public double[] GetRawMzArray()
        {
            return _mz;
        }

        public double GetRawMzValue(int index)
        {
            return _mz[index];
        }

        public double GetRawIntensityValue(int spectrumIdx, int valueIdx)
        {
            return _intensity[spectrumIdx][valueIdx];
        }

        public double[] GetRawIntensityArray(int spectrumIdx)
        {
            return _intensity[spectrumIdx];
        }

        public double[] GetRawIntensityRow(int valueIdx)
        {
            double[] result = new double[_intensity.Count];
            for (int i = 0; i < _intensity.Count; i++)
                result[i] = _intensity[i][valueIdx];
            return result;
        }

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
