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
using NUnit.Framework;
using Spectre.Algorithms.Methods;
using Spectre.Algorithms.Parameterization;
using Spectre.Algorithms.Results;

namespace Spectre.Algorithms.Tests.Methods
{
	[TestFixture, Category("Algorithm")]
	public class SegmentationTests
	{
		Segmentation _segmentation;

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
		public void Divik()
		{
			double[,] data = { { 1, 1, 1, 1 }, { 2, 2, 2, 2 }, { 2, 2, 2, 2 }, { 1, 1, 1, 1 } };
			int[,] coordinates = { { 1, 1 }, { 2, 2 }, { 1, 2 }, { 2, 1 } };
			var options = DivikOptions.ForLevels(1);
			options.UsingVarianceFiltration = false;
			options.UsingAmplitudeFiltration = false;
			options.MaxK = 2;
			options.Metric = Metric.Euclidean;

			DivikResult result = _segmentation.Divik(data, coordinates, options);

			// Assert
			Assert.IsNotNull(result);
		}
	}
}
