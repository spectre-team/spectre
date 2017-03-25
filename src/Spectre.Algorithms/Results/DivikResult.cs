/*
 * DivikResult.cs
 * Contains structure organizing all the output from DiviK algorithm.
 * 
   Copyright 2017 Wilgierz Wojciech, Grzegorz Mrukwa

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
using MathWorks.MATLAB.NET.Arrays.native;

namespace Spectre.Algorithms.Results
{
	/// <summary>
	/// Wraps DiviK algorithm results.
	/// </summary>
	public sealed class DivikResult
    {
        /// <summary>
        /// clustering quality index
        /// </summary>
        private readonly double _index;

        /// <summary>
        /// centroids obtained in clustering
        /// </summary>
        private readonly double[,] _centroid;

        /// <summary>
        /// partition of data
        /// </summary>
        private readonly int[] _partition;

        /// <summary>
        /// amplitude threshold
        /// </summary>
        private readonly double _amplitudeThreshold;

        /// <summary>
        /// amplitude filter
        /// </summary>
        private readonly bool[] _amplitudeFilter;

        /// <summary>
        /// variance threshold
        /// </summary>
        private readonly double _varianceThreshold;

        /// <summary>
        /// variance filter
        /// </summary>
        private readonly bool[] _varianceFilter;

        /// <summary>
        /// downmerged partition
        /// </summary>
        private readonly int[] _merged;

        /// <summary>
        /// result of further splits
        /// </summary>
        private readonly DivikResult[] _subregions;

        /// <summary>
        /// Initializes a new instance of the <see cref="DivikResult"/> class.
        /// </summary>
        /// <param name="matlabResult">The results coming from MCR.</param>
        internal DivikResult(object matlabResult)
        {
            MWStructArray array = (MWStructArray) matlabResult;
            if (array.IsField("centroids"))
                _centroid = (double[,]) array.GetField("centroids");

            if (array.IsField("partition"))
            {
                Console.WriteLine("partition");
                double[,] tmpPartition = (double[,]) array.GetField("partition");
                _partition = new int[tmpPartition.Length];
                for (var i = 0; i < tmpPartition.Length; ++i)
                {
                    _partition[i] = (int) tmpPartition[0, i];
                }
            }

            if (array.IsField("index"))
            {
                Console.WriteLine("index");
                double[,] tmpIndex = (double[,]) array.GetField("index");
                _index = tmpIndex[0, 0];
            }

            if (array.IsField("amp_thr"))
            {
                Console.WriteLine("amp_thr");
                double[,] tmpAmpThr = (double[,]) array.GetField("amp_thr");
                _amplitudeThreshold = tmpAmpThr[0, 0];
            }

            if (array.IsField("amp_filter"))
            {
                Console.WriteLine("amp_filter");
                double[,] tmpAmpFilter = (double[,]) array.GetField("amp_filter");
                _amplitudeFilter = new bool[tmpAmpFilter.Length];
                for (var i = 0; i < tmpAmpFilter.Length; ++i)
                {
                    _amplitudeFilter[i] = Convert.ToBoolean(tmpAmpFilter[0, i]);
                    Console.WriteLine(_amplitudeFilter[i]);
                }
            }

            if (array.IsField("var_thr"))
            {
                Console.WriteLine("var_thr");
                double[,] tmpVarThr = (double[,]) array.GetField("var_thr");
                _varianceThreshold = tmpVarThr[0, 0];
                Console.WriteLine(_varianceThreshold);
            }

            if (array.IsField("var_filter"))
            {
                Console.WriteLine("var_filter");
                double[,] tmpVarFilter = (double[,]) array.GetField("var_filter");
                _varianceFilter = new bool[tmpVarFilter.Length];
                for (var i = 0; i < tmpVarFilter.Length; ++i)
                {
                    _varianceFilter[i] = Convert.ToBoolean(tmpVarFilter[0, i]);
                    Console.WriteLine(_varianceFilter[i]);
                }
            }

            if (array.IsField("merged"))
            {
                Console.WriteLine("merged");
                double[,] tmpMerged = (double[,]) array.GetField("merged");
                _merged = new int[tmpMerged.Length];
                for (var i = 0; i < tmpMerged.Length; ++i)
                {
                    _merged[i] = (int) tmpMerged[0, i];
                }
            }

            if (array.IsField("subregions"))
            {
                Console.WriteLine("subregions");
                MWCellArray tmpSubregions = (MWCellArray)array.GetField("subregions");
                //subregions = new DivikResult[tmpSubregions.Length];
                //for (var i = 0; i < tmpSubregions.Length; ++i)
                //{
                //    subregions[i] = new DivikResult(tmpSubregions[i]);
                //}
                Console.WriteLine(tmpSubregions);
            }
        }

        /// <summary>
        /// Saves result under the specified path in JSON format.
        /// </summary>
        /// <param name="path">The destination path.</param>
        /// <exception cref="System.NotImplementedException">Always, as result saving has not been implemented yet.</exception>
        public void Save(string path)
        {
            throw new NotImplementedException("Result saving has not been implemented yet.");
        }
    }
}
