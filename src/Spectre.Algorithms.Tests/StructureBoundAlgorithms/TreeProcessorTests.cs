/*
 * TreeProcessorTests.cs
 * Tests tree processing in general.
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

using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Spectre.Algorithms.DataStructures;
using Spectre.Algorithms.StructureBoundAlgorithms;

namespace Spectre.Algorithms.Tests.StructureBoundAlgorithms
{
    [TestFixture]
    public class TreeProcessorTests
    {
        #region DummyTree

        private class DummyTree : ITree
        {
            public IEnumerable<ITree> Children { get; }

            public DummyTree(IEnumerable<DummyTree> children)
            {
                Children = children;
            }
        }
        #endregion

        #region Test conditions
        private readonly DummyTree _sampleUnbalancedTree = new DummyTree(new[]
        {
            new DummyTree(new DummyTree[]{}),
            new DummyTree(new []
            {
                new DummyTree(new DummyTree[]{}),
                null,
                new DummyTree(null),
                new DummyTree(new DummyTree[]{}),
            })
        });
        private readonly uint _expectedDepth = 3u;
        private readonly uint _expectedNumberOfLeaves = 6u;
        #endregion

        #region Fold
        [Test]
        public void FoldProcessesEachNode()
        {
            uint visits = 0u;
            _sampleUnbalancedTree.Fold(
                (subtree, subresults) =>
                {
                    subresults?.ToArray();
                    return ++visits;
                }, 0u);
            Assert.AreEqual(_expectedNumberOfLeaves, visits);
        }

        [Test]
        public void FoldProcessesChildrenFirst()
        {
            uint visits = 0u;
            var resultForRoot = _sampleUnbalancedTree.Fold(
                (subtree, subresults) =>
                {
                    subresults?.ToArray();
                    return ++visits;
                }, 0u);
            Assert.AreEqual(_expectedNumberOfLeaves, resultForRoot);
        }

        [Test]
        public void FoldDefersExecution()
        {
            uint visits = 0u;
            var resultForRoot = _sampleUnbalancedTree.Fold(
                (subtree, subresults) => ++visits, 0u);
            // @gmrukwa: Only top node is processed - leaves are not necessary for calculations.
            Assert.AreEqual(1u, resultForRoot);
        }

        [Test]
        public void FoldPassesCurrentNodeAsFirstInputOfTheFoldingFunction()
        {
            _sampleUnbalancedTree.Fold(
                (subtree, subresults) =>
                {
                    Assert.AreSame(_sampleUnbalancedTree, subtree);
                    return 0u;
                },
                0u);
        }

        [Test]
        public void FoldPassesSubresultsAsSecondInputOfTheFoldingFunction()
        {
            _sampleUnbalancedTree.Fold(
                (subtree, subresults) =>
                {
                    if (object.ReferenceEquals(subtree, _sampleUnbalancedTree))
                    {
                        Assert.AreEqual(_sampleUnbalancedTree.Children.Count(), subresults.Count());
                    }
                    return 0u;
                },
                0u);
        }

        [Test]
        public void FoldReturnsInitialValueForNulls()
        {
            _sampleUnbalancedTree.Fold(
                (subtree, subresults) =>
                {
                    Assert.AreEqual(subtree?.Children?.Count(child => child == null) ?? 0,
                                    subresults?.Sum() ?? 0);
                    return 0;
                },
                initialValue: 1);
        }
        #endregion

        #region Depth

        [Test]
        public void DepthOfSingleNodeIsOne()
        {
            var depth = new DummyTree(new DummyTree[]{}).Depth();
            Assert.AreEqual(1u, depth);
        }

        [Test]
        public void DepthOfSeveralLevelsCalculatesProperly()
        {
            var depth = new DummyTree(new []{ new DummyTree(new DummyTree[]{}) }).Depth();
            Assert.AreEqual(2u, depth);
        }

        [Test]
        public void DepthIsTheMaximalOne()
        {
            var depth = _sampleUnbalancedTree.Depth();
            Assert.AreEqual(_expectedDepth, depth);
        }
        #endregion
    }
}
