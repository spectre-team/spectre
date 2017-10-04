/*
 * BasicTextDatasetTest.cs
 * Checks, whether MCR is properly called and result may be obtained.
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
using System.IO;
using System.Linq;
using NUnit.Framework;
using Spectre.Data.Datasets;
using Spectre.Data.Structures;

namespace Spectre.Data.Tests.Datasets
{
    [TestFixture]
    [Category(name: "Dataset")]
    public class BasicTextDatasetTest
    {
        private IDataset _dataset;
        private string _startDirectory;

        private readonly string TestDirectory = TestContext.CurrentContext.TestDirectory
                                                + "\\..\\..\\..\\..\\..\\test_files";

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            _startDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(TestDirectory);
        }

        [OneTimeTearDown]
        public void TearDownFixture()
        {
            Directory.SetCurrentDirectory(_startDirectory);
        }

        [SetUp]
        public void SetUpClass()
        {
            double[] mz = {1.0, 2.0, 3.0};
            double[,] data = {{1, 2.1, 3.2}, {4, 5.1, 6.2}, {7, 8.1, 9.2}};

            _dataset = new BasicTextDataset(mz, data, coordinates: null);
        }

        [Test]
        public void CreateFromFileTest()
        {
            Assert.DoesNotThrow(code: () => { _dataset = new BasicTextDataset(textFilePath: "small-test.txt"); },
                message: "Dataset failed to initialize from file.");
            Assert.Throws<IOException>(code: () =>
                {
                    _dataset = new BasicTextDataset(textFilePath: "wrong-filepath.txt");
                },
                message: "Dataset has been initialized using wrong file path.");

            var coordinates = _dataset.SpatialCoordinates;
            var enumerator = coordinates.GetEnumerator();
            enumerator.MoveNext();
            var sc = enumerator.Current;
            Assert.AreEqual(expected: 1,
                actual: sc.X,
                message: "Spatial coordinates differ");
            Assert.AreEqual(expected: 1,
                actual: sc.Y,
                message: "Spatial coordinates differ");
            Assert.AreEqual(expected: 0,
                actual: sc.Z,
                message: "Spatial coordinates differ");

            enumerator.MoveNext();
            sc = enumerator.Current;
            Assert.AreEqual(expected: 2,
                actual: sc.X,
                message: "Spatial coordinates differ in second spectrum");
            Assert.AreEqual(expected: 1,
                actual: sc.Y,
                message: "Spatial coordinates differ in second spectrum");
            Assert.AreEqual(expected: 0,
                actual: sc.Z,
                message: "Spatial coordinates differ in second spectrum");

            Assert.AreEqual(expected: _dataset.GetRawMzArray(),
                actual: new[] {899.99, 902.58, 912.04},
                message: "The m/zs differ");
            Assert.AreEqual(expected: _dataset.GetRawIntensityArray(spectrumIdx: 0),
                actual: new[] {12.0, 20.0, 0.0},
                message: "The intensities of first spectrum differs");
        }

        [Test]
        public void CreateFromRawDataTest()
        {
            double[] mz = {1.0, 2.0, 3.0};
            double[,] data = {{1, 2.1, 3.2}};
            int[,] coords = {{1, 2, 3}};

            Assert.Throws<InvalidDataException>(code: () =>
                {
                    _dataset = new BasicTextDataset(mz: null, data: data, coordinates: null);
                },
                message: "Dataset accepted null m/z array.");
            Assert.Throws<InvalidDataException>(code: () =>
                {
                    _dataset = new BasicTextDataset(mz, data: null, coordinates: null);
                },
                message: "Dataset accepted null intensity array.");

            mz = new[] {1.0, 2.0, 3.0};
            data = new[,] {{1.0, 2.0}};

            Assert.Throws<InvalidDataException>(code: () =>
                {
                    _dataset = new BasicTextDataset(mz, data, coordinates: null);
                },
                message: "Dataset accepted raw data of different lengths.");

            data = new[,] {{1, 1.1, 1.2}, {1, 1.1, 1.2}};

            Assert.Throws<InvalidDataException>(code: () => { _dataset = new BasicTextDataset(mz, data, coords); },
                message: "Dataset accepted spatial coordinates array of length different than data.");

            coords = new[,] {{1, 2, 3}, {4, 5, 6}};

            Assert.DoesNotThrow(code: () => { _dataset = new BasicTextDataset(mz, data, coords); },
                message: "Dataset failed to initialize from correct raw data.");

            Assert.AreEqual(_dataset.SpectrumCount,
                actual: 2,
                message: "Dataset failed to load spectras correctly.");
            Assert.AreEqual(expected: _dataset.SpatialCoordinates.Count(),
                actual: 2,
                message: "Dataset do not assign spacial coordinates per spectrum.");
        }

        [Test]
        public void AppendFromFileTest()
        {
            var spectreCountBeforeAppend = _dataset.SpectrumCount;

            Assert.DoesNotThrow(code: () => { _dataset.AppendFromFile(filePath: "small-test.txt"); },
                message: "The file wasn't successfully appended");

            Assert.AreEqual(expected: spectreCountBeforeAppend + 4,
                actual: _dataset.SpectrumCount,
                message: "Append didn't manage to include all spectras");

            Assert.AreEqual(expected: 10,
                actual: _dataset.GetRawIntensityValue(spectrumIdx: 5, valueIdx: 1),
                message: "The value of added intensity differs from expected");

            var spatialCoordinates = _dataset.SpatialCoordinates;
            var enumerator = spatialCoordinates.GetEnumerator();
            for (var i = 0; i < (spectreCountBeforeAppend + 1); i++)
            {
                enumerator.MoveNext();
            }

            Assert.AreEqual(expected: 1.0,
                actual: enumerator.Current.X,
                message: "The spatial coordinate of X wasn't appended properly");
            Assert.AreEqual(expected: 1.0,
                actual: enumerator.Current.Y,
                message: "The spatial coordinate of Y wasn't appended properly");
            Assert.AreEqual(expected: 0.0,
                actual: enumerator.Current.Z,
                message: "The spatial coordinate of Z wasn't appended properly");
        }

        [Test]
        public void AppendFromRawDataTest()
        {
            Assert.Throws<InvalidDataException>(code: () => { _dataset.AppendFromRawData(data: null, coordinates: null); },
                message: "Dataset accepted null raw data.");

            Assert.AreEqual(_dataset.SpectrumCount,
                actual: 3,
                message: "Dataset has been changed while appending null data.");

            double[,] newData = {{0.1}};

            Assert.Throws<InvalidDataException>(code: () => { _dataset.AppendFromRawData(newData, coordinates: null); },
                message: "Dataset accepted raw data of different lengths.");

            Assert.AreEqual(_dataset.SpectrumCount, actual: 3);

            newData = new[,] {{2, 2.1, 2.2}, {3, 3.1, 3.2}, {4, 4.1, 4.2}};
            int[,] coords = {{1, 2, 3}};

            Assert.Throws<InvalidDataException>(code: () => { _dataset.AppendFromRawData(newData, coords); },
                message: "Dataset accepted spatial coordinates array of length different than data.");

            coords = new[,] {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}};

            Assert.DoesNotThrow(code: () => { _dataset.AppendFromRawData(newData, coords); },
                message: "Dataset failed to append correct raw data.");

            Assert.AreEqual(_dataset.SpectrumCount,
                actual: 6,
                message: "Dataset failed to load spectras correctly.");
            Assert.AreEqual(expected: _dataset.SpatialCoordinates.Count(),
                actual: 6,
                message: "Dataset do not assign spacial coordinates per spectrum.");
            Assert.AreEqual(expected: _dataset.GetRawIntensityValue(spectrumIdx: 4, valueIdx: 1),
                actual: 3.1,
                message: "Dataset appended incorrect values.");
        }

        [Test]
        public void GetDataPointTest()
        {
            var result = _dataset.GetDataPoint(spectrumIdx: 1, valueIdx: 1);
            var expected = new DataPoint(mz: 2, intensity: 5.1);

            Assert.AreEqual(result.Mz,
                expected.Mz,
                message: "Dataset returned wrong m/z values.");
            Assert.AreEqual(result.Intensity, expected.Intensity, message: "Dataset returned wrong intensity values.");
        }

        [Test]
        public void GetDataPointsTest()
        {
            DataPoint[] result;

            try
            {
                result = _dataset.GetDataPoints(spectrumIdx: 0, valueIdxFrom: 3, valueIdxTo: 0);
                Assert.Fail(message: "Dataset accepted incorrect indices.");
            }
            catch (Exception) { }

            try
            {
                result = _dataset.GetDataPoints(spectrumIdx: 0, valueIdxFrom: 0, valueIdxTo: 4);
                Assert.Fail(message: "Dataset accepted incorrect indices.");
            }
            catch (Exception) { }

            result = _dataset.GetDataPoints(spectrumIdx: 0, valueIdxFrom: 0, valueIdxTo: 3);

            Assert.AreEqual(result[2]
                    .Mz,
                _dataset.GetDataPoint(spectrumIdx: 0, valueIdx: 2)
                    .Mz,
                message: "Dataset returned wrong m/z values.");
            Assert.AreEqual(result[2]
                    .Intensity,
                _dataset.GetDataPoint(spectrumIdx: 0, valueIdx: 2)
                    .Intensity,
                message: "Dataset returned wrong intensity values.");
        }

        [Test]
        public void GetSpectrumLengthTest()
        {
            Assert.AreEqual(_dataset.SpectrumLength, actual: 3, message: "Dataset returned wrong spectrum length.");
        }

        [Test]
        public void GetSpatialCoordinatesTest()
        {
            double[,] data = {{1, 2.1, 3.2}};
            int[,] coords = {{1, 2, 3}};

            _dataset.AppendFromRawData(data, coords);

            var spatialCoordinates = _dataset.GetSpatialCoordinates(spectrumIdx: _dataset.SpectrumCount - 1);
            Assert.AreEqual(spatialCoordinates.X, actual: 1, message: "Dataset returned wrong X spacial coordinate.");
            Assert.AreEqual(spatialCoordinates.Y, actual: 2, message: "Dataset returned wrong Y spacial coordinate.");
            Assert.AreEqual(spatialCoordinates.Z, actual: 3, message: "Dataset returned wrong Z spacial coordinate.");
        }

        [Test]
        public void GetSpectrumCountTest()
        {
            Assert.AreEqual(_dataset.SpectrumCount, actual: 3, message: "Dataset returned wrong spectrum count.");
        }

        [Test]
        public void GetRawMzArrayTest()
        {
            var result = _dataset.GetRawMzArray();
            Assert.AreEqual(result, actual: new[] {1.0, 2.0, 3.0}, message: "Dataset returned wrong m/z array.");
        }

        [Test]
        public void GetRawMzValueTest()
        {
            var result = _dataset.GetRawMzValue(index: 1);
            Assert.AreEqual(result, actual: 2, message: "Dataset returned wrong m/z value.");
        }

        [Test]
        public void GetRawIntensityValueTest()
        {
            var result = _dataset.GetRawIntensityValue(spectrumIdx: 1, valueIdx: 1);
            Assert.AreEqual(result, actual: 5.1, message: "Dataset returned wrong intensity value.");
        }

        [Test]
        public void GetRawIntensityArrayTest()
        {
            var result = _dataset.GetRawIntensityArray(spectrumIdx: 2);
            Assert.AreEqual(result, actual: new[] {7, 8.1, 9.2}, message: "Dataset returned wrong intensity array.");
        }

        [Test]
        public void GetRawIntensityRowTest()
        {
            var result = _dataset.GetRawIntensityRow(valueIdx: 1);
            Assert.AreEqual(result, actual: new[] {2.1, 5.1, 8.1}, message: "Dataset returned wrong intensity row.");
        }

        [Test]
        public void GetRawIntensityRangeTest()
        {
            var result = _dataset.GetRawIntensityRange(spectrumIdxFrom: 1, spectrumIdxTo: 3, valueIdxFrom: 1, valueIdxTo: 3);
            Assert.AreEqual(result,
                actual: new[,] {{5.1, 6.2}, {8.1, 9.2}},
                message: "Dataset returned wrong intensity row.");
        }

        [Test]
        public void GetRawIntensitiesTest()
        {
            var result = _dataset.GetRawIntensities();

            Assert.AreEqual(result,
                actual: new[,] {{1, 2.1, 3.2}, {4, 5.1, 6.2}, {7, 8.1, 9.2}},
                message: "Dataset returned wrong intensity row.");
        }

        [Test]
        public void GetRawSpacialCoordinates()
        {
            double[] mz = {1.0, 2.0, 3.0};
            double[,] data = {{1, 1.1, 1.2}, {1, 1.1, 1.2}};
            int[,] coords = {{1, 2, 3}, {4, 5, 6}};

            _dataset.CreateFromRawData(mz, data, coords);

            var retCoords3D = _dataset.GetRawSpacialCoordinates(is2D: false);
            Assert.AreEqual(retCoords3D, coords, message: "Dataset returned incorrect 3D spatial coordinates array.");

            var retCoords2D = _dataset.GetRawSpacialCoordinates(is2D: true);
            Assert.AreEqual(retCoords2D,
                actual: new[,] {{1, 2}, {4, 5}},
                message: "Dataset returned incorrect 2D spatial coordinates array.");
        }

        [Test]
        public void SaveToFileTest()
        {
            var fileName = "save-test.txt";
            _dataset.SaveToFile(fileName);
            IDataset setFromSave = new BasicTextDataset(fileName);

            Assert.AreEqual(expected: _dataset.GetRawMzArray(),
                actual: setFromSave.GetRawMzArray(),
                message: "Mz values were stored incorrectly");

            for (var i = 0; i < _dataset.SpectrumCount; i++)
            {
                Assert.AreEqual(expected: _dataset.GetRawIntensityRow(i),
                    actual: setFromSave.GetRawIntensityRow(i),
                    message: "Intensities were stored incorrectly");
            }

            Assert.AreEqual(_dataset.SpatialCoordinates,
                setFromSave.SpatialCoordinates,
                message: "Spatial coordinates were stored incorrectly");

            File.Delete(fileName);
        }
    }
}
