/*
 * DataRootConfig.cs
 * Data directory configuration & validation class.
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
using System.IO;

namespace Spectre.Service.Configuration
{
    /// <summary>
    /// Data directory root configuration class.
    /// </summary>
    public class DataRootConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataRootConfig"/> class,
        /// accepting local and remote directory paths.
        /// </summary>
        /// <param name="localPath">Local data directory path.</param>
        /// <param name="remotePath">Remote data directory path.</param>
        public DataRootConfig(string localPath, string remotePath)
        {
            ValidatePath(localPath);

            if (remotePath != null)
            {
                ValidatePath(remotePath);
            }
        }

        /// <summary>
        /// Local data directory root.
        /// </summary>
        public string LocalPath { get; private set; }

        /// <summary>
        /// Remote data directory root.
        /// </summary>
        public string RemotePath { get; private set; }

        /// <summary>
        /// Validates if argument is a valid path.
        /// </summary>
        /// <param name="path">File path to validate.</param>
        private void ValidatePath(string path)
        {
            var fi = new FileInfo(path);
        }
    }
}
