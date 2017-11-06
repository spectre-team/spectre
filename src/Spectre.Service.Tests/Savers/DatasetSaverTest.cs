/*
 * DatasetSaverTest.cs
 * Tests for dataset saver class.
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
using NUnit.Framework;
using Spectre.Data.Datasets;
using Spectre.Dependencies;
using Spectre.Dependencies.Modules;
using Spectre.Service.Configuration;
using Spectre.Service.Loaders;
using Spectre.Service.Savers;

namespace Spectre.Service.Tests.Savers
{
    [TestFixture]
    public class DatasetSaverTest
    {
        private DataRootConfig _rootConfig;
        private DatasetSaver _datasetSaver;
        private MockFileSystem _mockFileSystem;

        private readonly string _rootDir = @"C:\spectre_data";
        private readonly string _cacheDir = "cache";
        private readonly string _remoteDir = "remote";

        private readonly string _fileDir = $"{TestContext.CurrentContext.TestDirectory} + "
                                           + @"\..\..\..\..\..\test_files";
        [OneTimeSetUp]
        public void SetUp()
        {
            DependencyResolver.AddModule(new MockModule());

            var localDirFull = Path.Combine(_rootDir, _cacheDir);
            var remoteDirFull = Path.Combine(_rootDir, _remoteDir);
            var correctDataset = File.ReadAllText(Path.Combine(_fileDir, "small-test.txt"));

            _mockFileSystem = DependencyResolver.GetService<IFileSystem>() as MockFileSystem;
            _mockFileSystem.AddDirectory(localDirFull);
            _mockFileSystem.AddDirectory(remoteDirFull);
            _mockFileSystem.AddFile(Path.Combine(localDirFull, "local_correct.txt"), new MockFileData(correctDataset));

            _rootConfig = new DataRootConfig(localDirFull, remoteDirFull);
            _datasetSaver = new DatasetSaver(_rootConfig);
        }

        [Test]
        public void SavesFromCorrectNameCache()
        {
            Assert.DoesNotThrow(code: () => _datasetSaver.SaveFromCache(name: "local_correct"),
                message: "Saver did not manage to find the local file.");
            Assert.IsNotEmpty(collection: _mockFileSystem.Directory.GetFiles(Path.Combine(_rootDir, _remoteDir), 
                searchPattern: "local_correct.*"),
                message: "Saver did not manage to save the dataset from cache to remote directory.");
        }

        [Test]
        public void ThrowsOnIncorrectName()
        {
            Assert.Throws<DatasetNotFoundException>(
                code: () => _datasetSaver.SaveFromCache(name: "invalid_name"),
                message: "Saver accepted invalid file name.");
        }

        [Test]
        public void SavesFromMemory()
        {
            double[] mz = { 1.0, 2.0, 3.0 };
            double[,] data = { { 1, 2.1, 3.2 }, { 4, 5.1, 6.2 }, { 7, 8.1, 9.2 } };
            IDataset testDataset = new BasicTextDataset(mz, data, null);
            _datasetSaver.SaveFromMemory(testDataset, "testDataset");
            Assert.IsNotEmpty(collection: _mockFileSystem.Directory.GetFiles(Path.Combine(_rootDir, _remoteDir),
                    searchPattern: "testDataset.*"),
                message: "Saver did not manage to save the dataset from memory to remote directory.");
        }
    }
}
