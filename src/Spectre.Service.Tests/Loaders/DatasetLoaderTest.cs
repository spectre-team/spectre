/*
 * DatasetLoaderTest.cs
 * Tests for dataset loader class.
 *
   Copyright 2017 Dariusz Kuchta
   
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
using NUnit.Framework;
using Spectre.Service.Configuration;
using Spectre.Service.Loaders;

namespace Spectre.Service.Tests.Loaders
{
    [TestFixture]
    public class DatasetLoaderTest
    {
        private DataRootConfig _rootConfig;
        private DatasetLoader _datasetLoader;

        private readonly string _fileDir = TestContext.CurrentContext.TestDirectory
                                           + "\\..\\..\\..\\..\\..\\test_files";
        [SetUp]
        public void SetUp()
        {
            _rootConfig = new DataRootConfig(_fileDir, _fileDir);
            _datasetLoader = new DatasetLoader(_rootConfig);
        }

        [Test]
        public void ReturnsFromCorrectName()
        {
            Assert.NotNull(_datasetLoader.GetFromName(name: "small-test.txt"));
        }

        [Test]
        public void ThrowsOnIncorrectName()
        {
            Assert.Throws<ArgumentException>(code: () => _datasetLoader.GetFromName(name: "invalid_name"));
        }
    }
}
