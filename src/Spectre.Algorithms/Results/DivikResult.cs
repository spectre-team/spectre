﻿/*
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


using System.IO;
using System.Linq;
using MathWorks.MATLAB.NET.Arrays.native;
using Newtonsoft.Json;

namespace Spectre.Algorithms.Results
{
    /// <summary>
    /// Wraps DiviK algorithm results.
    /// </summary>
    public sealed class DivikResult
    {
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

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DivikResult"/> class.
        /// </summary>
        /// <param name="matlabResult">The results coming from MCR.</param>
        internal DivikResult(object matlabResult)
        {
            var array = (MWStructArray) matlabResult;
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
        /// Empty constructor for serialization purposes.
        /// </summary>
        private DivikResult()
        {

        }

        #endregion

        #region Parsing

        private double ParseIndex(MWStructArray array)
        {
            if (array.IsField("index"))
            {
                var tmpIndex = (double[,]) array.GetField("index");
                return tmpIndex[0, 0];
            }
            return double.NaN;
        }

        private double[,] ParseCentroids(MWStructArray array)
        {
            if (array.IsField("centroids"))
            {
                var transposed = (double[,]) array.GetField("centroids");
                var straight = new double[transposed.GetLength(1), transposed.GetLength(0)];
                for (var i = 0; i < transposed.GetLength(1); ++i)
                    for (var j = 0; j < transposed.GetLength(0); ++j)
                        straight[i, j] = transposed[j, i];
                return straight;
            }
            return null;
        }

        private int[] ParsePartition(MWStructArray array)
        {
            if (array.IsField("partition"))
            {
                var tmpPartition = (double[,]) array.GetField("partition");
                var partition = new int[tmpPartition.Length];
                for (var i = 0; i < tmpPartition.Length; ++i)
                {
                    // -1 for indexing purposes (label should allow to check centroid)
                    partition[i] = (int) tmpPartition[0, i] - 1;
                }
                return partition;
            }
            return null;
        }

        private double ParseAmplitudeThreshold(MWStructArray array)
        {
            if (array.IsField("amp_thr"))
            {
                var tmpAmpThr = (double[,]) array.GetField("amp_thr");
                return tmpAmpThr[0, 0];
            }
            return double.NaN;
        }

        private bool[] ParseAmplitudeFilter(MWStructArray array)
        {
            if (array.IsField("amp_filter"))
            {
                var tmpAmpFilter = (bool[,]) array.GetField("amp_filter");
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
            if (array.IsField("var_thr"))
            {
                var tmpVarThr = (double[,]) array.GetField("var_thr");
                return tmpVarThr[0, 0];
            }
            return double.NaN;
        }

        private bool[] ParseVarianceFilter(MWStructArray array)
        {
            if (array.IsField("var_filter"))
            {
                var tmpVarFilter = (bool[,]) array.GetField("var_filter");
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
            if (array.IsField("merged"))
            {
                var tmpMerged = (double[,]) array.GetField("merged");
                var merged = new int[tmpMerged.Length];
                for (var i = 0; i < tmpMerged.Length; ++i)
                {
                    merged[i] = (int) tmpMerged[0, i];
                }
                return merged;
            }
            return null;
        }

        private DivikResult[] ParseSubregions(MWStructArray array)
        {
            if (array.IsField("subregions"))
            {
                var tmpSubregions = (MWCellArray) array.GetField("subregions");
                var subregions = new DivikResult[tmpSubregions.NumberOfElements];
                for (var i = 0; i < tmpSubregions.NumberOfElements; ++i)
                {
                    if (tmpSubregions[i + 1] is MWStructArray)
                        subregions[i] = new DivikResult(tmpSubregions[i + 1]);
                }
                return subregions;
            }
            return null;
        }

        #endregion

        #region Save

        /// <summary>
        /// Saves result under the specified path in JSON format.
        /// </summary>
        /// <param name="path">The destination path.</param>
        /// <param name="identation">Sets idented formattings. Defaults to true.</param>
        public void Save(string path, bool identation = true)
        {
            Formatting format = identation ? Formatting.Indented : Formatting.None;
            string data = JsonConvert.SerializeObject(this, format);
            File.WriteAllText(path, data);
        }

        #endregion

        #region Equals

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is DivikResult))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var other = (DivikResult) obj;

            if ((double.IsNaN(other.AmplitudeThreshold) != double.IsNaN(this.AmplitudeThreshold)) || (!double.IsNaN(this.AmplitudeThreshold) && other.AmplitudeThreshold != this.AmplitudeThreshold))
                return false;
            if ((double.IsNaN(other.VarianceThreshold) != double.IsNaN(this.VarianceThreshold)) || (!double.IsNaN(this.VarianceThreshold) && other.VarianceThreshold != this.VarianceThreshold))
                return false;
            if (other.QualityIndex != this.QualityIndex)
                return false;
            if ((other.AmplitudeFilter != null) != (this.AmplitudeFilter != null))
                return false;
            if (other.AmplitudeFilter != null && this.AmplitudeFilter != null && other.AmplitudeFilter.Length != this.AmplitudeFilter.Length)
                return false;
            if ((other.VarianceFilter != null) != (this.VarianceFilter != null))
                return false;
            if (other.VarianceFilter != null && this.VarianceFilter != null && other.VarianceFilter.Length != this.VarianceFilter.Length)
                return false;
            if (other.Centroids.Length != this.Centroids.Length)
                return false;
            if (other.Partition.Length != this.Partition.Length)
                return false;
            if (other.AmplitudeFilter != null && this.AmplitudeFilter != null && !other.AmplitudeFilter.SequenceEqual(this.AmplitudeFilter))
                return false;
            if (other.VarianceFilter != null && this.VarianceFilter != null && !other.VarianceFilter.SequenceEqual(this.VarianceFilter))
                return false;
            if (!other.Partition.SequenceEqual(this.Partition))
                return false;
            if ((other.Merged != null) != (this.Merged != null))
                return false;
            if (other.Merged != null && this.Merged != null && !other.Merged.SequenceEqual(this.Merged))
                return false;
            if (!other.Centroids.Cast<double>().SequenceEqual(this.Centroids.Cast<double>()))
                return false;
            if ((other.Subregions != null) != (this.Subregions != null))
                return false;
            if (other.Subregions != null && this.Subregions != null && !other.Subregions.SequenceEqual(this.Subregions))
                return false;

            return true;
        }


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
            return ((object)first == null && (object)second == null) || ((object)first != null && first.Equals(second)) || second.Equals(first);
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
    }
}
