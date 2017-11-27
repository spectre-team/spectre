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
        private string _testfilespath;
        private readonly RoiDataset _readroidataset = new RoiDataset();
        private readonly RoiDataset _writeroidataset = new RoiDataset();

        [SetUp]
        public void SetUp()
        {
            _testpath = Path.GetFullPath(_path);
            _testfilespath = _testpath + "\\image1.png";
            _readroidataset.Name = "image1";
            _readroidataset.Height = 6;
            _readroidataset.Width = 6;
            _readroidataset.RoiPixels = new List<RoiPixel>
            {
                new RoiPixel(1, 1),
                new RoiPixel(2, 1),
                new RoiPixel(3, 1)
            };

            _writeroidataset.Name = "writetestfile";
            _writeroidataset.Height = 10;
            _writeroidataset.Width = 10;
            _writeroidataset.RoiPixels = new List<RoiPixel>
            {
                new RoiPixel(1, 5),
                new RoiPixel(2, 5),
                new RoiPixel(3, 5),
                new RoiPixel(4, 5),
            };
        }

        [Test]
        public void ListRoisFromDirectory_returns_proper_names()
        {
            RoiUtilities service = new RoiUtilities(_testpath);

            var names = service.ListRoisFromDirectory();

            Assert.AreEqual("image1.png", Path.GetFileName(names[0]));
            Assert.AreEqual("image2.png", Path.GetFileName(names[1]));
        }

        [Test]
        public void ReadRoi_returns_proper_roi_pixels()
        {
            RoiUtilities service = new RoiUtilities(_testfilespath);
            var roi = service.RoiReader();

            Assert.AreEqual(roi.RoiPixels[0].GetXCoord(), _readroidataset.RoiPixels[0].GetXCoord());
            Assert.AreEqual(roi.RoiPixels[0].GetYCoord(), _readroidataset.RoiPixels[0].GetYCoord());

            Assert.AreEqual(roi.RoiPixels[1].GetXCoord(), _readroidataset.RoiPixels[1].GetXCoord());
            Assert.AreEqual(roi.RoiPixels[1].GetYCoord(), _readroidataset.RoiPixels[1].GetYCoord());

            Assert.AreEqual(roi.RoiPixels[2].GetXCoord(), _readroidataset.RoiPixels[2].GetXCoord());
            Assert.AreEqual(roi.RoiPixels[2].GetYCoord(), _readroidataset.RoiPixels[2].GetYCoord());
        }

        [Test]
        public void ReadRoi_returns_proper_dimensions()
        {
            RoiUtilities service = new RoiUtilities(_testfilespath);
            var roi = service.RoiReader();

            Assert.AreEqual(roi.Height, _readroidataset.Height);
            Assert.AreEqual(roi.Width, _readroidataset.Width);
        }

        [Test]
        public void WriteRoi_writes_file_properly()
        {
            RoiUtilities service = new RoiUtilities(_testpath);

            service.RoiWriter(_writeroidataset);
        }
    }
}
