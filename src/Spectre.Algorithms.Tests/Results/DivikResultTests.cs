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
            using (var segmentation = new Segmentation())
            {
                _result = segmentation.Divik(dataset, options);
            }
        }

        [Test]
        public void Save()
        {
            string path = TestContext.CurrentContext.TestDirectory + "\\..\\..\\..\\test-path.json";
            _result.Save(path);

            Assert.True(File.Exists(path), "File doesn't exist");
            Assert.AreNotEqual(0, new FileInfo(path).Length, "File is empty");

            string jsonData = File.ReadAllText(path);
            DivikResult deserialisedResult = JsonConvert.DeserializeObject<DivikResult>(jsonData);

            Assert.AreEqual(_result.AmplitudeFilter, deserialisedResult.AmplitudeFilter, "The AmplitudeFilter differs from original");
            Assert.AreEqual(_result.AmplitudeThreshold, deserialisedResult.AmplitudeThreshold, "The AmplitudeThreshold differs from original");
            Assert.AreEqual(_result.Centroids, deserialisedResult.Centroids, "The Centroids differ from original");
            Assert.AreEqual(_result.Merged, deserialisedResult.Merged, "The Merged differs from original");
            Assert.AreEqual(_result.Partition, deserialisedResult.Partition, "The Partition differs from original");
            Assert.AreEqual(_result.QualityIndex, deserialisedResult.QualityIndex, "The QualityIndex differs from original");
            Assert.AreEqual(_result.VarianceFilter, deserialisedResult.VarianceFilter, "The VarianceFilter differs from original");
            Assert.AreEqual(_result.VarianceThreshold, deserialisedResult.VarianceThreshold, "The VarianceThreshold differs from original");
            Assert.AreEqual(_result.Subregions, deserialisedResult.Subregions, "The Subregions differ from original");
            File.Delete(path);
        }
    }
}
