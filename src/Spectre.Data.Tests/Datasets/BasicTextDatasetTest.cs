using System;
using NUnit.Framework;
using Spectre.Data.Datasets;

namespace Spectre.Data.Tests.Datasets
{
    [TestFixture]
    public class BasicTextDatasetTest
    {
        private BasicTextDataset _dataset;

        [OneTimeSetUp]
        public void SetUpClass()
        {
            _dataset = new BasicTextDataset();
        }

        [Test]
        public void LoadFromFile()
        {
            throw new NotImplementedException();
        }
        [Test]
        public void LoadFromRawData()
        {
            throw new NotImplementedException();
        }
        [Test]
        public void GetElements()
        {
            throw new NotImplementedException();
        }
        [Test]
        public void GetSubArray()
        {
            throw new NotImplementedException();
        }
    }
}
