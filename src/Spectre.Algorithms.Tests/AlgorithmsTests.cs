using NUnit.Framework;
using Spectre.Algorithms;
using System;

namespace Spectre.Algorithms.Tests
{
	[TestFixture, Category("Algorithm")]
	public class AlgorithmsTests
	{
		Spectre.Algorithms.Algorithms alg = new Spectre.Algorithms.Algorithms();

		[Test]
		public void RemoveBaseline()
		{
			double[] mz = new double[] { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 };
			double[,] data = new double[,] { { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 },
				{ 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 } };

			System.Double[,] result = alg.RemoveBaseline(mz, data);

			// Assert
			Assert.IsNotNull(result);
		}

		[Test]
		public void PeakAlignmentFFT()
		{
			double[] mz = new double[] { 1, 1, 1 };
			double[,] data = new double[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };

			System.Double[,] result = alg.PeakAlignmentFFT(mz, data);

			// Assert
			Assert.IsNotNull(result);
		}

		[Test]
		public void TicNorm()
		{
			double[,] data = new double[,] { { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 },
				{ 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 } };

			System.Double[,] result = alg.TicNorm(data);

			// Assert
			Assert.IsNotNull(result);
		}

		[Test]
		public void EstimateGmm()
		{
			double[] mz = new double[] { 1, 2, 3 };
			double[,] data = new double[,] { { 1, 1.1, 1.2 }, { 1, 1.1, 1.2 }, { 1, 1.1, 1.2 } };

			object result = alg.EstimateGmm(mz, data, 0, 0);

			Console.WriteLine(result);

			// Assert
			Assert.IsNotNull(result);
		}

		[Test]
		public void ApplyGmm()
		{
			object data = 0;
			double[] mz = new double[5] { 1.1, 1.2, 0.97, 1.07, 1.02 };

			object result = alg.ApplyGmm(mz, data);

            Console.WriteLine(result);

            // Assert
            Assert.IsNotNull(result);
		}

		[Test]
		public void Divik()
		{
            double[,] data = new double[,] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } };
            int[,] coordinates = new int[,] { { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 } };
            object[] varargin = new object[] { "Cache", false, "VarianceFiltration", false, "AmplitudeFiltration", false, "Level", 1.0, "MaxK", 2, "Metric", "euclidean" };

            DivikResult result = alg.Divik(data, coordinates, varargin);
            
            // Assert
            Assert.IsNotNull(result);
		}
	}
}