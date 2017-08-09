/*
 * DivikResultControllerTest.cs
 * Test for proper responses after requests.
 *
   Copyright 2017 Grzegorz Mrukwa, Michał Gallus, Daniel Babiak, Sebastian Pustelnik

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

namespace Spectre.Tests.Controllers
{
    /// <summary>
    /// Tests for heatmap controller
    /// </summary>
    [TestFixture]
    internal class DivikResultControllerTest
    {
        private DivikResultController _controller;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _controller = new DivikResultController();
        }

        /// <summary>
        /// Tests the first preparation sample heatmap getter.
        /// </summary>
        [Test]
        public void TestGetFirstPreparationSampleHeatmap()
        {
            const int divikId = 1;
            const int level = 1;
            var heatmap = _controller.Get(id: 1, divikId: divikId, level: level);

            Assert.Multiple(testDelegate: () =>
            {
                Assert.NotNull(heatmap);
                Assert.IsInstanceOf<DivikResult>(heatmap);
                Assert.IsNotEmpty(heatmap.Data);
                Assert.IsNotEmpty(heatmap.X);
                Assert.IsNotEmpty(heatmap.Y);
                Assert.AreEqual(
                    expected: heatmap.X.Count(),
                    actual: heatmap.Y.Count(),
                    message: "Number of coordinates of X and Y do not match.");
                Assert.AreEqual(
                    expected: heatmap.Data.Count(),
                    actual: heatmap.Y.Count(),
                    message: "Number of coordinates is different from number of intensities.");

                Assert.Throws<ArgumentException>(
                    code: () => { _controller.Get(id: 1, divikId: -1, level: 1); },
                    message: "Accepted negative index");
                Assert.Throws<ArgumentException>(
                    code: () => { _controller.Get(id: 1, divikId: 1, level: -1); },
                    message: "Accepted negative index");
            });
        }
    }
}
