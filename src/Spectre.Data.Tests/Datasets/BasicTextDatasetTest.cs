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

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            Directory.SetCurrentDirectory(TestContext.CurrentContext.TestDirectory + "\\..\\..");
        }

        [SetUp]
        public void SetUpClass()
        {
            double[] mz = {1.0, 2.0, 3.0};
            double[,] data = {{1, 2.1, 3.2}, {4, 5.1, 6.2}, {7, 8.1, 9.2}};

            _dataset = new BasicTextDataset(mz, data);
        }

        [Test]
        public void CreateFromFileTest()
        {
            try
            {
                _dataset = new BasicTextDataset("small-test.txt");
            }
            catch (Exception e)
            {
                Assert.Fail("Dataset failed to initialize from file.", e);
            }
        }

        [Test]
        public void CreateFromRawDataTest()
        {
            double[] mz = null;
            double[,] data = null;

            try
            {
                _dataset = new BasicTextDataset(mz, data);
                Assert.Fail("Dataset accepted null raw data.");
            }
            catch (Exception)
            {
            }

            mz = new[] {1.0, 2.0, 3.0};
            data = new[,] {{1.0, 2.0}};

            try
            {
                _dataset = new BasicTextDataset(mz, data);
                Assert.Fail("Dataset accepted raw data of different lengths.");
            }
            catch (Exception)
            {
            }

            data = new[,] {{1, 1.1, 1.2}, {1, 1.1, 1.2}};

            try
            {
                _dataset = new BasicTextDataset(mz, data);
            }
            catch (Exception)
            {
                Assert.Fail("Dataset failed to initialize from correct raw data.");
            }

            Assert.AreEqual(_dataset.GetSpectrumCount(), 2, "Dataset failed to load spectras correctly.");
            Assert.AreEqual(_dataset.SpacialCoordinates.Count(), 2,
                "Dataset do not assign spacial coordinates per spectrum.");
        }

        [Test]
        public void AppendFromFileTest()
        {
        }

        [Test]
        public void AppendFromRawDataTest()
        {
            double[,] newData = null;

            try
            {
                _dataset.AppendFromRawData(newData);
                Assert.Fail("Dataset accepted null raw data.");
            }
            catch (Exception)
            {
            }

            Assert.AreEqual(_dataset.GetSpectrumCount(), 3);

            newData = new[,] {{0.1}};

            try
            {
                _dataset.AppendFromRawData(newData);
                Assert.Fail("Dataset accepted raw data of different lengths.");
            }
            catch (Exception)
            {
            }

            Assert.AreEqual(_dataset.GetSpectrumCount(), 3);

            newData = new[,] {{2, 2.1, 2.2}, {3, 3.1, 3.2}, {4, 4.1, 4.2}};

            try
            {
                _dataset.AppendFromRawData(newData);
            }
            catch (Exception)
            {
                Assert.Fail("Dataset failed to append correct raw data.");
            }

            Assert.AreEqual(_dataset.GetSpectrumCount(), 6, "Dataset failed to load spectras correctly.");
            Assert.AreEqual(_dataset.SpacialCoordinates.Count(), 6,
                "Dataset do not assign spacial coordinates per spectrum.");
            Assert.AreEqual(_dataset.GetRawIntensityValue(4, 1), 3.1, "Dataset appended incorrect values.");
        }

        [Test]
        public void GetDataPointTest()
        {
            DataPoint result = _dataset.GetDataPoint(1, 1);
            DataPoint expected = new DataPoint(2, 5.1);

            Assert.AreEqual(result.Mz, expected.Mz, "Dataset returned wrong m/z values.");
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
            Assert.AreEqual(_dataset.GetSpectrumLength(), 3, "Dataset returned wrong spectrum length.");
        }

        [Test]
        public void GetSpectrumCountTest()
        {
            Assert.AreEqual(_dataset.GetSpectrumCount(), 3, "Dataset returned wrong spectrum count.");
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
    }
}
