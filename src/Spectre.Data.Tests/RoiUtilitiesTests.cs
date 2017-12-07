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
        private readonly string _path = Path.Combine(TestContext.CurrentContext.TestDirectory, "..\\..\\..\\..\\..\\test_files\\Rois");
        private string _testPath;
        private string _testFilesPath;
        private string _writeTestPath;
        private readonly Roi _readRoiDataset = new Roi();
        private readonly Roi _writeRoiRataset = new Roi();

        [SetUp]
        public void SetUp()
        {
            _testPath = Path.GetFullPath(_path);
            _testFilesPath = Path.Combine(_testPath, "image1.png");
            _writeTestPath = Path.Combine(_testPath, "writetestfile.png");
            _readRoiDataset.Name = "image1";
            _readRoiDataset.Height = 6;
            _readRoiDataset.Width = 6;
            _readRoiDataset.RoiPixels = new List<RoiPixel>
            {
                new RoiPixel(1, 1),
                new RoiPixel(2, 1),
                new RoiPixel(3, 1)
            };

            _writeRoiRataset.Name = "writetestfile";
            _writeRoiRataset.Height = 10;
            _writeRoiRataset.Width = 10;
            _writeRoiRataset.RoiPixels = new List<RoiPixel>
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
            RoiReader service = new RoiReader(_testPath);

            var names = service.ListRoisFromDirectory();

            Assert.AreEqual("image1.png", Path.GetFileName(names[0]));
            Assert.AreEqual("image2.png", Path.GetFileName(names[1]));
        }

        [Test]
        public void ReadRoi_returns_proper_roi_pixels()
        {
            RoiReader service = new RoiReader(_testFilesPath);
            var roi = service.RoiLoader();

            Assert.AreEqual(roi.RoiPixels[0].XCoordinate, _readRoiDataset.RoiPixels[0].XCoordinate);
            Assert.AreEqual(roi.RoiPixels[0].YCoordinate, _readRoiDataset.RoiPixels[0].YCoordinate);

            Assert.AreEqual(roi.RoiPixels[1].XCoordinate, _readRoiDataset.RoiPixels[1].XCoordinate);
            Assert.AreEqual(roi.RoiPixels[1].YCoordinate, _readRoiDataset.RoiPixels[1].YCoordinate);

            Assert.AreEqual(roi.RoiPixels[2].XCoordinate, _readRoiDataset.RoiPixels[2].XCoordinate);
            Assert.AreEqual(roi.RoiPixels[2].YCoordinate, _readRoiDataset.RoiPixels[2].YCoordinate);
        }

        [Test]
        public void ReadRoi_returns_proper_dimensions()
        {
            RoiReader service = new RoiReader(_testFilesPath);
            var roi = service.RoiLoader();

            Assert.AreEqual(roi.Height, _readRoiDataset.Height);
            Assert.AreEqual(roi.Width, _readRoiDataset.Width);
        }

        [Test]
        public void WriteRoi_writes_file_properly()
        {
            RoiWriter service = new RoiWriter(_testPath);

            service.RoiUploader(_writeRoiRataset);
            
            RoiReader checkIfProperlyWrittenService = new RoiReader(_writeTestPath);

            var writetestroi = checkIfProperlyWrittenService.RoiLoader();

            Assert.AreEqual(writetestroi.RoiPixels[0].XCoordinate, _writeRoiRataset.RoiPixels[0].XCoordinate);
            Assert.AreEqual(writetestroi.RoiPixels[0].YCoordinate, _writeRoiRataset.RoiPixels[0].YCoordinate);

            Assert.AreEqual(writetestroi.RoiPixels[1].XCoordinate, _writeRoiRataset.RoiPixels[1].XCoordinate);
            Assert.AreEqual(writetestroi.RoiPixels[1].YCoordinate, _writeRoiRataset.RoiPixels[1].YCoordinate);

            Assert.AreEqual(writetestroi.RoiPixels[2].XCoordinate, _writeRoiRataset.RoiPixels[2].XCoordinate);
            Assert.AreEqual(writetestroi.RoiPixels[2].YCoordinate, _writeRoiRataset.RoiPixels[2].YCoordinate);

            Assert.AreEqual(writetestroi.RoiPixels[3].XCoordinate, _writeRoiRataset.RoiPixels[3].XCoordinate);
            Assert.AreEqual(writetestroi.RoiPixels[3].YCoordinate, _writeRoiRataset.RoiPixels[3].YCoordinate);
        }
    }
}
