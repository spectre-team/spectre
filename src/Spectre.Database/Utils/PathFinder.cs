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

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Database.Entities;

namespace Spectre.Database.Utils
{
    /// <summary>
    /// Class for finding the path of chosen property.
    /// </summary>
    public class PathFinder
    {
        /// <summary>
        /// Variable for returning location from hash
        /// </summary>
        private static string _locationfromhash;

        /// <summary>
        /// Variable for returning location from friendly name
        /// </summary>
        private static string _locationfromfriendlyname;

        /// <summary>
        /// The location
        /// </summary>
        private Dataset _path = new Dataset();

        /// <summary>
        /// The context description
        /// </summary>
        private Context _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathFinder"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PathFinder(Context context)
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
        public string ReturnForHash(string hash)
        {
            _path = _context.Datasets
                    .Where(b => b.Hash == hash)
                    .FirstOrDefault();

            PathFinder._locationfromhash = _path.Location.ToString();

            return PathFinder._locationfromhash;
        }

        /// <summary>
        /// Returns the name of friendly name.
        /// </summary>
        /// <param name="friendlyname">The friendlyname.</param>
        /// <returns>
        /// Returns location having friendly name.
        /// </returns>
        public string ReturnForFriendlyName(string friendlyname)
        {
            _path = _context.Datasets
                    .Where(b => b.FriendlyName == friendlyname)
                    .FirstOrDefault();

            PathFinder._locationfromfriendlyname = _path.Location.ToString();

            return PathFinder._locationfromfriendlyname;
        }
    }
}
