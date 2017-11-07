/*
 * DatasetsContext.cs
 * Database context for connections with database.
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

using System.Data.Entity;
using Spectre.Database.Entities;

namespace Spectre.Database.Contexts
{
    /// <summary>
    /// Context for database access.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class DatasetsContext : DbContext
    {
        /// <summary>
        /// Gets or sets the datasets.
        /// </summary>
        /// <value>
        /// The datasets.
        /// </value>
        public virtual DbSet<Dataset> Datasets { get; set; }

        /// <summary>
        /// Gets or sets the operation datasets.
        /// </summary>
        /// <value>
        /// The operation datasets.
        /// </value>
        public virtual DbSet<Operation> Operations { get; set; }
    }
}
