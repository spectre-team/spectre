/*
 * PreprocessingTests.cs
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
using Spectre.Data.Datasets;

namespace Spectre.Algorithms.Tests.Methods
{
	[TestFixture, Category("Algorithm")]
	public class PreprocessingTests
	{
		Preprocessing _preprocessing;

		[OneTimeSetUp]
		public void SetUpClass()
		{
			_preprocessing = new Preprocessing();
		}

		[OneTimeTearDown]
		public void TearDownClass()
		{
			_preprocessing.Dispose();
		}

		[Test]
		public void RemoveBaseline()
		{
			double[] mz = { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 };
			double[,] data = { { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 },
				{ 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 } };
            IDataset dataset = new BasicTextDataset(mz, data);

            IDataset result = _preprocessing.RemoveBaseline(dataset);

			// Assert
			Assert.IsNotNull(result);
		}

		[Test]
		public void PeakAlignmentFFT()
		{
			double[] mz = { 1, 1, 1 };
			double[,] data = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            IDataset dataset = new BasicTextDataset(mz, data);

            IDataset result = _preprocessing.AlignPeaksFft(dataset);

			// Assert
			Assert.IsNotNull(result);
		}

		[Test]
		public void TicNorm()
		{
            double[] mz = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            double[,] data = { { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 },
				{ 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 }, { 1.1, 1.2, 0.97, 1.07, 1.02, 5, 1.2, 1.5, 1.6, 1.2 } };
            IDataset dataset = new BasicTextDataset(mz, data);

            IDataset result = _preprocessing.NormalizeByTic(dataset);

			// Assert
			Assert.IsNotNull(result);
		}
	}
}
