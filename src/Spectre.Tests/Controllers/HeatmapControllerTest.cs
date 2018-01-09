/*
 * HeatmapControllerTest.cs
 * Test for proper responses after requests.
 *
   Copyright 2017 Grzegorz Mrukwa, Michał Gallus, Daniel Babiak

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
using System.Diagnostics;
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
    [Ignore("Not updated to new controllers")]
    internal class HeatmapControllerTest
    {
        private HeatmapController _controller;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _controller = new HeatmapController();
        }

        /// <summary>
        /// Tests the first preparation sample heatmap getter.
        /// </summary>
        [Test]
        public void TestGetFirstPreparationSampleHeatmap()
        {
            const int anyChannelId = 1;
            var heatmap = _controller.Get(id: 1, channelId: anyChannelId, flag: false);

            Assert.Multiple(testDelegate: () =>
            {
                Assert.NotNull(heatmap);
                Assert.IsInstanceOf<Heatmap>(heatmap);
                Assert.IsNotEmpty(heatmap.Intensities);
                Assert.IsNotEmpty(heatmap.X);
                Assert.IsNotEmpty(heatmap.Y);
                Assert.AreEqual(
                    expected: heatmap.X.Count(),
                    actual: heatmap.Y.Count(),
                    message: "Number of coordinates of X and Y do not match.");
                Assert.AreEqual(
                    expected: heatmap.Intensities.Count(),
                    actual: heatmap.Y.Count(),
                    message: "Number of coordinates is different from number of intensities.");

                Assert.Throws<ArgumentException>(
                    code: () => { _controller.Get(id: 1, channelId: -1, flag: false); },
                    message: "Accepted negative index");
            });
        }
    }
}
