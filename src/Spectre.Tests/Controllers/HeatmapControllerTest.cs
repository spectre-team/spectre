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
using System.Linq;
using NUnit.Framework;
using Spectre.Controllers;
using Spectre.Models.Msi;

namespace Spectre.Tests.Controllers
{
    [TestFixture]
    class HeatmapControllerTest
    {
        private HeatmapController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new HeatmapController();
        }

        [Test]
        public void TestGetFirstPreparationSampleHeatmap()
        {
            const int anyChannelId = 1;
            var heatmap = _controller.Get(1, anyChannelId, false);

            Assert.Multiple(() =>
            {
                Assert.NotNull(heatmap);
                Assert.IsInstanceOf<Heatmap>(heatmap);
                Assert.IsNotEmpty(heatmap.Intensities);
                Assert.IsNotEmpty(heatmap.X);
                Assert.IsNotEmpty(heatmap.Y);
                Assert.AreEqual(heatmap.X.Count(), heatmap.Y.Count(), "Number of coordinates of X and Y do not match.");
                Assert.AreEqual(heatmap.Intensities.Count(), heatmap.Y.Count(),
                    "Number of coordinates is different from number of intensities.");

                Assert.Throws<ArgumentException>(() => { _controller.Get(1, -1, false); }, "Accepted negative index");
            });
        }
    }
}
