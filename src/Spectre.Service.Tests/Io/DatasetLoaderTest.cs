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

using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using NUnit.Framework;
using Spectre.Dependencies;
using Spectre.Dependencies.Modules;
using Spectre.Service.Configuration;
using Spectre.Service.Io;

namespace Spectre.Service.Tests.Io
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
        [OneTimeSetUp]
        public void SetUp()
        {
            DependencyResolver.AddModule(new MockModule());

            var localDirFull = Path.Combine(_rootDir, _localDir);
            var remoteDirFull = Path.Combine(_rootDir, _remoteDir);
            var correctDataset = File.ReadAllText(Path.Combine(_fileDir, "small-test.txt"));

            _mockFileSystem = DependencyResolver.GetService<IFileSystem>() as MockFileSystem;

            _mockFileSystem.AddFile(Path.Combine(localDirFull, "local_correct.txt"), new MockFileData(correctDataset));
            _mockFileSystem.AddFile(Path.Combine(remoteDirFull, "remote_correct.txt"), new MockFileData(correctDataset));
            _mockFileSystem.AddFile(Path.Combine(localDirFull, "local_incorrect.txt"), new MockFileData(textContents: "incorrect_data"));
            _mockFileSystem.AddFile(Path.Combine(remoteDirFull, "remote_incorrect.txt"), new MockFileData(textContents: "incorrect_data"));

            _rootConfig = new DataRootConfig(localDirFull, remoteDirFull);
            _datasetLoader = new DatasetLoader(_rootConfig);
        }

        [Test]
        public void ReturnsFromCorrectNameLocal()
        {
            Assert.IsNotNull(anObject: _datasetLoader.GetFromName(name: "local_correct"),
                message: "Loader did not manage to load local file.");
        }

        [Test]
        public void ReturnsFromCorrectNameRemote()
        {
            Assert.IsNotNull(anObject: _datasetLoader.GetFromName(name: "remote_correct"),
                message: "Loader did not manage to load remote file.");
        }

        [Test]
        public void ThrowsOnIncorrectName()
        {
            Assert.Throws<DatasetNotFoundException>(code: () => _datasetLoader.GetFromName(name: "invalid_name"),
                message: "Loader accepted invalid file name.");
        }

        [Test]
        public void ThrowsOnIncorrectFileContents()
        {
            Assert.Throws<DatasetFormatException>(code: () => _datasetLoader.GetFromName(name: "local_incorrect"),
                message: "Loader did not manage to load remote file.");
        }

        [Test]
        public void DeletesIncorrectFilesFromLocal()
        {
            try
            {
                _datasetLoader.GetFromName(name: "remote_incorrect");
            }
            catch (DatasetFormatException)
            {
                // ignored
            }
            var result = _mockFileSystem.AllFiles.FirstOrDefault(predicate: file => file.Contains(value: Path.Combine(_localDir, "remote_incorrect")));
            Assert.IsNull(result, message: "Loader leaves copies of incorrect files in local directory.");
        }
    }
}
