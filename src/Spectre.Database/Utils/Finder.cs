/*
 * PathFinder.cs
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
    public class Finder : IFinder
    {
        /// <summary>
        /// The context description
        /// </summary>
        private Context _context;

        /// <summary>
        /// The dataset
        /// </summary>
        private Dataset _dataset = new Dataset();

        /// <summary>
        /// Initializes a new instance of the <see cref="Finder"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Finder(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns for hash.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns>
        /// Returns location having hash.
        /// </returns>
        public virtual string ReturnForHash(string hash)
        {
            if (_context.Datasets.Any(o => o.Hash == hash))
            {
                _dataset = _context.Datasets
                    .Where(b => b.Hash == hash)
                    .FirstOrDefault();

                return _dataset.UploadNumber.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the name of friendly name.
        /// </summary>
        /// <param name="friendlyname">The friendlyname.</param>
        /// <returns>
        /// Returns location having friendly name.
        /// </returns>
        public virtual string ReturnForFriendlyName(string friendlyname)
        {
            if (_context.Datasets.Any(o => o.FriendlyName == friendlyname))
            {
                _dataset = _context.Datasets
                    .Where(b => b.FriendlyName == friendlyname)
                    .FirstOrDefault();

                return _dataset.UploadNumber.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
