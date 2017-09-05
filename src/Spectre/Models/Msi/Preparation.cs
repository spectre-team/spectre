/*
 * Preparation.cs
 * Class representing single sample in MALDI MSI.
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

using System.Runtime.Serialization;

namespace Spectre.Models.Msi
{
    /// <summary>
    /// Exhibits general preparation data to API.
    /// </summary>
    [DataContract]
    public class Preparation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Preparation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="spectraNumber">Number of all spectra in preparation.</param>
        /// <param name="xRange">Pair of minimum &amp; maximum coordinate along X axis.</param>
        /// <param name="yRange">Pair of minimum &amp; maximum coordinate along Y axis.</param>
        public Preparation(string name, int id, int spectraNumber, Range xRange, Range yRange)
        {
            Name = name;
            Id = id;
            SpectraNumber = spectraNumber;
            XRange = xRange;
            YRange = yRange;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember]
        public int Id { get; private set; }

        /// <summary>
        /// Gets the number of spectra.
        /// </summary>
        /// <value>
        /// Number of spectra.
        /// </value>
        [DataMember]
        public int SpectraNumber { get; private set; }

        /// <summary>
        /// Specifies the inclusive range of spectrum coordinates along X axis.
        /// </summary>
        /// <value>
        /// Tuple containing minimal &amp; maximal coordinate values.
        /// </value>
        [DataMember]
        public Range XRange { get; private set; }

        /// <summary>
        /// Specifies the inclusive range of spectrum coordinates along Y axis.
        /// </summary>
        /// <value>
        /// Tuple containing minimal &amp; maximal coordinate values.
        /// </value>
        [DataMember]
        public Range YRange { get; private set; }

        /// <summary>
        /// Gets the number of chanelIds.
        /// </summary>
        /// <value>
        /// Number of chanelIds.
        /// </value>
        [DataMember]
        public int ChanelIdAvailableNumber { get; private set; }
    }
}
