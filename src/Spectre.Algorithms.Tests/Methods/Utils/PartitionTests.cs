/*
 * PartitionTests.cs
 * Checks whether partition processing is proper.
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
using System;
using System.Linq;
using NUnit.Framework;
using Spectre.Algorithms.Methods.Utils;

namespace Spectre.Algorithms.Tests.Methods.Utils
{
    [TestFixture]
    public class PartitionTests
    {
        #region Simplify
        [Test]
        public void PartitionSimplificationFailsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => Partition.Simplify<int>(null),
                "ArgumentNullException not thrown.");
        }

        [Test]
        public void IntPartitionSimplifiesToNotNull()
        {
            var partition = new[] { 1, 2, 4, 3 };
            var simplified = Partition.Simplify(partition);
            Assert.NotNull(simplified, "Int sequence simplified to null.");
        }

        [Test]
        public void IntPartitionSimplificationSortsLabels()
        {
            var partition = new [] {1, 2, 4, 3};
            var simplified = Partition.Simplify(partition);
            Assert.True(simplified.SequenceEqual(new[] {1, 2, 3, 4}),
                "Int sequence simplified to: [{0}]", string.Join(", ", simplified));
        }

        [Test]
        public void StringPartitionSimplifiesToNotNull()
        {
            var partition = new[] { "Ala", "nie", "ma", "kota" };
            var simplified = Partition.Simplify(partition);
            Assert.NotNull(simplified, "Sequence sequence simplified to null.");
        }

        [Test]
        public void StringPartitionSimplifies()
        {
            var partition = new[] { "Ala", "nie", "ma", "kota" };
            var simplified = Partition.Simplify(partition);
            Assert.True(simplified.SequenceEqual(new[] { 1, 2, 3, 4 }),
                "Int sequence simplified to: [{0}]", string.Join(", ", simplified));
        }
        #endregion

        #region Compare
        private const double DefaultTolerance = 0.0;

        [Test]
        public void IntPartitionComparisonThrowExceptionOnNull()
        {
            var notNull = new[] {1};
            Assert.Throws<ArgumentNullException>(() => Partition.Compare<int, int>(null, null, DefaultTolerance));
            Assert.Throws<ArgumentNullException>(() => Partition.Compare<int, int>(notNull, null, DefaultTolerance));
            Assert.Throws<ArgumentNullException>(() => Partition.Compare<int, int>(null, notNull, DefaultTolerance));
        }

        [Test]
        public void IntPartitionComparisonThrowExceptionOnDifferentLengths()
        {
            var partition = new[] { 1, 2 };
            var another = new[] { 1, 1, 1 };
            Assert.Throws<ArgumentException>(() => Partition.Compare(partition, another, DefaultTolerance),
                "Does not throw on partition length mismatch.");
        }

        [Test]
        public void IntEqualPartitionComparisonWithoutTolerance()
        {
            var partition = new[] {1, 2};
            var result = Partition.Compare(partition, partition, 0);
            Assert.True(result, "The same instance found unequal.");
        }

        [Test]
        public void IntUnequalPartitionComparisonWithoutTolerance()
        {
            var partition = new[] { 1, 2 };
            var another = new[] {1, 1};
            var result = Partition.Compare(partition, another, 0);
            Assert.False(result, "Another partition found equal.");
        }

        [Test]
        public void IntEqualPartitionWithMixedLabelsAndNoTolerance()
        {
            var partition = new[] {1, 2, 2};
            var equalOne = new[] {2, 1, 1};
            var result = Partition.Compare(partition, equalOne, 0);
            Assert.True(result, "Equality is label-sensitive.");
        }

        [Test]
        public void MixedTypeEqualPartitionAndNoTolerance()
        {
            var partition = new[] { 1, 2, 3, 1 };
            var equalOne = new[] { "Ala", "nie ma", "kota", "Ala" };
            var result = Partition.Compare(partition, equalOne, 0);
            Assert.True(result, "Equality is type-sensitive.");
        }

        [Test]
        public void IntEqualPartitionWithTolerance()
        {
            var partition = new[] { 1, 2, 2, 1 };
            var equalOne = new[] { 1, 2, 2, 2};
            var result = Partition.Compare(partition, equalOne, 0.25);
            Assert.True(result, "Equality is tolerance insensitive.");
        }

        [Test]
        public void IntEqualPartitionWithMixedLabelsAndTolerance()
        {
            var partition = new[] { 2, 1, 1, 2 };
            var equalOne = new[] { 1, 2, 2, 2 };
            var result = Partition.Compare(partition, equalOne, 0.25);
            Assert.True(result, "Equality is tolerance insensitive or label sensitive.");
        }

        [Test]
        public void IntUnequalPartitionWithMixedLabelsAndTolerance()
        {
            var partition = new[] { 2, 1, 1, 2 };
            var equalOne = new[] { 1, 2, 2, 2 };
            var result = Partition.Compare(partition, equalOne, 0.2);
            Assert.False(result, "Inequality not captured.");
        }
        #endregion
    }
}
