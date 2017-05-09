using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Spectre.Controllers;
using Spectre.Models.Msi;

namespace Spectre.Tests.Controllers
{
    [TestFixture]
    public class PreparationsControllerTest
    {
        private PreparationsController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new PreparationsController();
        }

        [Test]
        public void TestGetListOfPreparations()
        {
            var list = _controller.Get();

            Assert.NotNull(list);
            Assert.IsNotEmpty(list);
            Assert.AreEqual(1, list.Count());
            Assert.AreEqual("Head & neck cancer, patient 1, tumor region only", list.First().Name);
        }

        [Test]
        public void TestGetFirstPreparation()
        {
            var first = _controller.Get(1);

            Assert.NotNull(first);
            Assert.IsInstanceOf<Preparation>(first);
            Assert.AreEqual(1, first.Id);
            Assert.AreEqual("Head & neck cancer, patient 1, tumor region only", first.Name);
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
