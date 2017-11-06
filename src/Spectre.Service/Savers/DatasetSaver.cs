/*
 * DatasetSaver.cs
 * Class saving datasets from either local cache or memory.
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
using System.Linq;
using Spectre.Data.Datasets;
using Spectre.Dependencies;
using Spectre.Service.Configuration;
using Spectre.Service.Loaders;

namespace Spectre.Service.Savers
{
    /// <summary>
    /// Class saving datasets from either local cache or memory.
    /// </summary>
    public class DatasetSaver
    {
        #region Fields

        /// <summary>
        /// Root for cache directory.
        /// </summary>
        private readonly string _cacheRoot;

        /// <summary>
        /// Root for remote directory.
        /// </summary>
        private readonly string _remoteRoot;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetSaver"/> class.
        /// </summary>
        /// <param name="dataRootConfig">Validated configuration of needed directories.</param>
        public DatasetSaver(DataRootConfig dataRootConfig)
        {
            _cacheRoot = dataRootConfig.LocalPath;
            _remoteRoot = dataRootConfig.RemotePath;

            FileSystem = DependencyResolver.GetService<IFileSystem>();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Handle to file system.
        /// </summary>
        private IFileSystem FileSystem { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Method for saving given dataset from local cache directory into remote root.
        /// </summary>
        /// <param name="name">Name of the dataset.</param>
        public void SaveFromCache(string name)
        {
            var foundCachedFiles = FileSystem.Directory.GetFiles(_cacheRoot, name + ".*");
            if (foundCachedFiles.Length == 0)
            {
                throw new DatasetNotFoundException("Dataset file could not be found in the cache.", name);
            }
            var cachedFilePath = foundCachedFiles.First();
            string fullPathRemote = Path.Combine(_remoteRoot, Path.GetFileName(cachedFilePath));
            FileSystem.File.Copy(cachedFilePath, fullPathRemote);
        }

        /// <summary>
        /// Method for saving given dataset from application's memory into remote root.
        /// </summary>
        /// <param name="dataset">Dataset to be saved.</param>
        /// <param name="name">User-friendly name given to the dataset.</param>
        public void SaveFromMemory(IDataset dataset, string name)
        {
            string extension = ".txt";
            string fullPathRemote = FileSystem.Path.Combine(_remoteRoot, name + extension);
            dataset.SaveToFile(fullPathRemote);
        }

        #endregion
    }
}
