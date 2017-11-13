/*
 * Operation.cs
 * Entity providing informations about operation.
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Spectre.Database.Entities.Enums;

namespace Spectre.Database.Entities
{
    /// <summary>
    /// Class for specifying information about the operation performed on the dataset.
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OperationId { get; set; }

        /// <summary>
        /// Gets or sets the inputs.
        /// </summary>
        /// <value>
        /// The inputs.
        /// </value>
        public List<Dataset> Inputs { get; set; }

        /// <summary>
        /// Gets or sets the outputs.
        /// </summary>
        /// <value>
        /// The outputs.
        /// </value>
        public List<Dataset> Outputs { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>
        /// The parameters. (JSON string)
        /// </value>
        public string Parameters { get; set; }

        /// <summary>
        /// Gets or sets the type of the operation.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        public OperationType Type { get; set; }

        /// <summary>
        /// Gets or sets the operation status.
        /// </summary>
        /// <value>
        /// The operation status.
        /// </value>
        public Status OperationStatus { get; set; }

        /// <summary>
        /// Gets or sets the information about the validation error.
        /// </summary>
        /// <value>
        /// The log.
        /// </value>
        public string Log { get; set; }
    }
}
