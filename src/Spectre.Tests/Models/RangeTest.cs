/*
 * RangeTest.cs
 * Tests for Range model.
 *
   Copyright 2017 Maciej Gamrat

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
using Spectre.Controllers;
using Spectre.Models.Msi;

namespace Spectre.Tests.Models
{
    /// <summary>
    /// Tests for Range model.
    /// </summary>
    [TestFixture]
    public class RangeTest
    {
        /// <summary>
        /// Tests getters.
        /// </summary>
        [Test]
        public void TestRangeGetters()
        {
            var range = new Range(3, 5);
            Assert.AreEqual(range.Min, 3);
            Assert.AreEqual(range.Max, 5);
        }

        /// <summary>
        /// Tests getters.
        /// </summary>
        [Test]
        public void TestRangeSetters()
        {
            var range = new Range(0, 0);
            Assert.AreEqual(range.Min, 0);
            Assert.AreEqual(range.Max, 0);

            range.Min = 3;
            range.Max = 5;
            Assert.AreEqual(range.Min, 3);
            Assert.AreEqual(range.Max, 5);
        }
    }
}
