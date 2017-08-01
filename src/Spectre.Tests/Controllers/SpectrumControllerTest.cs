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

using System;
using System.Linq;
using NUnit.Framework;
using Spectre.Controllers;
using Spectre.Models.Msi;

namespace Spectre.Tests.Controllers
{
    [TestFixture]
    class SpectrumControllerTest
    {
        private SpectrumController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new SpectrumController();
        }

        [Test]
        public void TestGetFirstPreparationSampleSpectrum()
        {
            var spectrum = _controller.Get(1, 1);

            Assert.NotNull(spectrum);
            Assert.IsInstanceOf<Spectrum>(spectrum);
            Assert.IsNotEmpty(spectrum.Intensities);
            Assert.IsNotEmpty(spectrum.Mz);
            Assert.AreEqual(spectrum.Mz.Count(), spectrum.Intensities.Count(), "Intensities and Mz counts do not match.");
        }
    }
}
