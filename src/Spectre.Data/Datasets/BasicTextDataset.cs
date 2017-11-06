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
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading;
using Spectre.Data.Structures;
using Spectre.Dependencies;

namespace Spectre.Data.Datasets
{
    /// <summary>
    ///     Class representing dataset created from streaming an ordinary text file containing formatted data.
    /// </summary>
    public class BasicTextDataset : IDataset
    {
        #region Fields

        /// <summary>
        ///     Container for storing spatial coordinates for every loaded spectrum.
        /// </summary>
        private List<SpatialCoordinates> _spatialCoordinates;

        /// <summary>
        ///     Array of m/z values for all the spectras.
        /// </summary>
        private double[] _mz;

        /// <summary>
        ///     Container for storing intensity values for every loaded spectrum.
        /// </summary>
        private List<double[]> _intensityArray;

        /// <summary>
        ///     Container for storing label values for every spectrum.
        /// </summary>
        private List<int> _labels;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BasicTextDataset" /> class.
        ///     Constructor with file initialization.
        /// </summary>
        /// <param name="textFilePath">Path to the text file.</param>
        public BasicTextDataset(string textFilePath)
        {
            FileSystem = DependencyResolver.GetService<IFileSystem>();
            CreateFromFile(textFilePath);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BasicTextDataset" /> class.
        ///     Constructor with raw value initialization.
        /// </summary>
        /// <param name="mz">Array of m/z values.</param>
        /// <param name="data">Array of intensity values.</param>
        /// <param name="coordinates">Array of spatial coordinates.</param>
        public BasicTextDataset(double[] mz, double[,] data, int[,] coordinates)
        {
            FileSystem = DependencyResolver.GetService<IFileSystem>();
            CreateFromRawData(mz, data, coordinates);
        }

        #endregion

        #region Properties

        /// <inheritdoc cref="IDataset"/>
        public Metadata Metadata { get; private set; }

        /// <summary>
        ///     See <see cref="IDataset" /> for description.
        /// </summary>
        public IEnumerable<SpatialCoordinates> SpatialCoordinates
        {
            get { return _spatialCoordinates; }
            private set { _spatialCoordinates = value.ToList(); }
        }

        /// <summary>
        ///     See <see cref="IDataset" /> for description.
        /// </summary>
        public IEnumerable<int> Labels
        {
            get { return _labels; }
            set { _labels = value.ToList(); }
        }

        /// <summary>
        ///     See <see cref="IDataset" /> for description.
        /// </summary>
        public int SpectrumLength
        {
            get { return _mz.Length; }
        }

        /// <summary>
        ///     See <see cref="IDataset" /> for description.
        /// </summary>
        public int SpectrumCount
        {
            get { return _intensityArray.Count; }
        }

        /// <summary>
        /// Handle to file system.
        /// </summary>
        private IFileSystem FileSystem { get; }

        #endregion

        #region IDataset Methods

        /// <summary>
        ///     Method for creating new dataset from text file, overwriting current data.
        /// </summary>
        /// <param name="filePath">Path to a text file.</param>
        /// <exception cref="Exception">Thrown where there is a problem with file loading.</exception>
        public void CreateFromFile(string filePath)
        {
            try
            {
                using (var sr = FileSystem.File.OpenText(filePath))
                {
                    var metadata = sr.ReadLine(); // global metadata
#pragma warning disable SA1305 // Field names must not use Hungarian notation
                    var mzValues = sr.ReadLine()
#pragma warning restore SA1305 // Field names must not use Hungarian notation
                        .Split(separator: null);
                    mzValues = mzValues.Where(predicate: str => !string.IsNullOrEmpty(str))
                        .ToArray();
                    if (mzValues.Length < 1)
                    {
                        throw new InvalidDataException(message: "No m/z values found.");
                    }
                    _mz = new double[mzValues.Length];
                    for (var i = 0; i < _mz.Length; i++)
                    {
                        if (!double.TryParse(
                            s: mzValues[i],
                            style: NumberStyles.Any,
                            provider: CultureInfo.InvariantCulture,
                            result: out _mz[i]))
                        {
                            _mz[i] = double.NaN;
                        }
                    }
                }
            }
            catch (NullReferenceException e)
            {
                throw new IOException(
                    message: "M/z data could not be parsed from file " + filePath + ".",
                    innerException: e);
            }
            catch (InvalidDataException e)
            {
                throw new IOException(
                    message: "M/z array parsed from file " + filePath + " is empty.",
                    innerException: e);
            }
            catch (Exception e)
            {
                // catch remaining Exceptions that are related to StreamReader
                throw new IOException(message: "Streamer failed to read " + filePath + " file.", innerException: e);
            }

            _intensityArray = new List<double[]>();
            _spatialCoordinates = new List<SpatialCoordinates>();
            _labels = new List<int>();
            AppendFromFile(filePath);
        }

        /// <inheritdoc cref="IDataset"/>
        public void CreateFromRawData(double[] mz, double[,] data, int[,] coordinates)
        {
            if ((mz == null) || (data == null))
            {
                throw new InvalidDataException(message: "The input data is null.");
            }
            if ((coordinates != null) && (coordinates.GetLength(dimension: 0) != data.GetLength(dimension: 0)))
            {
                throw new InvalidDataException(
                    message: "Amount of input spectra does not match the amount of spatial coordinates.");
            }
            if (mz.Length != data.GetLength(dimension: 1))
            {
                throw new InvalidDataException(message: "Length of the data must be equal to length of m/z values.");
            }

            _intensityArray = new List<double[]>();
            _spatialCoordinates = new List<SpatialCoordinates>();
            _labels = new List<int>();
            _mz = mz;
            AppendFromRawData(data, coordinates);
        }

        /// <summary>
        ///     Method parsing the whole text file and initializing the array
        ///     with found data.
        /// </summary>
        /// <param name="filePath">Path to the text file.</param>
        /// <exception cref="InvalidDataException">Thrown where there is a problem with file parsing.</exception>
        public void AppendFromFile(string filePath)
        {
            try
            {
                using (var sr = FileSystem.File.OpenText(filePath))
                {
                    sr.ReadLine(); // omit global metadata
                    sr.ReadLine(); // omit m/z values
                    while (sr.Peek() > -1)
                    {
                        var metadata = sr.ReadLine()
                            ?.Split(separator: null);
                        var intensities = sr.ReadLine()
                            ?.Split(separator: null);
                        intensities = intensities.Where(predicate: str => !string.IsNullOrEmpty(str))
                            .ToArray();
                        if ((metadata.Length == 0) || (intensities.Length == 0))
                        {
                            continue;
                        }
                        if (intensities.Length != _mz.Length)
                        {
                            throw new InvalidDataException(
                                message: "Length of the data must be equal to length of m/z values.");
                        }

                        int x,
                            y,
                            z,
                            label;

                        if (!int.TryParse(
                            s: metadata.Length > 0 ? metadata[0] : null,
                            style: NumberStyles.Any,
                            provider: CultureInfo.InvariantCulture,
                            result: out x))
                        {
                            x = -1;
                        }
                        if (!int.TryParse(
                            s: metadata.Length > 1 ? metadata[1] : null,
                            style: NumberStyles.Any,
                            provider: CultureInfo.InvariantCulture,
                            result: out y))
                        {
                            y = -1;
                        }
                        if (!int.TryParse(
                            s: metadata.Length > 2 ? metadata[2] : null,
                            style: NumberStyles.Any,
                            provider: CultureInfo.InvariantCulture,
                            result: out z))
                        {
                            z = -1;
                        }
                        if (!int.TryParse(
                            s: metadata.Length > 3 ? metadata[3] : null,
                            style: NumberStyles.Any,
                            provider: CultureInfo.InvariantCulture,
                            result: out label))
                        {
                            label = -1;
                        }

                        _spatialCoordinates.Add(item: new SpatialCoordinates(x, y, z));
                        _intensityArray.Add(item: new double[_mz.Length]);
                        _labels.Add(label);

                        var backIdx = _intensityArray.Count - 1;
                        for (var i = 0; i < intensities.Length; i++)
                        {
                            if (!double.TryParse(
                                s: intensities[i],
                                style: NumberStyles.Any,
                                provider: CultureInfo.InvariantCulture,
                                result: out _intensityArray[backIdx][i]))
                            {
                                _intensityArray[backIdx][i] = double.NaN;
                            }
                        }
                    }
                }
            }
            catch (InvalidDataException e)
            {
                throw new IOException(message: "Length mismatch in parsed data.", innerException: e);
            }
            catch (Exception e)
            {
                // catch remaining Exceptions that are related to StreamReader
                throw new IOException(message: "Streamer failed to read " + filePath + " file.", innerException: e);
            }
        }

        /// <inheritdoc cref="IDataset"/>
        public void AppendFromRawData(double[,] data, int[,] coordinates)
        {
            if (data == null)
            {
                throw new InvalidDataException(message: "The input data is null.");
            }
            if ((coordinates != null) && (coordinates.GetLength(dimension: 0) != data.GetLength(dimension: 0)))
            {
                throw new InvalidDataException(
                    message: "Amount of input spectra does not match the amount of spatial coordinates.");
            }
            if (_mz.Length != data.GetLength(dimension: 1))
            {
                throw new InvalidDataException(
                    message: "The length of input data does not match the length of present data.");
            }

            var coordinateDimensions = coordinates?.GetLength(dimension: 1) ?? 0;

            for (var i = 0; i < data.GetLength(dimension: 0); i++)
            {
                int[] xyz = { -1, -1, -1 };
                for (var j = 0; j < coordinateDimensions; j++)
                {
                    xyz[j] = coordinates[i, j];
                }
                _spatialCoordinates.Add(item: new SpatialCoordinates(x: xyz[0], y: xyz[1], z: xyz[2]));
                _intensityArray.Add(item: new double[data.GetLength(dimension: 1)]);
                _labels.Add(item: -1);

                var backIdx = _intensityArray.Count - 1;
                for (var j = 0; j < data.GetLength(dimension: 1); j++)
                {
                    _intensityArray[backIdx][j] = data[i, j];
                }
            }
        }

        /// <inheritdoc cref="IDataset"/>
        public DataPoint GetDataPoint(int spectrumIdx, int valueIdx)
        {
            return new DataPoint(
                mz: _mz[valueIdx],
                intensity: _intensityArray[spectrumIdx][valueIdx]);
        }

        /// <inheritdoc cref="IDataset"/>
        public DataPoint[] GetDataPoints(int spectrumIdx, int valueIdxFrom, int valueIdxTo)
        {
            if (valueIdxFrom >= valueIdxTo)
            {
                throw new IndexOutOfRangeException();
            }
            var valueCnt = valueIdxTo - valueIdxFrom;

            var result = new DataPoint[valueCnt];
            for (var i = 0; i < valueCnt; i++)
            {
                result[i] = new DataPoint(
                    mz: _mz[valueIdxFrom + i],
                    intensity: _intensityArray[spectrumIdx][valueIdxFrom + i]);
            }
            return result;
        }

        /// <inheritdoc cref="IDataset"/>
        public SpatialCoordinates GetSpatialCoordinates(int spectrumIdx)
        {
            return _spatialCoordinates[spectrumIdx];
        }

        /// <inheritdoc cref="IDataset"/>
        public double[] GetRawMzArray()
        {
            return _mz;
        }

        /// <inheritdoc cref="IDataset"/>
        public double GetRawMzValue(int index)
        {
            return _mz[index];
        }

        /// <inheritdoc cref="IDataset"/>
        public double GetRawIntensityValue(int spectrumIdx, int valueIdx)
        {
            return _intensityArray[spectrumIdx][valueIdx];
        }

        /// <inheritdoc cref="IDataset"/>
        public double[] GetRawIntensityArray(int spectrumIdx)
        {
            return _intensityArray[spectrumIdx];
        }

        /// <inheritdoc cref="IDataset"/>
        public double[] GetRawIntensityRow(int valueIdx)
        {
            var result = new double[_intensityArray.Count];
            for (var i = 0; i < _intensityArray.Count; i++)
            {
                result[i] = _intensityArray[i][valueIdx];
            }
            return result;
        }

        /// <inheritdoc cref="IDataset"/>
        public double[,] GetRawIntensityRange(int spectrumIdxFrom, int spectrumIdxTo, int valueIdxFrom, int valueIdxTo)
        {
            if ((spectrumIdxFrom >= spectrumIdxTo) || (valueIdxFrom >= valueIdxTo))
            {
                throw new IndexOutOfRangeException();
            }
            var spectrumCnt = spectrumIdxTo - spectrumIdxFrom;
            var valueCnt = valueIdxTo - valueIdxFrom;

            var result = new double[spectrumCnt, valueCnt];

            for (var i = 0; i < spectrumCnt; i++)
            {
                for (var j = 0; j < valueCnt; j++)
                {
                    result[i, j] = _intensityArray[index: spectrumIdxFrom + i][valueIdxFrom + j];
                }
            }

            return result;
        }

        /// <inheritdoc cref="IDataset"/>
        public double[,] GetRawIntensities()
        {
            return GetRawIntensityRange(
                spectrumIdxFrom: 0,
                spectrumIdxTo: SpectrumCount,
                valueIdxFrom: 0,
                valueIdxTo: SpectrumLength);
        }

        /// <inheritdoc cref="IDataset"/>
        public int[,] GetRawSpacialCoordinates(bool is2D)
        {
            var dimensions = is2D ? 2 : 3;

            var result = new int[SpectrumCount, dimensions];
            for (var i = 0; i < SpectrumCount; i++)
            {
                var xyz = _spatialCoordinates[i]
                    .GetRaw();
                for (var j = 0; j < dimensions; j++)
                {
                    result[i, j] = xyz[j];
                }
            }

            return result;
        }

        /// <inheritdoc cref="IDataset"/>
        public void SaveToFile(string path)
        {
            var fileBuilder = new StringBuilder();

            // fixes comma-instead-of-dot related issues
            BeginInvariantCulture();
            {
                AppendMetadata(fileBuilder);
                AppendMzValues(fileBuilder);
                AppendIntensitiesAndLocalMetadata(fileBuilder);

                SaveDataToFile(path, fileBuilder);
            }
            EndInvariantCulture();
        }

        #endregion

        #region Private methods

        private void SaveDataToFile(string path, StringBuilder fileBuilder)
        {
            using (var sw = FileSystem.File.CreateText(path))
            {
                sw.Write(value: fileBuilder.ToString());
            }
        }

        private void AppendIntensitiesAndLocalMetadata(StringBuilder fileBuilder)
        {
            var spectrumBuilder = new StringBuilder();
            var coordinates = _spatialCoordinates.ToArray();
            var labels = _labels.ToArray();
            var intensities = _intensityArray.ToArray();

            for (var i = 0; i < intensities.Length; i++)
            {
                fileBuilder.AppendLine(value: $"{coordinates[i].ToString()} {labels[i].ToString()}");

                for (var j = 0;
                    j
                    < intensities[0]
                        .Length;
                    j++)
                {
                    spectrumBuilder.Append(value: intensities[i][j]);
                    spectrumBuilder.Append(value: ' ');
                }
                fileBuilder.AppendLine(value: spectrumBuilder.ToString());
                spectrumBuilder.Clear();
            }
        }

        private void AppendMzValues(StringBuilder fileBuilder)
        {
#pragma warning disable SA1305 // Field names must not use Hungarian notation
            var mzValuesString = new StringBuilder();
#pragma warning restore SA1305 // Field names must not use Hungarian notation
            foreach (var mz in _mz)
            {
                mzValuesString.Append(mz);
                mzValuesString.Append(value: ' ');
            }
            fileBuilder.AppendLine(value: mzValuesString.ToString());
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
