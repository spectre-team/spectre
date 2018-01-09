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
using Spectre.Algorithms.Parameterization;
using Spectre.Controllers;
using Spectre.Models.Msi;

namespace Spectre.Tests.Controllers
{
    /// <summary>
    /// Tests for divik result controller
    /// </summary>
    [TestFixture]
    [Ignore("Not updated to new controllers")]
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
        /// Tests the first preparation sample divik getter.
        /// </summary>
        [Test]
        public void TestGetFirstPreparationSampleDivikResultIfExist()
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
            });
        }

        /// <summary>
        /// Tests equals in data in the first preparation sample divik result getter.
        /// </summary>
        public void TestGetFirstPreparationSampleDivikResultEqualsInData()
        {
            const int divikId = 1;
            const int level = 1;
            var heatmap = _controller.Get(id: 1, divikId: divikId, level: level);

            Assert.Multiple(testDelegate: () =>
            {
                Assert.AreEqual(
                    expected: heatmap.X.Count(),
                    actual: heatmap.Y.Count(),
                    message: "Number of coordinates of X and Y do not match.");
                Assert.AreEqual(
                    expected: heatmap.Data.Count(),
                    actual: heatmap.Y.Count(),
                    message: "Number of coordinates is different from number of intensities.");
            });
        }

        /// <summary>
        /// Tests the first preparation sample heatmap getter with bad request.
        /// </summary>
        [Test]
        public void TestGetResultWithBadRequest()
        {
            Assert.Multiple(testDelegate: () =>
            {
                Assert.Throws<ArgumentException>(
                    code: () => { _controller.Get(id: 1, divikId: -1, level: 1); },
                    message: "Accepted negative index");
                Assert.Throws<ArgumentException>(
                    code: () => { _controller.Get(id: 1, divikId: 1, level: -1); },
                    message: "Accepted negative index");
                Assert.IsNull(_controller.Get(id: 0, divikId: 1, level: 1));
            });
        }

        /// <summary>
        /// Tests the divik config getter.
        /// </summary>
        [Test]
        public void TestIfExistGetDivikResultConfig()
        {
            const int divikId = 1;
            var config = _controller.GetConfig(id: 1, divikId: divikId);

            Assert.Multiple(testDelegate: () =>
            {
                Assert.NotNull(config);
                Assert.IsInstanceOf<DivikOptions>(config);
            });
        }
    }
}
