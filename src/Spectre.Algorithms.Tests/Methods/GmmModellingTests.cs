/*
 * GmmModellingTests.cs
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
using System.Linq;
using NUnit.Framework;
using Spectre.Algorithms.Methods;
using Spectre.Data.Datasets;

namespace Spectre.Algorithms.Tests.Methods
{
	[TestFixture, Category("Algorithm")]
	public class GmmModellingTests
	{
		private GmmModelling _gmm;
        private readonly double[,] data = { { 1, 1.1, 1.2 }, { 1, 1.1, 1.2 }, { 1, 1.1, 1.2 } };
	    private readonly double[] mz = {1, 2, 3};

        [OneTimeSetUp]
		public void SetUpClass()
		{
			_gmm = new GmmModelling();
		}

		[OneTimeTearDown]
		public void TearDownClass()
		{
			_gmm.Dispose();
		}

		[Test]
		public void EstimateGmm()
		{
			IDataset dataset = new BasicTextDataset(mz, data, null);

			var result = _gmm.EstimateGmm(dataset);

			Console.WriteLine(result);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.True(data.Cast<double>()
                    .Take(data.GetLength(1))
                    .SequenceEqual(result.OriginalMeanSpectrum));
                Assert.True(mz.SequenceEqual(result.OriginalMz));
                Assert.False(result.IsMerged);
                Assert.False(result.IsNoiseReduced);
                Assert.Null(result.MzMergingThreshold);
            });
        }

		[Test]
		public void ApplyGmm()
		{
			IDataset dataset = new BasicTextDataset(mz, data, null);

            var model = _gmm.EstimateGmm(dataset);

			IDataset result = _gmm.ApplyGmm(model, dataset);

			Console.WriteLine(result);

			// Assert
            Assert.IsNotNull(result);
		}

	    [Test]
	    public void ReduceByHeight()
	    {
            IDataset dataset = new BasicTextDataset(mz, data, null);

            var model = _gmm.EstimateGmm(dataset);

	        Assert.Throws<NotImplementedException>(() =>
	        {
	            var reduced = _gmm.ReduceModelByComponentHeight(model);
	        });

	        //var reduced = _gmm.ReduceModelByComponentHeight(model);

	        //Assert.Multiple(() =>
	        //{
	        //    Assert.IsNotNull(reduced);
	        //    Assert.True(data.Cast<double>()
	        //        .Take(data.GetLength(1))
	        //        .SequenceEqual(reduced.OriginalMeanSpectrum));
	        //    Assert.True(mz.SequenceEqual(reduced.OriginalMz));
	        //    Assert.False(reduced.IsMerged);
	        //    Assert.True(reduced.IsNoiseReduced);
	        //    Assert.Null(reduced.MzMergingThreshold);
	        //});
	    }

	    [Test]
	    public void ReduceByArea()
	    {
            IDataset dataset = new BasicTextDataset(mz, data, null);

            var model = _gmm.EstimateGmm(dataset);

	        Assert.Throws<NotImplementedException>(() =>
	        {
	            var reduced = _gmm.ReduceModelByComponentArea(model);
	        });

	        //var reduced = _gmm.ReduceModelByComponentArea(model);

	        //Assert.Multiple(() =>
	        //{
	        //    Assert.IsNotNull(reduced);
	        //    Assert.True(data.Cast<double>()
	        //        .Take(data.GetLength(1))
	        //        .SequenceEqual(reduced.OriginalMeanSpectrum));
	        //    Assert.True(mz.SequenceEqual(reduced.OriginalMz));
	        //    Assert.False(reduced.IsMerged);
	        //    Assert.True(reduced.IsNoiseReduced);
	        //    Assert.Null(reduced.MzMergingThreshold);
	        //});
	    }

	    [Test]
	    public void MergeComonents()
	    {
            IDataset dataset = new BasicTextDataset(mz, data, null);

            var model = _gmm.EstimateGmm(dataset);

            var merged = _gmm.MergeComponents(model, mzThreshold: 0.3);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(merged);
                Assert.True(data.Cast<double>()
                    .Take(data.GetLength(1))
                    .SequenceEqual(merged.OriginalMeanSpectrum));
                Assert.True(mz.SequenceEqual(merged.OriginalMz));
                Assert.True(merged.IsMerged);
                Assert.False(merged.IsNoiseReduced);
                Assert.NotNull(merged.MzMergingThreshold);
                Assert.AreEqual(0.3, merged.MzMergingThreshold.Value, 0.0001);
            });
        }

	    //[Test]
	    //public void ReduceAndMergeComponents()
	    //{
     //       IDataset dataset = new BasicTextDataset(mz, data);

     //       var model = _gmm.EstimateGmm(dataset);

     //       var merged = _gmm.MergeComponents(model, mzThreshold: 0.3);

     //       var reduced = _gmm.ReduceModelByComponentArea(merged);

     //       Assert.Multiple(() =>
     //       {
     //           Assert.IsNotNull(reduced);
     //           Assert.True(data.Cast<double>()
     //               .Take(data.GetLength(1))
     //               .SequenceEqual(reduced.OriginalMeanSpectrum));
     //           Assert.True(mz.SequenceEqual(reduced.OriginalMz));
     //           Assert.True(reduced.IsMerged);
     //           Assert.True(reduced.IsNoiseReduced);
     //           Assert.NotNull(reduced.MzMergingThreshold);
     //           Assert.AreEqual(0.3, reduced.MzMergingThreshold.Value, 0.0001);
     //       });
     //   }
	}
}
