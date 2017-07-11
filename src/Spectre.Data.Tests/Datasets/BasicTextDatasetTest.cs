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
    [TestFixture, Category("Dataset")]
    public class BasicTextDatasetTest
    {
        private IDataset _dataset;
        private string _startDirectory;
        private readonly string TestDirectory = TestContext.CurrentContext.TestDirectory + "\\..\\..\\..\\..\\..\\test_files";

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

            _dataset = new BasicTextDataset(mz, data, null);
        }

        [Test]
        public void CreateFromFileTest()
        {
            Assert.DoesNotThrow(() => { _dataset = new BasicTextDataset("small-test.txt"); },
                "Dataset failed to initialize from file.");
            Assert.Throws<IOException>(() => { _dataset = new BasicTextDataset("wrong-filepath.txt"); },
                "Dataset has been initialized using wrong file path.");

            IEnumerable<SpatialCoordinates> coordinates = _dataset.SpatialCoordinates;
            var enumerator = coordinates.GetEnumerator();
            enumerator.MoveNext();
            SpatialCoordinates sc = enumerator.Current;
            Assert.AreEqual(1, sc.X,
                "Spatial coordinates differ");
            Assert.AreEqual(1, sc.Y,
                "Spatial coordinates differ");
            Assert.AreEqual(0, sc.Z,
                "Spatial coordinates differ");

            enumerator.MoveNext();
            sc = enumerator.Current;
            Assert.AreEqual(2, sc.X,
                "Spatial coordinates differ in second spectrum");
            Assert.AreEqual(1, sc.Y,
                "Spatial coordinates differ in second spectrum");
            Assert.AreEqual(0, sc.Z,
                "Spatial coordinates differ in second spectrum");

            Assert.AreEqual(_dataset.GetRawMzArray(), new[] {899.99, 902.58, 912.04},
                "The m/zs differ");
            Assert.AreEqual(_dataset.GetRawIntensityArray(0), new[] {12.0, 20.0, 0.0},
                "The intensities of first spectrum differs");
        }

        [Test]
        public void CreateFromRawDataTest()
        {
            double[] mz = { 1.0, 2.0, 3.0 };
            double[,] data = { { 1, 2.1, 3.2 } };
            int[,] coords = {{1, 2, 3}};

            Assert.Throws<InvalidDataException>(() => { _dataset = new BasicTextDataset(null, data, null); },
                "Dataset accepted null m/z array.");
            Assert.Throws<InvalidDataException>(() => { _dataset = new BasicTextDataset(mz, null, null); },
                "Dataset accepted null intensity array.");

            mz = new[] { 1.0, 2.0, 3.0 };
            data = new[,] { { 1.0, 2.0 } };

            Assert.Throws<InvalidDataException>(() => { _dataset = new BasicTextDataset(mz, data, null); },
                "Dataset accepted raw data of different lengths.");

            data = new[,] { { 1, 1.1, 1.2 }, { 1, 1.1, 1.2 } };

            Assert.Throws<InvalidDataException>(() => { _dataset = new BasicTextDataset(mz, data, coords); },
                "Dataset accepted spatial coordinates array of length different than data.");

            coords = new[,] {{1, 2, 3}, {4, 5, 6}};

            Assert.DoesNotThrow(() => { _dataset = new BasicTextDataset(mz, data, coords); },
                "Dataset failed to initialize from correct raw data.");

            Assert.AreEqual(_dataset.SpectrumCount, 2,
                "Dataset failed to load spectras correctly.");
            Assert.AreEqual(_dataset.SpatialCoordinates.Count(), 2,
                "Dataset do not assign spacial coordinates per spectrum.");
        }

        [Test]
        public void AppendFromFileTest()
        {
            int spectreCountBeforeAppend = _dataset.SpectrumCount;

            Assert.DoesNotThrow(() => { _dataset.AppendFromFile("small-test.txt"); },
                "The file wasn't successfully appended");

            Assert.AreEqual(spectreCountBeforeAppend + 4, _dataset.SpectrumCount,
                "Append didn't manage to include all spectras");

            Assert.AreEqual(10, _dataset.GetRawIntensityValue(5, 1),
                "The value of added intensity differs from expected");

            IEnumerable<SpatialCoordinates> spatialCoordinates = _dataset.SpatialCoordinates;
            var enumerator = spatialCoordinates.GetEnumerator();
            for (int i = 0; i < spectreCountBeforeAppend + 1; i++)
                enumerator.MoveNext();

            Assert.AreEqual(1.0, enumerator.Current.X,
                "The spatial coordinate of X wasn't appended properly");
            Assert.AreEqual(1.0, enumerator.Current.Y,
                "The spatial coordinate of Y wasn't appended properly");
            Assert.AreEqual(0.0, enumerator.Current.Z,
                "The spatial coordinate of Z wasn't appended properly");
        }

        [Test]
        public void AppendFromRawDataTest()
        {
            Assert.Throws<InvalidDataException>(() => { _dataset.AppendFromRawData(null, null); },
                "Dataset accepted null raw data.");

            Assert.AreEqual(_dataset.SpectrumCount, 3,
                "Dataset has been changed while appending null data.");

            double[,] newData = {{0.1}};

            Assert.Throws<InvalidDataException>(() => { _dataset.AppendFromRawData(newData, null); },
                "Dataset accepted raw data of different lengths.");

            Assert.AreEqual(_dataset.SpectrumCount, 3);

            newData = new[,] {{2, 2.1, 2.2}, {3, 3.1, 3.2}, {4, 4.1, 4.2}};
            int[,] coords = { { 1, 2, 3 } };

            Assert.Throws<InvalidDataException>(() => { _dataset.AppendFromRawData(newData, coords); },
                "Dataset accepted spatial coordinates array of length different than data.");

            coords = new[,] { { 1, 2, 3 }, { 4, 5, 6 }, {7, 8, 9} };

            Assert.DoesNotThrow(() => { _dataset.AppendFromRawData(newData, coords); },
                "Dataset failed to append correct raw data.");

            Assert.AreEqual(_dataset.SpectrumCount, 6,
                "Dataset failed to load spectras correctly.");
            Assert.AreEqual(_dataset.SpatialCoordinates.Count(), 6,
                "Dataset do not assign spacial coordinates per spectrum.");
            Assert.AreEqual(_dataset.GetRawIntensityValue(4, 1), 3.1,
                "Dataset appended incorrect values.");
        }

        [Test]
        public void GetDataPointTest()
        {
            DataPoint result = _dataset.GetDataPoint(1, 1);
            DataPoint expected = new DataPoint(2, 5.1);

            Assert.AreEqual(result.Mz, expected.Mz,
                "Dataset returned wrong m/z values.");
            Assert.AreEqual(result.Intensity, expected.Intensity, "Dataset returned wrong intensity values.");
        }

        [Test]
        public void GetDataPointsTest()
        {
            DataPoint[] result;

            try
            {
                result = _dataset.GetDataPoints(0, 3, 0);
                Assert.Fail("Dataset accepted incorrect indices.");
            }
            catch (Exception)
            {
            }

            try
            {
                result = _dataset.GetDataPoints(0, 0, 4);
                Assert.Fail("Dataset accepted incorrect indices.");

            }
            catch (Exception)
            {
            }

            result = _dataset.GetDataPoints(0, 0, 3);

            Assert.AreEqual(result[2].Mz, _dataset.GetDataPoint(0, 2).Mz, "Dataset returned wrong m/z values.");
            Assert.AreEqual(result[2].Intensity, _dataset.GetDataPoint(0, 2).Intensity, "Dataset returned wrong intensity values.");
        }

        [Test]
        public void GetSpectrumLengthTest()
        {
            Assert.AreEqual(_dataset.SpectrumLength, 3, "Dataset returned wrong spectrum length.");
        }

        [Test]
        public void GetSpatialCoordinatesTest()
        {
            double[,] data = { { 1, 2.1, 3.2 } };
            int[,] coords = { { 1, 2, 3 } };

            _dataset.AppendFromRawData(data, coords);

            SpatialCoordinates spatialCoordinates = _dataset.GetSpatialCoordinates(_dataset.SpectrumCount - 1);
            Assert.AreEqual(spatialCoordinates.X, 1, "Dataset returned wrong X spacial coordinate.");
            Assert.AreEqual(spatialCoordinates.Y, 2, "Dataset returned wrong Y spacial coordinate.");
            Assert.AreEqual(spatialCoordinates.Z, 3, "Dataset returned wrong Z spacial coordinate.");
        }

        [Test]
        public void GetSpectrumCountTest()
        {
            Assert.AreEqual(_dataset.SpectrumCount, 3, "Dataset returned wrong spectrum count.");
        }

        [Test]
        public void GetRawMzArrayTest()
        {
            double[] result = _dataset.GetRawMzArray();
            Assert.AreEqual(result, new[] { 1.0, 2.0, 3.0 }, "Dataset returned wrong m/z array.");
        }

        [Test]
        public void GetRawMzValueTest()
        {
            double result = _dataset.GetRawMzValue(1);
            Assert.AreEqual(result, 2, "Dataset returned wrong m/z value.");
        }

        [Test]
        public void GetRawIntensityValueTest()
        {
            double result = _dataset.GetRawIntensityValue(1, 1);
            Assert.AreEqual(result, 5.1, "Dataset returned wrong intensity value.");
        }

        [Test]
        public void GetRawIntensityArrayTest()
        {
            double[] result = _dataset.GetRawIntensityArray(2);
            Assert.AreEqual(result, new[] { 7, 8.1, 9.2 }, "Dataset returned wrong intensity array.");
        }

        [Test]
        public void GetRawIntensityRowTest()
        {
            double[] result = _dataset.GetRawIntensityRow(1);
            Assert.AreEqual(result, new[] { 2.1, 5.1, 8.1 }, "Dataset returned wrong intensity row.");
        }

        [Test]
        public void GetRawIntensityRangeTest()
        {
            double[,] result = _dataset.GetRawIntensityRange(1, 3, 1, 3);
            Assert.AreEqual(result, new[,] { {5.1, 6.2}, {8.1, 9.2} }, "Dataset returned wrong intensity row.");
        }

        [Test]
        public void GetRawIntensitiesTest()
        {
            double[,] result = _dataset.GetRawIntensities();

            Assert.AreEqual(result, new[,] { { 1, 2.1, 3.2 }, { 4, 5.1, 6.2 }, { 7, 8.1, 9.2 } }, "Dataset returned wrong intensity row.");
        }

        [Test]
        public void GetRawSpacialCoordinates()
        {
            double[] mz = { 1.0, 2.0, 3.0 };
            double[,] data = { { 1, 1.1, 1.2 }, { 1, 1.1, 1.2 } };
            int[,] coords = { { 1, 2, 3 }, { 4, 5, 6 } };

            _dataset.CreateFromRawData(mz, data, coords);

            var retCoords3D = _dataset.GetRawSpacialCoordinates(false);
            Assert.AreEqual(retCoords3D, coords, "Dataset returned incorrect 3D spatial coordinates array.");

            var retCoords2D = _dataset.GetRawSpacialCoordinates(true);
            Assert.AreEqual(retCoords2D, new[,] { {1, 2 }, { 4, 5 } }, "Dataset returned incorrect 2D spatial coordinates array.");
        }

        [Test]
        public void SaveToFileTest()
        {
            string fileName = "save-test.txt";
            _dataset.SaveToFile(fileName);
            IDataset setFromSave = new BasicTextDataset(fileName);

            Assert.AreEqual(_dataset.GetRawMzArray(), setFromSave.GetRawMzArray(), "Mz values were stored incorrectly");

            for (int i = 0; i < _dataset.SpectrumCount; i++)
            {
                Assert.AreEqual(_dataset.GetRawIntensityRow(i), setFromSave.GetRawIntensityRow(i), "Intensities were stored incorrectly");
            }

            Assert.AreEqual(_dataset.SpatialCoordinates, setFromSave.SpatialCoordinates, "Spatial coordinates were stored incorrectly");

            File.Delete(fileName);
        }
    }
}
