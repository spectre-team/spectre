/*
 * DivikResultTests.cs
 * Tests DivikResult class.
 *
   Copyright 2017 Grzegorz Mrukwa, Michał Gallus

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
using Newtonsoft.Json;
using NUnit.Framework;
using Spectre.AlgorithmsCli.Methods;
using Spectre.Algorithms.Parameterization;
using Spectre.Algorithms.Results;
using Spectre.Data.Datasets;

namespace Spectre.Algorithms.Tests.Results
{
    [TestFixture, Category("Algorithm"), Category("VeryLong")]
    public class DivikResultTests
    {
        private DivikResult _result;
        private Segmentation _segmentation;
        private readonly string TestDirectory = TestContext.CurrentContext.TestDirectory + "\\..\\..\\..\\..\\..\\test_files";
        private readonly string _testFilePath = TestContext.CurrentContext.TestDirectory + "\\..\\..\\..\\..\\..\\test_files\\hnc1_tumor.txt";

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            var dataset = new BasicTextDataset(_testFilePath);
            var options = DivikOptions.ForLevels(1);
            options.MaxK = 2;
            options.Caching = false;
            options.PlottingPartitions = false;
            options.PlottingDecomposition = false;
            options.PlottingDecompositionRecursively = false;
            options.PlottingRecursively = false;
            options.UsingAmplitudeFiltration = false;
            _segmentation = new Segmentation();
            _result = _segmentation.Divik(dataset, options);
        }

        [OneTimeTearDown]
        public void TearDownFixture()
        {
            _segmentation.Dispose();
            _segmentation = null;
        }

        [Test]
        public void Save()
        {
            string path = TestDirectory + "\\test-path.json";
            _result.Save(path, true);

            Assert.True(File.Exists(path), "File doesn't exist");
            Assert.AreNotEqual(0, new FileInfo(path).Length, "File is empty");

            string jsonData = File.ReadAllText(path);
            DivikResult deserialisedResult = JsonConvert.DeserializeObject<DivikResult>(jsonData);

            Assert.AreEqual(_result, deserialisedResult, "Divik results differ");
            File.Delete(path);
        }

        [Test]
        public void SavedIdented()
        {
            string path = TestDirectory + "\\test-path.json";
            _result.Save(path, false);

            string contents = File.ReadAllText(path);
            Assert.False(contents.Contains("\n"), "Non-idented formatting contains new line");
            File.Delete(path);
        }

        [Test]
        public void EqualsAgainstIdenticalInstance()
        {
            var dataset = new BasicTextDataset(_testFilePath);
            var options = DivikOptions.ForLevels(1);
            options.MaxK = 2;
            options.Caching = false;
            options.PlottingPartitions = false;
            options.PlottingDecomposition = false;
            options.PlottingDecompositionRecursively = false;
            options.PlottingRecursively = false;
            options.UsingAmplitudeFiltration = false;
            var result = _segmentation.Divik(dataset, options);

            Assert.True(result.Equals(_result), "Equal objects not indicated.");
        }

        [Test]
        public void EqualsAgainstDifferentInstance()
        {
            var dataset = new BasicTextDataset(_testFilePath);
            var options = DivikOptions.ForLevels(1);
            options.MaxK = 2;
            options.Caching = false;
            options.PlottingPartitions = false;
            options.PlottingDecomposition = false;
            options.PlottingDecompositionRecursively = false;
            options.PlottingRecursively = false;
            options.UsingVarianceFiltration = false;
            var result = _segmentation.Divik(dataset, options);

            Assert.False(result.Equals(_result), "Unequal objects not indicated.");
        }

        [Test]
        public void EqualsAgainstNull()
        {
            Assert.False(_result.Equals(null), "Instance equalized with null");
        }

        [Test]
        public void EqualityOperatorForNulls()
        {
            DivikResult r1 = null;
            DivikResult r2 = null;
            Assert.True(r1 == r2, "Nulls not indicated as equal");
        }

        [Test]
        public void DifferenceOperatorForNulls()
        {
            DivikResult r1 = null;
            DivikResult r2 = null;
            Assert.False(r1 != r2, "Nulls indicated as unequal");
        }

        [Test]
        public void EqualityOperatorAgainstNull()
        {
            DivikResult r1 = null;
            Assert.False(r1 == _result, "null indicated equal to instance");
        }

        [Test]
        public void InequalityOperatorAgainstNull()
        {
            DivikResult r1 = null;
            Assert.True(r1 != _result, "null not indicated unequal to instance");
        }
    }
}
