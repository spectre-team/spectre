/*
 * SpectrumControllerTest.cs
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

using System.Linq;
using NUnit.Framework;
using Spectre.Controllers;
using Spectre.Models.Msi;

namespace Spectre.Tests.Controllers
{
    /// <summary>
    /// Tests of spectrum controller
    /// </summary>
    [TestFixture]
    internal class SpectrumControllerTest
    {
        private SpectrumController _controller;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _controller = new SpectrumController();
        }

        /// <summary>
        /// Tests the get first preparation sample spectrum.
        /// </summary>
        [Test]
        public void TestGetFirstPreparationSampleSpectrum()
        {
            var spectrum = _controller.Get(id: 1, spectrumId: 1);

            Assert.NotNull(spectrum);
            Assert.IsInstanceOf<Spectrum>(spectrum);
            Assert.IsNotEmpty(spectrum.Intensities);
            Assert.IsNotEmpty(spectrum.Mz);
            Assert.AreEqual(
                expected: spectrum.Mz.Count(),
                actual: spectrum.Intensities.Count(),
                message: "Intensities and Mz counts do not match.");
        }
    }
}
