/*
 * DivikResult.cs
 * Class representing divik result.
 * Copyright 2017 Grzegorz Mrukwa, Sebastian Pustelnik, Daniel Babiak

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
using System.Runtime.Serialization;
using System.Web;

namespace Spectre.Models.Msi.DivikResult
{
    /// <summary>
    /// Provides divik result
    /// </summary>
    [DataContract]
    public class DivikResult
    {
        /// <summary>
        /// Divik data
        /// </summary>
        [DataMember]
        public IEnumerable<int> Data { get; set; }

        /// <summary>
        /// Gets or sets the x coordinates.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [DataMember]
        public IEnumerable<int> X { get; set; }

        /// <summary>
        /// Gets or sets the y coordinates.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [DataMember]
        public IEnumerable<int> Y { get; set; }
    }
}