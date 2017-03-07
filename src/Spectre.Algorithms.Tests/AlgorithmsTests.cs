/*
 * AlgorithmsTests.cs
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
using System;
using Spectre.Algorithms.Parameterization;
using Spectre.Algorithms.Results;

namespace Spectre.Algorithms.Tests
{
	[TestFixture, Category("Algorithm")]
	public class AlgorithmsTests
	{
		Algorithms alg;

		[OneTimeSetUp]
		public void SetUpClass()
		{
			alg = new Algorithms();
		}

		[OneTimeTearDown]
		public void TearDownClass()
		{
			alg.Dispose();
		}

		[Test]
		public void RemoveBaseline()
		{
			double[] mz = { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 };
			double[,] data = { { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 },
				{ 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 } };

			double[,] result = alg.RemoveBaseline(mz, data);

			// Assert
			Assert.IsNotNull(result);
		}

		[Test]
		public void PeakAlignmentFFT()
		{
			double[] mz = { 1, 1, 1 };
			double[,] data = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };

			double[,] result = alg.AlignPeaksFft(mz, data);

			// Assert
			Assert.IsNotNull(result);
		}

		[Test]
		public void TicNorm()
		{
			double[,] data = { { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 },
				{ 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 } };

			double[,] result = alg.NormalizeByTic(data);

			// Assert
			Assert.IsNotNull(result);
		}

		[Test]
		public void EstimateGmm()
		{
			double[] mz = { 1, 2, 3 };
			double[,] data = { { 1, 1.1, 1.2 }, { 1, 1.1, 1.2 }, { 1, 1.1, 1.2 } };

			object result = alg.EstimateGmm(mz, data, false, false);

			Console.WriteLine(result);

			// Assert
			Assert.IsNotNull(result);
		}

		[Test]
		public void ApplyGmm()
		{
			double[,] data = { { 1, 1.1, 1.2 }, { 1, 1.1, 1.2 }, { 1, 1.1, 1.2 } };
			double[] mz = { 1, 2, 3 };
			var model = alg.EstimateGmm(mz, data, false, false);

			double[,] result = alg.ApplyGmm(model, data, mz);

            Console.WriteLine(result);

            // Assert
            Assert.IsNotNull(result);
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

            DivikResult result = alg.Divik(data, coordinates, options);
            
            // Assert
            Assert.IsNotNull(result);
		}
	}
}