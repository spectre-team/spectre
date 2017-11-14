/*
 * DivikResultSummary.cs
 * Summary of the DiviK algorithm analysis result.
 *
   Copyright 2017 Spectre Team

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
using System.Linq;
using Spectre.Algorithms.Methods.Utils;
using Spectre.Algorithms.Results;
using Spectre.Algorithms.StructureBoundAlgorithms;

namespace Spectre.Algorithms.ResultsProcessors
{
    /// <summary>
    /// Summary of DiviK analysis result.
    /// </summary>
    public class DivikResultSummary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivikResultSummary"/> class.
        /// </summary>
        /// <param name="tree">The result tree.</param>
        public DivikResultSummary(DivikResult tree)
        {
            Depth = tree.Depth();
            var counts = Partition.GetClusterSizes(tree.Merged).Values;
            NumberOfClusters = (uint)counts.Count;
            var numberOfObservations = tree.Merged.Length;
            ClusterSizeMean = (double)numberOfObservations / NumberOfClusters;
            ClusterSizeVariance = counts
                                      .Select(count => Math.Pow(count - ClusterSizeMean, 2))
                                      .Sum() / NumberOfClusters;
            SizeReduction = 1.0 - (double)NumberOfClusters / numberOfObservations;
        }

        /// <summary>
        /// Gets the depth of the resulting tree.
        /// </summary>
        /// <value>
        /// The depth.
        /// </value>
        public uint Depth { get; }

        /// <summary>
        /// Gets the cluster size mean.
        /// </summary>
        /// <value>
        /// The cluster size mean.
        /// </value>
        public double ClusterSizeMean { get; }

        /// <summary>
        /// Gets the cluster size variance.
        /// </summary>
        /// <value>
        /// The cluster size variance.
        /// </value>
        public double ClusterSizeVariance { get; }

        /// <summary>
        /// Gets the number of clusters.
        /// </summary>
        /// <value>
        /// The number of clusters.
        /// </value>
        public uint NumberOfClusters { get; }

        /// <summary>
        /// Gets the size reduction.
        /// </summary>
        /// <value>
        /// The size reduction, as a percent of input dataset size.
        /// </value>
        public double SizeReduction { get; }
    }
}
