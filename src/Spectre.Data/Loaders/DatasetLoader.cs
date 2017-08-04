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
using Spectre.Data.Datasets;

namespace Spectre.Data.Loaders
{
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

        #region Constructors
        /// <summary>
        /// Default constructor. Marked private so there is no stateless instance.
        /// </summary>
        private DatasetLoader()
        {
            
        }

        /// <summary>
        /// Constructor specifying roots for local and remote directories.
        /// </summary>
        /// <param name="localRoot">Root for local directory.</param>
        /// <param name="remoteRoot">Root for remote directory.</param>
        public DatasetLoader(string localRoot, string remoteRoot)
        {
            _localRoot = localRoot;
            _remoteRoot = remoteRoot;
        }
        #endregion

        #region Loaders
        public IDataset GetFromName(string name)
        {
            string fullPathLocal = _localRoot + "\\" + name;
            if (File.Exists(fullPathLocal))
                return new BasicTextDataset(fullPathLocal);

            string fullPathRemote = _remoteRoot + "\\" + name;
            if (File.Exists(fullPathRemote))
                return new BasicTextDataset(fullPathRemote);

            throw new ArgumentException("Dataset \"" + name + "\" not found neither locally nor remotely.");
        }

        public IDataset GetFromHash(string hash)
        {
            string name = "";
            //translate hash to name from database
            return GetFromName(name);
        }

        public IDataset GetFromUserId(string userId)
        {
            string name = "";
            //translate userId to name from database
            return GetFromName(name);
        }
        #endregion
    }
}
