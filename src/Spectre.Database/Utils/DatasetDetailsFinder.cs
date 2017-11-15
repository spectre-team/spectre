/*
 * DatasetDetailsFinder.cs
 * Class for translating hash and friendly name to name of the file.
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

namespace Spectre.Database.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Spectre.Database.Contexts;
    using Spectre.Database.Entities;

    /// <summary>
    /// Class for finding the path of chosen property.
    /// </summary>
    public class DatasetDetailsFinder : IDatasetDetailsFinder
    {
        /// <summary>
        /// The context used for queries.
        /// </summary>
        private DatasetsContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetDetailsFinder"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DatasetDetailsFinder(DatasetsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Query for translating hash to upload number.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns>
        /// Returns UploadNumber for Hash.
        /// Null for not existing Hash.
        /// </returns>
        public virtual string HashToUploadNumberOrDefault(string hash)
        {
            if (_context.Datasets.Any(o => o.Hash == hash))
            {
                var dataset = _context.Datasets
                    .Where(b => b.Hash == hash)
                    .FirstOrDefault();

                return dataset.UploadNumber.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Query for translating friendly name to upload number.
        /// </summary>
        /// <param name="friendlyname">The friendlyname.</param>
        /// <returns>
        /// Returns UploadNumber for friendly name.
        /// Null for not existing friendly name.
        /// </returns>
        public virtual string FriendlyNameToUploadNumberOrDefault(string friendlyname)
        {
            if (_context.Datasets.Any(o => o.FriendlyName == friendlyname))
            {
                var dataset = _context.Datasets
                    .Where(b => b.FriendlyName == friendlyname)
                    .FirstOrDefault();

                return dataset.UploadNumber;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Query for translating upload number to hash.
        /// </summary>
        /// <param name="uploadnumber">The uploadnumber.</param>
        /// <returns>
        /// Returns hash for upload name.
        /// Null for not existing upload name.
        /// </returns>
        public virtual string UploadNumberToHashOrDefault(string uploadnumber)
        {
            if (_context.Datasets.Any(o => o.UploadNumber == uploadnumber))
            {
                var dataset = _context.Datasets
                    .Where(b => b.UploadNumber == uploadnumber)
                    .FirstOrDefault();

                return dataset.Hash;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Query for translating hash to friendly name.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns>
        /// Returns FriendlyName for Hash.
        /// Null for not existing Hash.
        /// </returns>
        public virtual string HashToFriendlyNameOrDefault(string hash)
        {
            if (_context.Datasets.Any(o => o.Hash == hash))
            {
                var dataset = _context.Datasets
                    .Where(b => b.Hash == hash)
                    .FirstOrDefault();

                return dataset.FriendlyName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Query for translating upload number to friendly name.
        /// </summary>
        /// <param name="uploadnumber">The uploadnumber.</param>
        /// <returns>
        /// Returns friendly name for given upload number.
        /// Null for not existing upload number.
        /// </returns>
        public virtual string UploadNumberToFriendlyNameOrDefault(string uploadnumber)
        {
            if (_context.Datasets.Any(o => o.UploadNumber == uploadnumber))
            {
                var dataset = _context.Datasets
                    .Where(b => b.UploadNumber == uploadnumber)
                    .FirstOrDefault();

                return dataset.FriendlyName;
            }
            else
            {
                return null;
            }
        }
    }
}
