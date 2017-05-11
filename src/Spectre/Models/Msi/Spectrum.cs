/*
 * Spectrum.cs
 * Class representing a single spectrum in a MALDI MSI sample.
 * 
   Copyright 2017 Grzegorz Mrukwa

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

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Spectre.Models.Msi
{
    /// <summary>
    /// Provides details about single spectrum in the dataset.
    /// </summary>
    [DataContract]
    public class Spectrum
    {
        /// <summary>
        /// Gets or sets the identifier of the spectrum.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the m/z values.
        /// </summary>
        /// <value>
        /// The mz.
        /// </value>
        [DataMember]
        public IEnumerable<double> Mz { get; set; }
        /// <summary>
        /// Gets or sets the intensities for all m/z-s.
        /// </summary>
        /// <value>
        /// The intensities.
        /// </value>
        [DataMember]
        public IEnumerable<double> Intensities { get; set; }
        /// <summary>
        /// Gets or sets the x coordinate.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [DataMember]
        public int X { get; set; }
        /// <summary>
        /// Gets or sets the y coordinate.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [DataMember]
        public int Y { get; set; }
    }
}