/*
 * DivikResultSummaryTests.cs
 * Tests whether summary is built properly.
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

using NUnit.Framework;
using Spectre.Algorithms.Results;
using Spectre.Algorithms.ResultsProcessors;

namespace Spectre.Algorithms.Tests.ResultsProcessor
{
    [TestFixture]
    public class DivikResultSummaryTests
    {
        #region Sample result
        private readonly DivikResult _sampleResult = new DivikResult()
        {
            Partition = new []{1,1,1,2,2,2},
            Merged = new []{1,1,2,3,3,3},
            Subregions = new []
            {
                new DivikResult()
                {
                    Partition = new []{1,1,2},
                    Merged = new []{1,1,2}
                }, 
                null
            }
        };
        #endregion

        [Test]
        public void InitializesWithoutException()
        {
            Assert.DoesNotThrow(() => new DivikResultSummary(_sampleResult));
        }

        [Test]
        public void RecognizesProperDepth()
        {
            var summary = new DivikResultSummary(_sampleResult);
            Assert.AreEqual(2, summary.Depth);
        }

        [Test]
        public void CountsDistinctClusters()
        {
            var summary = new DivikResultSummary(_sampleResult);
            Assert.AreEqual(3, summary.NumberOfClusters);
        }

        [Test]
        public void EstimatesMeanClusterSize()
        {
            var summary = new DivikResultSummary(_sampleResult);
            Assert.AreEqual(2.0, summary.ClusterSizeMean, 1e-6);
        }

        [Test]
        public void EstimatesClusterSizeVariance()
        {
            var summary = new DivikResultSummary(_sampleResult);
            Assert.AreEqual(2.0/3.0, summary.ClusterSizeVariance, 1e-6);
        }

        [Test]
        public void FindsSizeReduction()
        {
            var summary = new DivikResultSummary(_sampleResult);
            Assert.AreEqual(0.5, summary.SizeReduction, 1e-6);
        }
    }
}
