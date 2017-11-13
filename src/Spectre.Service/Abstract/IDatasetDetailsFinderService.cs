/*
 * IDatasetDetailsFinderService.cs
 * Interface for DatasetDetailsFinderService
 *
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

namespace Spectre.Service.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Spectre.Database.Contexts;
    using Spectre.Database.Utils;

    /// <summary>
    /// Interface for DatasetDetailsFinderService
    /// </summary>
    public interface IDatasetDetailsFinderService
    {
        /// <summary>
        /// Finds the hash using friendly name.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns>
        /// Returns UploadNumber basing on hash.
        /// Null for not existing hash.
        /// </returns>
        string HashToUploadNumberOrDefault(string hash);

        /// <summary>
        /// Finds the friendky name using hash.
        /// </summary>
        /// <param name="friendlyname">The friendlyname.</param>
        /// <returns>
        /// Returns UploadNumber basing on friendly name.
        /// Null for not existing friendly name.
        /// </returns>
        string FriendlyNameToUploadNumberOrDefault(string friendlyname);

        /// <summary>
        /// Service translating uploadnumber to hash.
        /// </summary>
        /// <param name="uploadnumber">The uploadnumber.</param>
        /// <returns>
        /// Returns Hash having upload number.
        /// Null for not existing upload number.
        /// </returns>
        string UploadNumberToHashOrDefault(string uploadnumber);

        /// <summary>
        /// Query for translating hash to friendly name.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns>
        /// Returns FriendlyName for Hash.
        /// Null for not existing Hash.
        /// </returns>
        string HashToFriendlyNameOrDefault(string hash);

        /// <summary>
        /// Query for translating upload number to friendly name.
        /// </summary>
        /// <param name="uploadnumber">The uploadnumber.</param>
        /// <returns>
        /// Returns friendly name for given upload number.
        /// Null for not existing upload number.
        /// </returns>
        string UploadNumberToFriendlyNameOrDefault(string uploadnumber);
    }
}