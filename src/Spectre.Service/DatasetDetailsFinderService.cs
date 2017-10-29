/*
 * DatasetDetailsFinderService.cs
   Class providing services for DatasetDetailsFinder. With use of this services
   it is possible to use querries from DatasetDetailsFinder without creating
   an instance of DatasetsContext manually.

   Copyright 2017 Roman Lisak

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

namespace Spectre.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Spectre.Database.Contexts;
    using Spectre.Database.Utils;
    using Spectre.Service.Abstract;

    /// <summary>
    /// PathFinder service.
    /// </summary>
    /// <seealso cref="Spectre.Service.Abstract.IDatasetDetailsFinderService" />
    internal class DatasetDetailsFinderService : IDatasetDetailsFinderService
    {
        /// <summary>
        /// The context
        /// </summary>
        private static DatasetsContext _context;

        /// <summary>
        /// The service object used durring calling the querries.
        /// </summary>
        private readonly DatasetDetailsFinder _service = new DatasetDetailsFinder(DatasetDetailsFinderService._context);

        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetDetailsFinderService" /> class.
        /// </summary>
        public DatasetDetailsFinderService()
        {
            DatasetDetailsFinderService._context = new DatasetsContext();
        }

        /// <summary>
        /// Service translating hash to upload number.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns>
        /// Returns upload number basing on Hash.
        /// Null for not existing hash.
        /// </returns>
        public string HashToUploadNumberOrDefault(string hash)
        {
            return _service.HashToUploadNumberOrDefault(hash);
        }

        /// <summary>
        /// Service translating friendly name to upload number.
        /// </summary>
        /// <param name="friendlyname">The friendlyname.</param>
        /// <returns>
        /// Returns upload number basing on friendly name.
        /// Null for not existing friendly name.
        /// </returns>
        public string FriendlyNameToUploadNumberOrDefault(string friendlyname)
        {
            return _service.FriendlyNameToUploadNumberOrDefault(friendlyname);
        }

        /// <summary>
        /// Service translating uploadnumber to hash.
        /// </summary>
        /// <param name="uploadnumber">The uploadnumber.</param>
        /// <returns>
        /// Returns Hash basing on upload number.
        /// Null for not existing upload number.
        /// </returns>
        public string UploadNumberToHashOrDefault(string uploadnumber)
        {
            return _service.UploadNumberToHashOrDefault(uploadnumber);
        }
    }
}
