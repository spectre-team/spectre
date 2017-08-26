/*
 * DatasetLoader.cs
 * Class loading datasets from either local root or remote root.
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
using System.IO;
using System.IO.Abstractions;
using Ninject;
using Spectre.Data.Datasets;
using Spectre.Service.Configuration;

namespace Spectre.Service.Loaders
{
    /// <summary>
    /// Class for loading datasets from specified directories.
    /// </summary>
    public class DatasetLoader
    {
        #region Fields

        /// <summary>
        /// Root for local directory.
        /// </summary>
        private readonly string _localRoot;

        /// <summary>
        /// Root for remote directory.
        /// </summary>
        private readonly string _remoteRoot;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetLoader"/> class.
        /// </summary>
        /// <param name="dataRootConfig">Validated configuration of needed directories.</param>
        public DatasetLoader(DataRootConfig dataRootConfig)
        {
            _localRoot = dataRootConfig.LocalPath;
            _remoteRoot = dataRootConfig.RemotePath;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Handle to file system.
        /// </summary>
        [Inject]
        public IFileSystem FileSystem { private get; set; }

        #endregion

        #region Loaders

        /// <summary>
        /// Returns dataset basing directly on the name of the file.
        /// </summary>
        /// <param name="name">File name.</param>
        /// <returns>Found dataset.</returns>
        /// <exception cref="ArgumentException">Throws when the file is not found both locally and remotely.</exception>
        /// <exception cref="IOException">Throws when the loader fails to create dataset from the file.</exception>
        public IDataset GetFromName(string name)
        {
            string fullPathLocal = Path.Combine(_localRoot, name);

            if (!FileSystem.File.Exists(fullPathLocal))
            {
                string fullPathRemote = Path.Combine(_remoteRoot, name);
                if (!FileSystem.File.Exists(fullPathRemote))
                {
                    throw new ArgumentException($"Dataset '{name}' not found neither locally nor remotely.", nameof(name));
                }

                FileSystem.File.Copy(fullPathRemote, fullPathLocal);
            }

            try
            {
                return new BasicTextDataset(fullPathLocal);
            }
            catch (IOException)
            {
                FileSystem.File.Delete(fullPathLocal);
                throw;
            }
        }

        /*
        //  TODO: @dkuchta: complete the methods below when database access is available
        /// <summary>
        /// Returns dataset from database, translating hash of the file into file directory.
        /// </summary>
        /// <param name="hash">Hash as a key to database.</param>
        /// <returns>Found dataset.</returns>
        public IDataset GetFromHash(string hash)
        {
            string name = "";
            //translate hash to name from database
            return GetFromName(name);
        }

        /// <summary>
        /// Returns dataset from database, translating ID of the file into file directory.
        /// </summary>
        /// <param name="userId">ID as a key to database.</param>
        /// <returns>Found dataset.</returns>
        public IDataset GetFromUserId(string userId)
        {
            string name = "";
            //translate userId to name from database
            return GetFromName(name);
        }
        */
        #endregion
    }
}
