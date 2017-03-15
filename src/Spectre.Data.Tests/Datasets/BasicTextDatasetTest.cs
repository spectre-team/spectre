using System;
using NUnit.Framework;
using Spectre.Data.Datasets;

namespace Spectre.Data.Tests.Datasets
{
    [TestFixture, Category("Dataset")]
    public class BasicTextDatasetTest
    {
        private IDataset _dataset;

        [Test]
        public void CreateFromFileTest()
        {
            try
            {
                _dataset = new BasicTextDataset("small-test.txt");
            }
            catch (Exception e)
            {
                Assert.Fail("Dataset failed to initialize from file.", e);
            }
        }

        public void CreateFromRawDataTest()
        {
            double[] mz = null;
            double[,] data = null;

            try
            {
                _dataset = new BasicTextDataset(mz, data);
                Assert.Fail("Dataset accepted empty input.");
            }
            catch (Exception) { }

            mz = new[] {1.0, 2.0, 3.0};
            data = new[,] {{1.0, 2.0}};

            try
            {
                _dataset = new BasicTextDataset(mz, data);
                Assert.Fail("Dataset accepted data of different lengths.");
            }
            catch (Exception) { }

            data = new[,] { { 1, 1.1, 1.2 }, { 1, 1.1, 1.2 }, { 1, 1.1, 1.2 } };

            try
            {
                _dataset = new BasicTextDataset(mz, data);
            }
            catch (Exception)
            {
                Assert.Fail("Dataset failed to initialize from correct raw data.");
            }
        }
    }
}
