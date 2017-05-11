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
    [TestFixture, Category("Algorithm")]
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
            var someNumbers = MakeRandomDoubles(-1, 1).Take(300).ToArray().Concat(
                MakeRandomDoubles(10, 15).Take(500).ToArray().Concat(
                MakeRandomDoubles(3, 4).Take(300).ToArray().Concat(
                MakeRandomDoubles(7, 9).Take(800).ToArray().Concat(
                MakeRandomDoubles(7.5, 8.5).Take(400).ToArray()))));

            var thresholds = _gmm.EstimateThresholds(someNumbers);

            Assert.Multiple(() =>
            {
                Assert.NotNull(thresholds);
                Assert.GreaterOrEqual(thresholds.Length, 1, "Found no thresholds.");
                Assert.True(thresholds.Any(t => t < 10 && t > 1),
                    "No threshold between 1 and 10.");
            });
        }

        private static IEnumerable<double> MakeRandomDoubles(double lowerBound, double upperBound, int seed=0)
        {
            var rng = new Random(seed);
            while (true)
            {
                yield return rng.NextDouble()*(upperBound - lowerBound) + lowerBound;
            }
        }
    }
}
