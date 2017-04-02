/*
 * SegmentationTests.cs
 * Checks, whether MCR is properly called and result may be obtained.
 *
   Copyright 2017 Wilgierz Wojciech, Grzegorz Mrukwa

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
using System.IO;

namespace Spectre.Algorithms.Tests.Methods
{
	[TestFixture, Category("Algorithm")]
	public class SegmentationTests
	{
		private Segmentation _segmentation;
		private readonly string TestDirectory = TestContext.CurrentContext.TestDirectory + "\\..\\..\\..\\..\\..\\test_files";

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

		[Test]
		public void DivikSimple()
		{
            double[] mz = { 1, 2, 3, 4 };
            double[,] data = { { 1, 1, 1, 1 }, { 2, 2, 2, 2 }, { 2, 2, 2, 2 }, { 1, 1, 1, 1 } };
			int[,] coordinates = { { 1, 1 }, { 2, 2 }, { 1, 2 }, { 2, 1 } };
            IDataset dataset = new BasicTextDataset(mz, data, coordinates);

            var options = DivikOptions.ForLevels(1);
			options.UsingVarianceFiltration = false;
			options.UsingAmplitudeFiltration = false;
			options.MaxK = 2;
			options.Metric = Metric.Euclidean;

			DivikResult result = _segmentation.Divik(dataset, options);

			// Assert
			Assert.IsNotNull(result);
		}

        [Test, Category("VeryLong")]
        public void DivikBigData()
        {
            // path to directory with test project
            var path = TestDirectory + "\\hnc1_tumor.txt";
            var dataset = new BasicTextDataset(path);
            var options = DivikOptions.ForLevels(2);

            DivikResult result = _segmentation.Divik(dataset, options);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
