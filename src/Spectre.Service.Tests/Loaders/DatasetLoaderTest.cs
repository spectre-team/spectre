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
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Ninject;
using Ninject.Parameters;
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
        private MockFileSystem _mockFileSystem;

        private readonly string _rootDir = @"C:\spectre_data";
        private readonly string _localDir = "local";
        private readonly string _remoteDir = "remote";

        private readonly string _fileDir = $"{TestContext.CurrentContext.TestDirectory} + "
                                           + @"\..\..\..\..\..\test_files";
        [SetUp]
        public void SetUp()
        {
            var localDirFull = Path.Combine(_rootDir, _localDir);
            var remoteDirFull = Path.Combine(_rootDir, _remoteDir);
            var correctDataset = File.ReadAllText(Path.Combine(_fileDir, "small-test.txt"));

            _mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { localDirFull, new MockDirectoryData() },
                { remoteDirFull, new MockDirectoryData() },
                { Path.Combine(localDirFull, "local_correct.txt"), new MockFileData(correctDataset) },
                { Path.Combine(remoteDirFull, "remote_correct.txt"), new MockFileData(correctDataset) },
                { Path.Combine(localDirFull, "local_incorrect.txt"), new MockFileData(textContents: "incorrect_data") }
            });

            var kernel = new StandardKernel();
            kernel.Bind<IFileSystem>()
                .ToConstant(_mockFileSystem);

            _rootConfig = new DataRootConfig(localDirFull, remoteDirFull);

            _datasetLoader = kernel.Get<DatasetLoader>(new ConstructorArgument(name: "dataRootConfig", value: _rootConfig));
        }

        [Test]
        public void ReturnsFromCorrectNameLocal()
        {
            Assert.Throws<DatasetLoadException>(code: () => _datasetLoader.GetFromName(name: "local_correct.txt"), 
                                                message: "Loader did not manage to load local file.");
        }

        [Test]
        public void ReturnsFromCorrectNameRemote()
        {
            Assert.Throws<DatasetLoadException>(code: () => _datasetLoader.GetFromName(name: "remote_correct.txt"),
                                                message: "Loader did not manage to load remote file.");
        }

        [Test]
        public void ThrowsOnIncorrectName()
        {
            Assert.Throws<FileNotFoundException>(code: () => _datasetLoader.GetFromName(name: "invalid_name"), 
                                                message: "Loader accepted invalid file name.");
        }

        [Test]
        public void ThrowsOnIncorrectFileContents()
        {
            Assert.Throws<DatasetLoadException>(code: () => _datasetLoader.GetFromName(name: "local_incorrect.txt"),
                                                message: "Loader did not manage to load remote file.");
        }

        [Test]
        public void DeletesIncorrectFilesFrom()
        {
            try
            {
                _datasetLoader.GetFromName(name: "local_incorrect.txt");
            }
            catch (Exception)
            {
                // ignored
            }
            var result = _mockFileSystem.AllFiles.FirstOrDefault(predicate: file => file.Contains(value: "local_incorrect.txt"));
            Assert.IsNull(result, message: "Loader leaves copies of incorrect files in local directory.");
        }
    }
}
