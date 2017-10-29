/*
 * Dataset.cs
 * Database model of dataset metadata.
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
using Spectre.Database.Entities.Enums;

namespace Spectre.Database.Entities
{
    /// <summary>
    /// Abstraction of dataset metadata needed to load a dataset.
    /// </summary>
    public class Dataset
    {
        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DatasetId { get; set; }

        /// <summary>
        /// The upload number
        /// </summary>
        /// <value>
        /// The upload number.
        /// </value>
        public string UploadNumber { get; set; }

        /// <summary>
        /// Gets or sets the hash.
        /// </summary>
        /// <value>
        /// The hash.
        /// </value>
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the friendly name.
        /// </summary>
        /// <value>
        /// The friendly name
        /// </value>
        public string FriendlyName { get; set; }

        ///// <summary>
        ///// Gets or sets the owner.
        ///// </summary>
        ///// <value>
        ///// The owner.
        ///// </value>
        // public string Owner { get; set; } will be implemented with proper User entity when OAuth support lands

        /// <summary>
        /// Gets or sets the upload time.
        /// </summary>
        /// <value>
        /// The upload time.
        /// </value>
        public DateTime UploadTime { get; set; }

        /// <summary>
        /// Gets or sets the description of the dataset.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type dataset. Either public, private or shared.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public AccessType Type { get; set; }
    }
}
