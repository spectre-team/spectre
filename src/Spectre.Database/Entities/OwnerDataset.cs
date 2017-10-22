/*
 * OwnerDataset.cs
 * Dataset specifying the information about given dataset.
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spectre.Database.Entities
{
    /// <summary>
    /// Specifying the type of access for given dataset.
    /// </summary>
    public enum AccessType
    {
        /// <summary>
        /// Dataset available to the public.
        /// </summary>
        Public,

        /// <summary>
        /// Dataset available only to the owner.
        /// </summary>
        Private,

        /// <summary>
        /// Dataset available to users with link.
        /// </summary>
        Shared
    }

    /// <summary>
    /// Dataset specifying the information about given dataset.
    /// </summary>
    public class OwnerDataset
    {
        /// <summary>
        /// The identifier
        /// </summary>
        /// <value>
        /// The custom identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OwnerId { get; set; }

        /// <summary>
        /// The upload number
        /// </summary>
        /// <value>
        /// The upload number.
        /// </value>
        public string UploadNumber { get; set; }

        /// <summary>
        /// Gets or sets the checksum.
        /// </summary>
        /// <value>
        /// The checksum.
        /// </value>
        public string Checksum { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public AccessType Type { get; set; }
    }
}
