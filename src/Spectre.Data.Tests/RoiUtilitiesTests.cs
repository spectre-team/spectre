/*
 * RoiUtilitesTests
 * Class with tests for RoiUtilities class.

   Copyright 2017 Roman Lisak

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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Spectre.Data.Datasets;
using Spectre.Data.RoiIo;

namespace Spectre.Data.Tests
{
    [TestFixture]
    public class RoiUtilitiesTests
    {
        private readonly string _path = TestContext.CurrentContext.TestDirectory + "\\..\\..\\..\\..\\..\\test_files\\Rois";
        private string _testpath;
        private string _readpath1;
        private int _properheight;
        private int _properwidth;
        private readonly List<RoiPixel> _properroipixels = new List<RoiPixel>();

        [SetUp]
        public void SetUp()
        {
            _testpath = Path.GetFullPath(_path);
            _readpath1 = _testpath + "\\image1.png";
            _properheight = 6;
            _properwidth = 6;

            _properroipixels.Add(new RoiPixel(1, 1));
            _properroipixels.Add(new RoiPixel(2, 1));
            _properroipixels.Add(new RoiPixel(3, 1));
        }

        [Test]
        public void ListRoisFromDirectory_returns_proper_names()
        {
            RoiUtilities service = new RoiUtilities();

            var names = service.ListRoisFromDirectory(_testpath);

            Assert.AreEqual("image1.png", Path.GetFileName(names[0]));
            Assert.AreEqual("image2.png", Path.GetFileName(names[1]));
        }

        [Test]
        public void ReadRoi_returns_proper_roi_pixels()
        {
            RoiUtilities service = new RoiUtilities();
            var roi = service.RoiReader(_readpath1);

            Assert.AreEqual(roi.RoiPixels[0].GetXCoord(), _properroipixels[0].GetXCoord());
            Assert.AreEqual(roi.RoiPixels[0].GetYCoord(), _properroipixels[0].GetYCoord());

            Assert.AreEqual(roi.RoiPixels[1].GetXCoord(), _properroipixels[1].GetXCoord());
            Assert.AreEqual(roi.RoiPixels[1].GetYCoord(), _properroipixels[1].GetYCoord());

            Assert.AreEqual(roi.RoiPixels[2].GetXCoord(), _properroipixels[2].GetXCoord());
            Assert.AreEqual(roi.RoiPixels[2].GetYCoord(), _properroipixels[2].GetYCoord());
        }

        [Test]
        public void ReadRoi_returns_proper_dimensions()
        {
            RoiUtilities service = new RoiUtilities();
            var roi = service.RoiReader(_readpath1);

            Assert.AreEqual(roi.Height, _properheight);
            Assert.AreEqual(roi.Width, _properwidth);
        }
    }
}
