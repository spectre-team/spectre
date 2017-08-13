using NUnit.Framework;
using Spectre.Service.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectre.Service.Tests
{
    [TestFixture]
    class DataRootConfigTest
    {
        private string goodLocalPath  = @"\Path\To\Data";
        private string wrongLocalPath = @"\Path\To\*";

        private string goodRemotePath  = @"\\Path\To\Data";
        private string wrongRemotePath = @"\\Path\To\*";

        [Test]
        public void DataRootThrowsOnIncorrectLocalPath()
        {
            Assert.Throws<ArgumentNullException>(code: () => new DataRootConfig(null, goodRemotePath));
            Assert.Throws<ArgumentException>(code: () => new DataRootConfig(wrongLocalPath, goodRemotePath));
        }

        [Test]
        public void DataRootThrowsOnIncorrectRemotePath()
        {
            Assert.Throws<ArgumentException>(code: () => new DataRootConfig(goodLocalPath, wrongRemotePath));
        }

        [Test]
        public void DataRootAcceptsNoRemotePath()
        {
            Assert.DoesNotThrow(code: () => new DataRootConfig(goodLocalPath, null));
        }

        [Test]
        public void DataRootWorksForCorrectPaths()
        {
            Assert.DoesNotThrow(code: () => new DataRootConfig(goodLocalPath, goodRemotePath));
        }
    }
}
