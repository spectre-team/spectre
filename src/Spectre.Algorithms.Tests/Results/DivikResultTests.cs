/*
 * DivikResultTests.cs
 * Tests DivikResult class.
 * 
   Copyright 2017 Grzegorz Mrukwa

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
using NUnit.Framework;
using Spectre.Algorithms.Methods;
using Spectre.Algorithms.Parameterization;
using Spectre.Algorithms.Results;
using Spectre.Data.Datasets;

namespace Spectre.Algorithms.Tests.Results
{
    [TestFixture, Category("Algorithm")]
    public class DivikResultTests
    {
        private DivikResult _result;
        private Segmentation _segmentation;

        private readonly string _testFilePath = TestContext.CurrentContext.TestDirectory + "\\..\\..\\..\\small-test.txt";

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
            Assert.Throws<NotImplementedException>(() => _result.Save("test-path.json"));
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
            options.UsingAmplitudeFiltration = false;
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
