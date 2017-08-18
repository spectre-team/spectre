/*
 * DivikConfig.cs
 * Model class with divik config.
 *
   Copyright 2017 Grzegorz Mrukwa, Michał Gallus, Daniel Babiak

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

namespace Spectre.Models.Msi
{
    /// <summary>
    /// Provides divik config
    /// </summary>
    public class DivikConfig
    {
        /// <summary>
        /// Max k
        /// </summary>
        [DataMember]
        public int MaxK { get; set; }

        /// <summary>
        /// Level
        /// </summary>
        [DataMember]
        public int Level { get; set; }

        /// <summary>
        /// Using levels
        /// </summary>
        [DataMember]
        public bool UsingLevels { get; set; }

        /// <summary>
        /// Amplitude
        /// </summary>
        [DataMember]
        public bool Amplitude { get; set; }

        /// <summary>
        /// Variance
        /// </summary>
        [DataMember]
        public bool Variance { get; set; }

        /// <summary>
        /// Percent size limit
        /// </summary>
        [DataMember]
        public double PercentSizeLimit { get; set; }

        /// <summary>
        /// Feature preservation limit
        /// </summary>
        [DataMember]
        public double FeaturePreservationLimit { get; set; }

        /// <summary>
        /// Metric
        /// </summary>
        [DataMember]
        public string Metric { get; set; }

        /// <summary>
        /// Plotting partitions
        /// </summary>
        [DataMember]
        public bool PlottingPartitions { get; set; }

        /// <summary>
        /// Plotting partitions recursively
        /// </summary>
        [DataMember]
        public bool PlottingPartitionsRecursively { get; set; }

        /// <summary>
        /// Plotting decomposition
        /// </summary>
        [DataMember]
        public bool PlottingDecomposition { get; set; }

        /// <summary>
        /// Plotting decomposition recursively
        /// </summary>
        [DataMember]
        public bool PlottingDecompositionRecursively { get; set; }

        /// <summary>
        /// Max decomposition components
        /// </summary>
        [DataMember]
        public int MaxDecompositionComponents { get; set; }
    }
}