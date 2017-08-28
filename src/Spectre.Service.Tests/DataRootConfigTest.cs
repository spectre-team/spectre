/*
 * DataRootConfigTest.cs
 * Tests for data directory root configuration.
 *
   Copyright 2017 Maciej Gamrat

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
        public void DataRootThrowsOnMissingLocalPath()
        {
            Assert.Throws<ConfigValidationException>(code: () => new DataRootConfig(null, goodRemotePath));
        }

        [Test]
        public void DataRootThrowsOnInvalidLocalPath()
        {
            Assert.Throws<ConfigValidationException>(code: () => new DataRootConfig(wrongLocalPath, goodRemotePath));
        }

        [Test]
        public void DataRootThrowsOnMissingRemotePath()
        {
            Assert.Throws<ConfigValidationException>(code: () => new DataRootConfig(goodLocalPath, null));
        }

        [Test]
        public void DataRootThrowsOnInvalidRemotePath()
        {
            Assert.Throws<ConfigValidationException>(code: () => new DataRootConfig(goodLocalPath, wrongRemotePath));
        }

        [Test]
        public void DataRootDoesNotThrowForCorrectPaths()
        {
            Assert.DoesNotThrow(code: () => new DataRootConfig(goodLocalPath, goodRemotePath));
        }

        [Test]
        public void DataRootReturnsInputValues()
        {
            var dataRootConfig = new DataRootConfig(goodLocalPath, goodRemotePath);
            Assert.AreEqual(expected: goodLocalPath, actual: dataRootConfig.LocalPath);
            Assert.AreEqual(expected: goodRemotePath, actual: dataRootConfig.RemotePath);
        }
    }
}
