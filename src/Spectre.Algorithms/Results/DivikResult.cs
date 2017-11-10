/*
 * DivikResult.cs
 * Contains structure organizing all the output from DiviK algorithm.
 *
   Copyright 2017 Wilgierz Wojciech, Grzegorz Mrukwa, Michał Gallus

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
using System.IO;
using System.Linq;
using MathWorks.MATLAB.NET.Arrays.native;
using Newtonsoft.Json;
using Spectre.Algorithms.DataStructures;

namespace Spectre.Algorithms.Results
{
    /// <summary>
    /// Wraps DiviK algorithm results.
    /// </summary>
    public sealed class DivikResult : ITree
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DivikResult"/> class.
        /// </summary>
        /// <param name="matlabResult">The results coming from MCR.</param>
        public DivikResult(object matlabResult) // made public only for migration purposes! (was internal)
        {
            var array = (MWStructArray)matlabResult;
            Centroids = ParseCentroids(array);
            QualityIndex = ParseIndex(array);
            Partition = ParsePartition(array);
            AmplitudeThreshold = ParseAmplitudeThreshold(array);
            AmplitudeFilter = ParseAmplitudeFilter(array);
            VarianceThreshold = ParseVarianceThreshold(array);
            VarianceFilter = ParseVarianceFilter(array);
            Merged = ParseMerged(array);
            Subregions = ParseSubregions(array);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DivikResult"/> class.
        /// Empty constructor for serialization purposes.
        /// </summary>
        public DivikResult() // made public only for migration purposes! (was private)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Clustering quality index
        /// </summary>
        public double QualityIndex { get; set; }

        /// <summary>
        /// Centroids obtained in clustering
        /// </summary>
        public double[,] Centroids { get; set; }

        /// <summary>
        /// Partition of data
        /// </summary>
        public int[] Partition { get; set; }

        /// <summary>
        /// Amplitude threshold
        /// </summary>
        public double AmplitudeThreshold { get; set; }

        /// <summary>
        /// Amplitude filter
        /// </summary>
        public bool[] AmplitudeFilter { get; set; }

        /// <summary>
        /// Variance threshold
        /// </summary>
        public double VarianceThreshold { get; set; }

        /// <summary>
        /// Variance filter
        /// </summary>
        public bool[] VarianceFilter { get; set; }

        /// <summary>
        /// Downmerged partition
        /// </summary>
        public int[] Merged { get; set; }

        /// <summary>
        /// Result of further splits
        /// </summary>
        public DivikResult[] Subregions { get; set; }

        #endregion

        #region Implementation of ITree

        /// <inheritdoc />
        public IEnumerable<ITree> Children => Subregions;

        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">First instance to compare.</param>
        /// <param name="second">Second instance to compare.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(DivikResult first, DivikResult second)
        {
            return (((object)first == null) && ((object)second == null))
                   || (((object)first != null) && first.Equals(second))
                   || second.Equals(first);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="first">First instance to compare.</param>
        /// <param name="second">Second instance to compare.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(DivikResult first, DivikResult second)
        {
            return !(first == second);
        }

        #endregion

        #region Save

        /// <summary>
        /// Saves result under the specified path in JSON format.
        /// </summary>
        /// <param name="path">The destination path.</param>
        /// <param name="indentation">Sets indented formattings.</param>
        public void Save(string path, bool indentation)
        {
            var format = indentation ? Formatting.Indented : Formatting.None;
            var data = JsonConvert.SerializeObject(value: this, formatting: format);
            File.WriteAllText(path, data);
        }

        #endregion

        #region Equals

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is DivikResult))
            {
                return false;
            }

            if (object.ReferenceEquals(objA: this, objB: obj))
            {
                return true;
            }

            var other = (DivikResult)obj;

            if ((double.IsNaN(other.AmplitudeThreshold) != double.IsNaN(AmplitudeThreshold))
                || (!double.IsNaN(AmplitudeThreshold) && (other.AmplitudeThreshold != AmplitudeThreshold)))
            {
                return false;
            }
            if ((double.IsNaN(other.VarianceThreshold) != double.IsNaN(VarianceThreshold))
                || (!double.IsNaN(VarianceThreshold) && (other.VarianceThreshold != VarianceThreshold)))
            {
                return false;
            }
            if (other.QualityIndex != QualityIndex)
            {
                return false;
            }
            if ((other.AmplitudeFilter != null) != (AmplitudeFilter != null))
            {
                return false;
            }
            if ((other.AmplitudeFilter != null)
                && (AmplitudeFilter != null)
                && (other.AmplitudeFilter.Length != AmplitudeFilter.Length))
            {
                return false;
            }
            if ((other.VarianceFilter != null) != (VarianceFilter != null))
            {
                return false;
            }
            if ((other.VarianceFilter != null)
                && (VarianceFilter != null)
                && (other.VarianceFilter.Length != VarianceFilter.Length))
            {
                return false;
            }
            if (other.Centroids.Length != Centroids.Length)
            {
                return false;
            }
            if (other.Partition.Length != Partition.Length)
            {
                return false;
            }
            if ((other.AmplitudeFilter != null)
                && (AmplitudeFilter != null)
                && !other.AmplitudeFilter.SequenceEqual(AmplitudeFilter))
            {
                return false;
            }
            if ((other.VarianceFilter != null)
                && (VarianceFilter != null)
                && !other.VarianceFilter.SequenceEqual(VarianceFilter))
            {
                return false;
            }
            if (!other.Partition.SequenceEqual(Partition))
            {
                return false;
            }
            if ((other.Merged != null) != (Merged != null))
            {
                return false;
            }
            if ((other.Merged != null) && (Merged != null) && !other.Merged.SequenceEqual(Merged))
            {
                return false;
            }
            if (!other.Centroids.Cast<double>()
                .SequenceEqual(second: Centroids.Cast<double>()))
            {
                return false;
            }
            if ((other.Subregions != null) != (Subregions != null))
            {
                return false;
            }
            if ((other.Subregions != null) && (Subregions != null) && !other.Subregions.SequenceEqual(Subregions))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            var hash = 17;
            hash = 23 * hash + AmplitudeThreshold.GetHashCode();
            hash = 23 * hash + VarianceThreshold.GetHashCode();
            hash = 23 * hash + QualityIndex.GetHashCode();
            hash = 23 * hash + AmplitudeFilter.GetHashCode();
            hash = 23 * hash + VarianceFilter.GetHashCode();
            hash = 23 * hash + Centroids.GetHashCode();
            hash = 23 * hash + Partition.GetHashCode();
            hash = 23 * hash + Merged.GetHashCode();
            hash = 23 * hash + Subregions.GetHashCode();

            return hash;
        }

        #endregion

        #region Parsing

        private double ParseIndex(MWStructArray array)
        {
            if (array.IsField(fieldName: "index"))
            {
                var tmpIndex = (double[,])array.GetField(fieldName: "index");
                return tmpIndex[0, 0];
            }
            return double.NaN;
        }

        private double[,] ParseCentroids(MWStructArray array)
        {
            if (array.IsField(fieldName: "centroids"))
            {
                var transposed = (double[,])array.GetField(fieldName: "centroids");
                var straight = new double[transposed.GetLength(dimension: 1), transposed.GetLength(dimension: 0)];
                for (var i = 0; i < transposed.GetLength(dimension: 1); ++i)
                {
                    for (var j = 0; j < transposed.GetLength(dimension: 0); ++j)
                    {
                        straight[i, j] = transposed[j, i];
                    }
                }
                return straight;
            }
            return null;
        }

        private int[] ParsePartition(MWStructArray array)
        {
            if (array.IsField(fieldName: "partition"))
            {
                var tmpPartition = (double[,])array.GetField(fieldName: "partition");
                var partition = new int[tmpPartition.Length];
                for (var i = 0; i < tmpPartition.Length; ++i)
                {
                    // -1 for indexing purposes (label should allow to check centroid)
                    partition[i] = (int)tmpPartition[0, i] - 1;
                }
                return partition;
            }
            return null;
        }

        private double ParseAmplitudeThreshold(MWStructArray array)
        {
            if (array.IsField(fieldName: "amp_thr"))
            {
                var tmpAmpThr = (double[,])array.GetField(fieldName: "amp_thr");
                return tmpAmpThr[0, 0];
            }
            return double.NaN;
        }

        private bool[] ParseAmplitudeFilter(MWStructArray array)
        {
            if (array.IsField(fieldName: "amp_filter"))
            {
                var tmpAmpFilter = (bool[,])array.GetField(fieldName: "amp_filter");
                var ampFilter = new bool[tmpAmpFilter.Length];
                for (var i = 0; i < tmpAmpFilter.Length; ++i)
                {
                    ampFilter[i] = tmpAmpFilter[i, 0];
                }
                return ampFilter;
            }
            return null;
        }

        private double ParseVarianceThreshold(MWStructArray array)
        {
            if (array.IsField(fieldName: "var_thr"))
            {
                var tmpVarThr = (double[,])array.GetField(fieldName: "var_thr");
                return tmpVarThr[0, 0];
            }
            return double.NaN;
        }

        private bool[] ParseVarianceFilter(MWStructArray array)
        {
            if (array.IsField(fieldName: "var_filter"))
            {
                var tmpVarFilter = (bool[,])array.GetField(fieldName: "var_filter");
                var varFilter = new bool[tmpVarFilter.Length];
                for (var i = 0; i < tmpVarFilter.Length; ++i)
                {
                    varFilter[i] = tmpVarFilter[i, 0];
                }
                return varFilter;
            }
            return null;
        }

        private int[] ParseMerged(MWStructArray array)
        {
            if (array.IsField(fieldName: "merged"))
            {
                var tmpMerged = (double[,])array.GetField(fieldName: "merged");
                var merged = new int[tmpMerged.Length];
                for (var i = 0; i < tmpMerged.Length; ++i)
                {
                    merged[i] = (int)tmpMerged[0, i];
                }
                return merged;
            }
            return null;
        }

        private DivikResult[] ParseSubregions(MWStructArray array)
        {
            if (array.IsField(fieldName: "subregions"))
            {
                var tmpSubregions = (MWCellArray)array.GetField(fieldName: "subregions");
                var subregions = new DivikResult[tmpSubregions.NumberOfElements];
                for (var i = 0; i < tmpSubregions.NumberOfElements; ++i)
                {
                    if (tmpSubregions[i + 1] is MWStructArray)
                    {
                        subregions[i] = new DivikResult(matlabResult: tmpSubregions[i + 1]);
                    }
                }
                return subregions;
            }
            return null;
        }

        #endregion
    }
}
