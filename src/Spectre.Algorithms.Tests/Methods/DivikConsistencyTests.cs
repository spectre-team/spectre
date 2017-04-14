/*
 * DivikConsistencyTests.cs
 * Checks, whether current implementation of DiviK algorithm provides the same results.
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

using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using Spectre.Algorithms.Methods;
using Spectre.Algorithms.Methods.Utils;
using Spectre.Algorithms.Parameterization;
using Spectre.Algorithms.Results;
using Spectre.Data.Datasets;

namespace Spectre.Algorithms.Tests.Methods
{
    [TestFixture, Category("Algorithm")]
    public class DivikConsistencyTests
    {
        private Segmentation _segmentation;
        private readonly string _testDirectory = TestContext.CurrentContext.TestDirectory + "\\..\\..\\..\\..\\..\\test_files";

        [OneTimeSetUp]
        public void SetUpClass()
        {
            _segmentation = new Segmentation();
        }

        [OneTimeTearDown]
        public void TearDownClass()
        {
            _segmentation.Dispose();
        }

        [Test, Category("VeryLong")]
        public void BigDataSetEuclideanTest()
        {
            var datasetFilename = _testDirectory + "\\single.txt";
            var resultPath = _testDirectory + "\\expected_divik_results\\single\\euclidean\\divik-result.json";

            var options = DivikOptions.ForLevels(2);
            options.Metric = Metric.Euclidean;

            TestForDataset(datasetFilename, resultPath, options, true);
        }

        [Test, Category("VeryLong")]
        public void BigDataSetPearsonTest()
        {
            var datasetFilename = _testDirectory + "\\single.txt";
            var resultPath = _testDirectory + "\\expected_divik_results\\single\\pearson\\divik-result.json";

            var options = DivikOptions.ForLevels(2);

            TestForDataset(datasetFilename, resultPath, options, true);
        }

        [Test, Category("VeryLong")]
        public void BigDataSetPearsonNoFiltersTest()
        {
            var datasetFilename = _testDirectory + "\\single.txt";
            var resultPath = _testDirectory + "\\expected_divik_results\\single\\pearson_no_filters\\divik-result.json";

            var options = DivikOptions.ForLevels(2);
            options.UsingAmplitudeFiltration = false;
            options.UsingVarianceFiltration = false;
            
            TestForDataset(datasetFilename, resultPath, options, true);
        }

        [Test]
        public void MediumDataSetEuclideanTest()
        {
            var datasetFilename = _testDirectory + "\\hnc1_tumor.txt";
            var resultPath = _testDirectory + "\\expected_divik_results\\hnc1_tumor\\euclidean\\divik-result.json";

            var options = DivikOptions.ForLevels(1);
            options.Metric = Metric.Euclidean;
            options.UsingAmplitudeFiltration = false;

            TestForDataset(datasetFilename, resultPath, options, false);
        }

        [Test]
        public void MediumDataSetPearsonTest()
        {
            var datasetFilename = _testDirectory + "\\hnc1_tumor.txt";
            var resultPath = _testDirectory + "\\expected_divik_results\\hnc1_tumor\\pearson\\divik-result.json";

            var options = DivikOptions.ForLevels(1);
            options.UsingAmplitudeFiltration = false;

            TestForDataset(datasetFilename, resultPath, options, false);
        }

        [Test]
        public void SyntheticDataSetTest()
        {
            var datasetFilename = _testDirectory + "\\synthetic_1.txt";
            var resultPath = _testDirectory + "\\expected_divik_results\\synthetic\\1\\divik-result.json";

            var options = DivikOptions.ForLevels(5);
            options.Metric = Metric.Euclidean;
            options.UsingAmplitudeFiltration = false;
            options.UsingVarianceFiltration = false;

            TestForDataset(datasetFilename, resultPath, options, true);
        }

        private void TestForDataset(string dataPath, string resultPath, DivikOptions options, bool checkNested)
        {
            var dataset = new BasicTextDataset(dataPath);

            DivikResult result;
            using (var captureService = new Service.ConsoleCaptureService())
            {
                captureService.Written += (caller, text) => System.Diagnostics.Debug.Write(text);
                result = _segmentation.Divik(dataset, options);
            }

            var referenceJson = File.ReadAllText(resultPath);
            var referenceResult = JsonConvert.DeserializeObject<DivikResult>(referenceJson);

            var equalOnTop = Partition.Compare(result.Partition, referenceResult.Partition);
            var equalDownmerged = Partition.Compare(result.Merged, referenceResult.Merged);

            if(checkNested)
                Assert.Multiple(() =>
                {
                    Assert.True(equalOnTop, "Not equal on top.");
                    Assert.True(equalDownmerged, "Not equal in nested.");
                });
            else
                Assert.True(equalOnTop, "Not equal on top.");
        }
    }
}
