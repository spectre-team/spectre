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

using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using NUnit.Framework;
using Spectre.Controllers;
using Spectre.Data.Datasets;
using Spectre.Models.Msi;

namespace Spectre.Tests.Controllers
{
    /// <summary>
    /// Tests of spectrum controller
    /// </summary>
    [TestFixture]
    internal class SpectrumControllerTest
    {
        private const double Delta = 0.001;

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
        /// Tests getting non-existent preparation.
        /// </summary>
        [Test]
        public void TestThrows404ForInvalidPreparation()
        {
            try
            {
                var spectrum = _controller.Get(id: 0, spectrumId: 1);
                Assert.Fail("Should throw for non-existent preparation");
            }
            catch (HttpResponseException e)
            {
                Assert.AreEqual(
                    actual: e.Response.StatusCode,
                    expected: HttpStatusCode.NotFound,
                    message: "Should respond with proper HTTP code");
            }
        }

        /// <summary>
        /// Tests getting non-existent spectrum.
        /// </summary>
        [Test]
        public void TestThrows404ForInvalidSpectrum()
        {
            var dataset = new BasicTextDataset(Path.Combine(ConfigurationManager.AppSettings["LocalDataDirectory"], "hnc1_tumor.txt"));
            var preparationId = 1;
            var spectrumId = dataset.SpectrumCount;

            Assume.That(dataset.SpectrumCount, Is.GreaterThan(0), "Should be working on a non-empty dataset");
            Assume.That(spectrumId, Is.GreaterThanOrEqualTo(dataset.SpectrumCount), "Should be a non-existent spectrum");

            try
            {
                var spectrum = _controller.Get(id: preparationId, spectrumId: spectrumId);
                Assert.Fail("Should throw for non-existent spectrum");
            }
            catch (HttpResponseException e)
            {
                Assert.AreEqual(
                    actual: e.Response.StatusCode,
                    expected: HttpStatusCode.NotFound,
                    message: "Should respond with proper HTTP code");
            }
        }

        /// <summary>
        /// Tests the get first preparation sample spectrum.
        /// </summary>
        [Test]
        public void TestGetFirstPreparationSampleSpectrum()
        {
            var spectrum = _controller.Get(id: 1, spectrumId: 2);

            Assert.NotNull(spectrum);
            Assert.IsInstanceOf<Spectrum>(spectrum);

            Assert.IsNotEmpty(spectrum.Intensities);
            Assert.AreEqual(actual: spectrum.Intensities.First(), expected: 5487.54763657640, delta: Delta);

            Assert.IsNotEmpty(spectrum.Mz);
            Assert.AreEqual(actual: spectrum.Mz.First(), expected: 799.796609809649, delta: Delta);

            Assert.AreEqual(
                expected: spectrum.Mz.Count(),
                actual: spectrum.Intensities.Count(),
                message: "Intensities and Mz counts do not match.");
        }

        /// <summary>
        /// Tests reading of sample spectrum by valid coordinates.
        /// </summary>
        [Test]
        public void TestReturnsSampleSpectrumByCoords()
        {
            var spectrum = _controller.Get(id: 1, x: 94, y: 31);

            Assert.NotNull(spectrum);
            Assert.IsInstanceOf<Spectrum>(spectrum);

            Assert.IsNotEmpty(spectrum.Intensities);
            Assert.AreEqual(actual: spectrum.Intensities.First(), expected: 5487.54763657640, delta: Delta);

            Assert.IsNotEmpty(spectrum.Mz);
            Assert.AreEqual(actual: spectrum.Mz.First(), expected: 799.796609809649, delta: Delta);

            Assert.AreEqual(
                expected: spectrum.Mz.Count(),
                actual: spectrum.Intensities.Count(),
                message: "Intensities and Mz counts should match.");
        }

        /// <summary>
        /// Tests reading of sample spectrum by invalid coordinates.
        /// </summary>
        [Test]
        public void TestThrows404ForInvalidCoords()
        {
            try
            {
                var spectrum = _controller.Get(id: 1, x: 0, y: 0);
                Assert.Fail("Should throw for invalid coords");
            }
            catch (HttpResponseException e)
            {
                Assert.AreEqual(
                    actual: e.Response.StatusCode,
                    expected: HttpStatusCode.NotFound,
                    message: "Should respond with proper HTTP code");
            }
        }
    }
}
