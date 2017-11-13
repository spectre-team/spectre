/*
 * IDatasetDetailsFinder.cs
 * Interface for DatasetDetailsFinder.
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectre.Database.Utils
{
    /// <summary>
    /// Interface for the PathFinder class
    /// </summary>
    public interface IDatasetDetailsFinder
    {
        /// <summary>
        /// Query for translating hash to upload number.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns>
        /// Returns UploadNumber for Hash.
        /// Null for not existing Hash.
        /// </returns>
        string HashToUploadNumberOrDefault(string hash);

        /// <summary>
        /// Query for translating friendly name to upload number.
        /// </summary>
        /// <param name="friendlyname">The friendlyname.</param>
        /// <returns>
        /// Returns UploadNumber for friendly name.
        /// Null for not existing friendly name.
        /// </returns>
        string FriendlyNameToUploadNumberOrDefault(string friendlyname);

        /// <summary>
        /// Query for translating upload number to hash.
        /// </summary>
        /// <param name="uploadnumber">The uploadnumber.</param>
        /// <returns>
        /// Returns hash for upload name.
        /// Null for not existing upload name.
        /// </returns>
        string UploadNumberToHashOrDefault(string uploadnumber);

        /// <summary>
        /// Query for translating Hash to friendly name.
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
