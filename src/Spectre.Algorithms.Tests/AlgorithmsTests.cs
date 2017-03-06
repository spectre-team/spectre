using NUnit.Framework;
using Spectre.Algorithms;
using System;
using System.Net.Configuration;

namespace Spectre.Algorithms.Tests
{
	[TestFixture, Category("Algorithm")]
	public class AlgorithmsTests
	{
		Spectre.Algorithms.Algorithms alg;

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

			double[,] result = alg.PeakAlignmentFFT(mz, data);

			// Assert
			Assert.IsNotNull(result);
		}

		[Test]
		public void TicNorm()
		{
			double[,] data = { { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 },
				{ 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 } };

			double[,] result = alg.TicNorm(data);

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
            double[,] data = { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } };
            int[,] coordinates = { { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 } };
            object[] varargin = { "Cache", false, "VarianceFiltration", false, "AmplitudeFiltration", false, "Level", 1.0, "MaxK", 2, "Metric", "euclidean" };

            DivikResult result = alg.Divik(data, coordinates, varargin);
            
            // Assert
            Assert.IsNotNull(result);
		}
	}
}