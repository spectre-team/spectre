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
        /// index from matlabResult[1]
        /// </summary>
        private double index;

        /// <summary>
        /// centoid from matlabResult[1]
        /// </summary>
        private double[,] centoid;

        /// <summary>
        /// partition from matlabResult[1]
        /// </summary>
        private int[] partition;

        /// <summary>
        /// amp_thr from matlabResult[1]
        /// </summary>
        private double _ampThr;

        /// <summary>
        /// amp_filter from matlabResult[1]
        /// </summary>
        private bool[] _ampFilter;

        /// <summary>
        /// var_thr from matlabResult[1]
        /// </summary>
        private double _varThr;

        /// <summary>
        /// var_filter from matlabResult[1]
        /// </summary>
        private bool[] _varFilter;

        /// <summary>
        /// merged from matlabResult[1]
        /// </summary>
        private int[] merged;

        /// <summary>
        /// subregions from matlabResult[1]
        /// </summary>
        private DivikResult[] subregions;

        /// <summary>
        /// Initializes a new instance of the <see cref="DivikResult"/> class.
        /// </summary>
        /// <param name="matlabResult">The results coming from MCR.</param>
        internal DivikResult(object matlabResult)
        {
            MWStructArray array = (MWStructArray) matlabResult;
            if (array.IsField("centroids"))
                centoid = (double[,]) array.GetField("centroids");

            if (array.IsField("partition"))
            {
                Console.WriteLine("partition");
                double[,] tmpPartition = (double[,]) array.GetField("partition");
                partition = new int[tmpPartition.Length];
                for (var i = 0; i < tmpPartition.Length; ++i)
                {
                    partition[i] = (int) tmpPartition[0, i];
                    Console.WriteLine(partition[i]);
                }
            }

            if (array.IsField("index"))
            {
                Console.WriteLine("index");
                double[,] tmpIndex = (double[,]) array.GetField("index");
                index = tmpIndex[0, 0];
                Console.WriteLine(index);
            }

            if (array.IsField("amp_thr"))
            {
                Console.WriteLine("amp_thr");
                double[,] tmpAmpThr = (double[,]) array.GetField("amp_thr");
                _ampThr = tmpAmpThr[0, 0];
                Console.WriteLine(_ampThr);
            }

            if (array.IsField("amp_filter"))
            {
                Console.WriteLine("amp_filter");
                double[,] tmpAmpFilter = (double[,]) array.GetField("amp_filter");
                _ampFilter = new bool[tmpAmpFilter.Length];
                for (var i = 0; i < tmpAmpFilter.Length; ++i)
                {
                    _ampFilter[i] = Convert.ToBoolean(tmpAmpFilter[0, i]);
                    Console.WriteLine(_ampFilter[i]);
                }
            }

            if (array.IsField("var_thr"))
            {
                Console.WriteLine("var_thr");
                double[,] tmpVarThr = (double[,]) array.GetField("var_thr");
                _varThr = tmpVarThr[0, 0];
                Console.WriteLine(_varThr);
            }

            if (array.IsField("var_filter"))
            {
                Console.WriteLine("var_filter");
                double[,] tmpVarFilter = (double[,]) array.GetField("var_filter");
                _varFilter = new bool[tmpVarFilter.Length];
                for (var i = 0; i < tmpVarFilter.Length; ++i)
                {
                    _varFilter[i] = Convert.ToBoolean(tmpVarFilter[0, i]);
                    Console.WriteLine(_varFilter[i]);
                }
            }

            if (array.IsField("merged"))
            {
                Console.WriteLine("merged");
                double[,] tmpMerged = (double[,]) array.GetField("merged");
                merged = new int[tmpMerged.Length];
                for (var i = 0; i < tmpMerged.Length; ++i)
                {
                    merged[i] = (int) tmpMerged[0, i];
                    Console.WriteLine(merged[i]);
                }
            }

            if (array.IsField("subregions"))
            {
                object[,] tmpSubregions = (object[,]) array.GetField("subregions");
                subregions = new DivikResult[tmpSubregions.Length];
                for (var i = 0; i < tmpSubregions.Length; ++i)
                {
                    subregions[i] = new DivikResult(tmpSubregions[0, i]);
                }
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
