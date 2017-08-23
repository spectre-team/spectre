/*
 * GmmFilteringTests.cs
 * Checks whether filtering finds thresholds.
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
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spectre.Algorithms.Methods;

namespace Spectre.Algorithms.Tests.Methods
{
    [TestFixture]
    [Category(name: "Algorithm")]
    public class GmmFilteringTests
    {
        private GmmFiltering _gmm;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _gmm = new GmmFiltering();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _gmm.Dispose();
            _gmm = null;
        }

        [Test]
        public void GmmBasedFilter()
        {
            var someNumbers = GmmFilteringTests.MakeRandomDoubles(lowerBound: -1, upperBound: 1)
                .Take(count: 300)
                .ToArray()
                .Concat(
                    second: GmmFilteringTests.MakeRandomDoubles(lowerBound: 10, upperBound: 15)
                        .Take(count: 500)
                        .ToArray()
                        .Concat(
                            second: GmmFilteringTests.MakeRandomDoubles(lowerBound: 3, upperBound: 4)
                                .Take(count: 300)
                                .ToArray()
                                .Concat(
                                    second: GmmFilteringTests.MakeRandomDoubles(lowerBound: 7, upperBound: 9)
                                        .Take(count: 800)
                                        .ToArray()
                                        .Concat(
                                            second: GmmFilteringTests
                                                .MakeRandomDoubles(lowerBound: 7.5, upperBound: 8.5)
                                                .Take(count: 400)
                                                .ToArray()))));

            var thresholds = _gmm.EstimateThresholds(someNumbers);

            Assert.Multiple(testDelegate: () =>
            {
                Assert.NotNull(thresholds);
                Assert.GreaterOrEqual(thresholds.Length, arg2: 1, message: "Found no thresholds.");
                Assert.True(condition: thresholds.Any(predicate: t => (t < 10) && (t > 1)),
                    message: "No threshold between 1 and 10.");
            });
        }

        private static IEnumerable<double> MakeRandomDoubles(double lowerBound, double upperBound, int seed = 0)
        {
            var rng = new Random(seed);
            while (true)
            {
                yield return rng.NextDouble() * (upperBound - lowerBound) + lowerBound;
            }
        }
    }
}
