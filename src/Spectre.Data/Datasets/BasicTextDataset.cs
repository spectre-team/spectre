/*
 * BasicTextDataset.cs
 * Class representing dataset created from streaming an ordinary text 
 * file containing formatted data.
 * 
   Copyright 2017 Dariusz Kuchta, Michał Gallus

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
using System.Linq;
using System.Text;
using System.Threading;
using Spectre.Data.Structures;

namespace Spectre.Data.Datasets
{
    /// <summary>
    /// Class representing dataset created from streaming an ordinary text file containing formatted data.
    /// </summary>
    public class BasicTextDataset : IDataset
    {
        #region Fields

        /// <summary>
        /// Metadata of the dataset.
        /// </summary>
        private Metadata _metadata;

        /// <summary>
        /// Container for storing spatial coordinates for every loaded spectrum.
        /// </summary>
        private List<SpatialCoordinates> _spatialCoordinates;

        /// <summary>
        /// Array of m/z values for all the spectras.
        /// </summary>
        private double[] _mz;

        /// <summary>
        /// Container for storing intensity values for every loaded spectrum.
        /// </summary>
        private List<double[]> _intensityArray;

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
        /// <param name="coordinates">Array of spatial coordinates.</param>
        public BasicTextDataset(double[] mz, double[,] data, int[,] coordinates = null)
        {
            CreateFromRawData(mz, data, coordinates);
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
        public IEnumerable<SpatialCoordinates> SpatialCoordinates
        {
            get { return _spatialCoordinates; }
            private set { _spatialCoordinates = value.ToList(); }
        }

        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public int SpectrumLength => _mz.Length;

        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public int SpectrumCount => _intensityArray.Count;

        /// <summary>
        /// Method for creating new dataset from text file, overwriting current data.
        /// </summary>
        /// <param name="filePath">Path to a text file.</param>
        /// <exception cref="Exception">Thrown where there is a problem with file loading.</exception>
        public void CreateFromFile(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    var metadata = sr.ReadLine(); // global metadata
                    var mzValues = sr.ReadLine().Split(null);
                    mzValues = mzValues.Where(str => !string.IsNullOrEmpty(str)).ToArray();
                    if (mzValues.Length < 1)
                        throw new InvalidDataException("No m/z values found.");
                    _mz = new double[mzValues.Length];
                    for (int i = 0; i < _mz.Length; i++)
                        if (!double.TryParse(mzValues[i], NumberStyles.Any, CultureInfo.InvariantCulture, out _mz[i]))
                            _mz[i] = double.NaN;
                }
            }
            catch (NullReferenceException e)
            {
                throw new IOException("M/z data could not be parsed from file " + filePath + ".", e);
            }
            catch (InvalidDataException e)
            {
                throw new IOException("M/z array parsed from file " + filePath + " is empty.", e);
            }
            catch (Exception e) //  catch remaining Exceptions that are related to StreamReader
            {
                throw new IOException("Streamer failed to read " + filePath + " file.", e);
            }

            _intensityArray = new List<double[]>();
            _spatialCoordinates = new List<SpatialCoordinates>();
            AppendFromFile(filePath);
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        /// <exception cref="InvalidDataException">Throws when the data is null or 
        /// the length of data is not matching the dataset length.</exception>
        public void CreateFromRawData(double[] mz, double[,] data, int[,] coordinates = null)
        {
            if (mz == null || data == null)
                throw new InvalidDataException("The input data is null.");
            if (coordinates != null && coordinates.GetLength(0) != data.GetLength(0))
                throw new InvalidDataException("Amount of input spectra does not match the amount of spatial coordinates.");
            if (mz.Length != data.GetLength(1))
                throw new InvalidDataException("Length of the data must be equal to length of m/z values.");

            _intensityArray = new List<double[]>();
            _spatialCoordinates = new List<SpatialCoordinates>();
            _mz = mz;
            AppendFromRawData(data, coordinates);
        }

        /// <summary>
        /// Method parsing the whole text file and initializing the array
        /// with found data.
        /// </summary>
        /// <param name="filePath">Path to the text file.</param>
        /// <exception cref="InvalidDataException">Thrown where there is a problem with file parsing.</exception>
        public void AppendFromFile(string filePath)
        {
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
                        intensities = intensities.Where(str => !string.IsNullOrEmpty(str)).ToArray();
                        if (metadata.Length == 0 || intensities.Length == 0)
                            continue;
                        if (intensities.Length != _mz.Length)
                            throw new InvalidDataException("Length of the data must be equal to length of m/z values.");

                        int x, y, z;

                        if (!int.TryParse((metadata.Length>0)?metadata[0]:null, NumberStyles.Any, CultureInfo.InvariantCulture, out x))
                            x = -1;
                        if (!int.TryParse((metadata.Length>1)?metadata[1]:null, NumberStyles.Any, CultureInfo.InvariantCulture, out y))
                            y = -1;
                        if (!int.TryParse((metadata.Length>2)?metadata[2]:null, NumberStyles.Any, CultureInfo.InvariantCulture, out z))
                            z = -1;

                        _spatialCoordinates.Add(new SpatialCoordinates(x, y, z));
                        _intensityArray.Add(new double[_mz.Length]);

                        int backIdx = _intensityArray.Count - 1;
                        for (int i = 0; i < intensities.Length; i++)
                            if (!double.TryParse(intensities[i], NumberStyles.Any, CultureInfo.InvariantCulture, out _intensityArray[backIdx][i]))
                                _intensityArray[backIdx][i] = double.NaN;
                            
                    }
                }
            }
            catch(InvalidDataException e)
            {
                throw new IOException("Length mismatch in parsed data.", e);
            }
            catch (Exception e) //  catch remaining Exceptions that are related to StreamReader
            {
                throw new IOException("Streamer failed to read " + filePath + " file.", e);
            }
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        /// <exception cref="InvalidDataException">Throws when the data is null or 
        /// the length of data is not matching the dataset length.</exception>
        public void AppendFromRawData(double[,] data, int[,] coordinates = null)
        {
            if(data == null)
                throw new InvalidDataException("The input data is null.");
            if (coordinates != null && coordinates.GetLength(0) != data.GetLength(0))
                throw new InvalidDataException("Amount of input spectra does not match the amount of spatial coordinates.");
            if (_mz.Length != data.GetLength(1))
                throw new InvalidDataException("The length of input data does not match the length of present data.");

            int coordinateDimensions = coordinates?.GetLength(1) ?? 0;

            for (int i = 0; i < data.GetLength(0); i++)
            {
                int[] xyz = { -1, -1, -1 };
                for (int j = 0; j < coordinateDimensions; j++)
                    xyz[j] = coordinates[i, j];
                _spatialCoordinates.Add(new SpatialCoordinates(xyz[0], xyz[1], xyz[2]));

                _intensityArray.Add(new double[data.GetLength(1)]);
                int backIdx = _intensityArray.Count - 1;
                for (int j = 0; j < data.GetLength(1); j++)
                    _intensityArray[backIdx][j] = data[i, j];
            }
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public DataPoint GetDataPoint(int spectrumIdx, int valueIdx)
        {
            return new DataPoint(_mz[valueIdx], _intensityArray[spectrumIdx][valueIdx]);
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
                result[i] = new DataPoint(_mz[valueIdxFrom + i], _intensityArray[spectrumIdx][valueIdxFrom + i]);
            return result;
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public SpatialCoordinates GetSpatialCoordinates(int spectrumIdx)
        {
            return _spatialCoordinates[spectrumIdx];
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
            return _intensityArray[spectrumIdx][valueIdx];
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public double[] GetRawIntensityArray(int spectrumIdx)
        {
            return _intensityArray[spectrumIdx];
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public double[] GetRawIntensityRow(int valueIdx)
        {
            double[] result = new double[_intensityArray.Count];
            for (int i = 0; i < _intensityArray.Count; i++)
                result[i] = _intensityArray[i][valueIdx];
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
                    result[i, j] = _intensityArray[spectrumIdxFrom + i][valueIdxFrom + j];

            return result;
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public double[,] GetRawIntensities()
        {
            return GetRawIntensityRange(0, SpectrumCount, 0, SpectrumLength);
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public int[,] GetRawSpacialCoordinates(bool is2D)
        {
            int dimensions = (is2D) ? 2 : 3;

            int[,] result = new int[SpectrumCount,dimensions];
            for(int i = 0; i < SpectrumCount; i++)
            {
                int[] xyz = _spatialCoordinates[i].GetRaw();
                for (int j = 0; j < dimensions; j++)
                    result[i, j] = xyz[j];
            }

            return result;
        }
        /// <summary>
        /// See <see cref="IDataset"/> for description.
        /// </summary>
        public void SaveToFile(string path)
        {
            StringBuilder fileBuilder = new StringBuilder();

            // fixes comma-instead-of-dot related issues
            BeginInvariantCulture();
            {
                AppendMetadata(fileBuilder);
                AppendMzValues(fileBuilder);
                AppendIntensitiesAndCoordinates(fileBuilder);

                SaveDataToFile(path, fileBuilder);
            }
            EndInvariantCulture();
        }
        #endregion

        #region Private methods

        private void SaveDataToFile(string path, StringBuilder fileBuilder)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(fileBuilder.ToString());
            }
        }

        private void AppendIntensitiesAndCoordinates(StringBuilder fileBuilder)
        {
            StringBuilder spectrumBuilder = new StringBuilder();
            SpatialCoordinates[] coordinates = _spatialCoordinates.ToArray();
            double[][] intensities = _intensityArray.ToArray();

            for (int i = 0; i < intensities.Length; i++)
            {
                fileBuilder.AppendLine(coordinates[i].ToString());
                for (int j = 0; j < intensities[0].Length; j++)
                {
                    spectrumBuilder.Append(intensities[i][j]);
                    spectrumBuilder.Append(' ');
                }
                fileBuilder.AppendLine(spectrumBuilder.ToString());
                spectrumBuilder.Clear();
            }

        }

        private void AppendMzValues(StringBuilder fileBuilder)
        {
            StringBuilder mzValuesString = new StringBuilder();
            foreach (var mz in _mz)
            {
                mzValuesString.Append(mz);
                mzValuesString.Append(' ');
            }
            fileBuilder.AppendLine(mzValuesString.ToString());
        }

        private void AppendMetadata(StringBuilder fileBuilder)
        {
            fileBuilder.AppendLine(Metadata.Description);
        }

        private void EndInvariantCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentCulture;
        }

        private void BeginInvariantCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        #endregion

    }
}
