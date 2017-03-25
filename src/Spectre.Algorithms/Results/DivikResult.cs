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
            setCentoids(array);
            setIndex(array);
            setPartition(array);
            setAmpThr(array);
            setAmpFilter(array);
            setVarThr(array);
            setVarFilter(array);
            setMerged(array);
            setSubregions(array);
        }

        internal void setIndex(MWStructArray array)
        {
            if (array.IsField("index"))
            {
                double[,] tmpIndex = (double[,]) array.GetField("index");
                index = tmpIndex[0, 0];
            }
            else index = Double.NaN;
        }

        internal void setCentoids(MWStructArray array)
        {
            if (array.IsField("centroids"))
                centoid = (double[,]) array.GetField("centroids");
            else centoid = null;
        }

        internal void setPartition(MWStructArray array)
        {
            if (array.IsField("partition"))
            {
                double[,] tmpPartition = (double[,]) array.GetField("partition");
                partition = new int[tmpPartition.Length];
                for (var i = 0; i < tmpPartition.Length; ++i)
                {
                    partition[i] = (int) tmpPartition[0, i];
                }
            }
            else partition = null;
        }

        internal void setAmpThr(MWStructArray array)
        {
            if (array.IsField("amp_thr"))
            {
                double[,] tmpAmpThr = (double[,])array.GetField("amp_thr");
                _ampThr = tmpAmpThr[0, 0];
            }
            _ampThr = double.NaN;
        }

        internal void setAmpFilter(MWStructArray array)
        {
            if (array.IsField("amp_filter"))
            {
                double[,] tmpAmpFilter = (double[,])array.GetField("amp_filter");
                _ampFilter = new bool[tmpAmpFilter.Length];
                for (var i = 0; i < tmpAmpFilter.Length; ++i)
                {
                    _ampFilter[i] = Convert.ToBoolean(tmpAmpFilter[0, i]);
                }
            }
            _ampFilter = null;
        }

        internal void setVarThr(MWStructArray array)
        {
            if (array.IsField("var_thr"))
            {
                double[,] tmpVarThr = (double[,])array.GetField("var_thr");
                _varThr = tmpVarThr[0, 0];
            }
            _varThr = double.NaN;
        }

        internal void setVarFilter(MWStructArray array)
        {
            if (array.IsField("var_filter"))
            {
                double[,] tmpVarFilter = (double[,])array.GetField("var_filter");
                _varFilter = new bool[tmpVarFilter.Length];
                for (var i = 0; i < tmpVarFilter.Length; ++i)
                {
                    _varFilter[i] = Convert.ToBoolean(tmpVarFilter[0, i]);
                }
            }
            _varFilter = null;
        }

        internal void setMerged(MWStructArray array)
        {
            if (array.IsField("merged"))
            {
                double[,] tmpMerged = (double[,])array.GetField("merged");
                merged = new int[tmpMerged.Length];
                for (var i = 0; i < tmpMerged.Length; ++i)
                {
                    merged[i] = (int)tmpMerged[0, i];
                }
            }
            merged = null;
        }

        internal void setSubregions(MWStructArray array)
        {
            if (array.IsField("subregions"))
            {
                MWCellArray tmpSubregions = (MWCellArray)array.GetField("subregions");
                subregions = new DivikResult[tmpSubregions.NumberOfElements];
                for (var i = 0; i < tmpSubregions.NumberOfElements; ++i)
                {
                    if (tmpSubregions[i + 1].GetType() == typeof(MWStructArray))
                        subregions[i] = new DivikResult(tmpSubregions[i + 1]);
                }
            }
            subregions = null;
        }

        public double getIndex()
        {
            return index;
        }

        public int[] getPartition()
        {
            return partition;
        }

        public double[,] getCentoids()
        {
            return centoid;
        }

        public double getAmpThr()
        {
            return _ampThr;
        }

        public bool[] getAmpFilter()
        {
            return _ampFilter;
        }

        public double getVarThr()
        {
            return _varThr;
        }

        public bool[] getVarFilter()
        {
            return _varFilter;
        }

        public int[] getMerged()
        {
            return merged;
        }

        public DivikResult[] getSubregions()
        {
            return subregions;
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
