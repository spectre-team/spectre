/*
 * Metric.cs
 * Metrics used in DiviK
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

namespace Spectre.Algorithms.Parameterization
{
    /// <summary>
    /// Contains metrics supported in DiviK.
    /// </summary>
    public enum Metric
    {
        /// <summary>
        /// The euclidean
        /// </summary>
        Euclidean,

        /// <summary>
        /// The pearson
        /// </summary>
        Pearson,

        /// <summary>
        /// The spearman
        /// </summary>
        Spearman,

        /// <summary>
        /// The cityblock
        /// </summary>
        Cityblock,

        /// <summary>
        /// The minkowski
        /// </summary>
        Minkowski,

        /// <summary>
        /// The chebychev
        /// </summary>
        Chebychev,

        /// <summary>
        /// The mahalanobis
        /// </summary>
        Mahalanobis,

        /// <summary>
        /// The cosine
        /// </summary>
        Cosine,

        /// <summary>
        /// The hamming
        /// </summary>
        Hamming,

        /// <summary>
        /// The jaccard
        /// </summary>
        Jaccard
    }
}
