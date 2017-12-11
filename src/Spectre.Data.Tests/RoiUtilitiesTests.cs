/*
 * RoiUtilitesTests.cs
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
using NUnit.Framework;
using Spectre.Data.Datasets;
using Spectre.Data.RoiIo;

namespace Spectre.Data.Tests
{
    [TestFixture]
    public class RoiUtilitiesTests
    {
        private readonly string _path = Path.Combine(TestContext.CurrentContext.TestDirectory, "..\\..\\..\\..\\..\\test_files\\Rois");
        private string _testDirectoryPath;
        private string _testReadFilesPath;
        private string _testWriteFilePath;
        private Roi _readRoiDataset;
        private Roi _writeRoiRataset;
        private Roi _addRoiRataset;

        [SetUp]
        public void SetUp()
        {
            _testDirectoryPath = Path.GetFullPath(_path);
            _testReadFilesPath = Path.Combine(_testDirectoryPath, "image1.png");
            _testWriteFilePath = Path.Combine(_testDirectoryPath, "writetestfile.png");

            _readRoiDataset = new Roi("image1", 6, 6, new List<RoiPixel>
            {
                new RoiPixel(1, 1),
                new RoiPixel(2, 1),
                new RoiPixel(3, 1)
            });

            _writeRoiRataset = new Roi("writetestfile", 10, 10, new List<RoiPixel>
            {
                new RoiPixel(1, 5),
                new RoiPixel(2, 5),
                new RoiPixel(3, 5),
                new RoiPixel(4, 5)
            });

            _addRoiRataset = new Roi("addtestfile", 10, 10, new List<RoiPixel>
            {
                new RoiPixel(1, 6),
                new RoiPixel(2, 6),
                new RoiPixel(3, 6),
                new RoiPixel(4, 6)
            });
        }

        [Test]
        public void GetAllRoisFromDirectory_returns_proper_rois()
        {
            RoiReader service = new RoiReader();

            var allRoisFromDirectory = service.GetAllRoisFromDirectory(_testDirectoryPath);

            Assert.AreEqual(actual: allRoisFromDirectory[0].Name, expected: _readRoiDataset.Name);
            Assert.AreEqual(actual: allRoisFromDirectory[1].Name, expected: _writeRoiRataset.Name);
        }

        [Test]
        public void RoiDictionary_GetRoiOrDefault_returns_proper_roi()
        {
            var roiDictionaryService = new RoiDictionary(_testDirectoryPath);
            var obtainedRoi = roiDictionaryService.GetRoiOrDefault("image1");

            Assert.AreEqual(actual: obtainedRoi.First().Name, expected: "image1");
        }

        [Test]
        public void RoiDictionary_Add_adds_roi_properly()
        {
            var roiDictionaryService = new RoiDictionary(_testDirectoryPath);

            roiDictionaryService.Add(_addRoiRataset);

            var obtainedRoi = roiDictionaryService.GetRoiOrDefault("addtestfile");

            Assert.AreEqual(actual: obtainedRoi.First().Name, expected: "addtestfile");
        }

        [Test]
        public void RoiDictionary_Remove_removes_properly()
        {
            var roiDictionaryService = new RoiDictionary(_testDirectoryPath);

            roiDictionaryService.Remove("image1");

            var obtainedRoi = roiDictionaryService.GetRoiOrDefault("image1");

            Assert.IsNull(obtainedRoi);
        }

        [Test]
        public void ReadRoi_returns_proper_roi_pixels()
        {
            RoiReader service = new RoiReader();
            var roi = service.RoiDownloader(_testReadFilesPath);

            Assert.AreEqual(actual: roi.RoiPixels[0].XCoordinate, expected: _readRoiDataset.RoiPixels[0].XCoordinate);
            Assert.AreEqual(actual: roi.RoiPixels[0].YCoordinate, expected: _readRoiDataset.RoiPixels[0].YCoordinate);

            Assert.AreEqual(actual: roi.RoiPixels[1].XCoordinate, expected: _readRoiDataset.RoiPixels[1].XCoordinate);
            Assert.AreEqual(actual: roi.RoiPixels[1].YCoordinate, expected: _readRoiDataset.RoiPixels[1].YCoordinate);

            Assert.AreEqual(actual: roi.RoiPixels[2].XCoordinate, expected: _readRoiDataset.RoiPixels[2].XCoordinate);
            Assert.AreEqual(actual: roi.RoiPixels[2].YCoordinate, expected: _readRoiDataset.RoiPixels[2].YCoordinate);
        }

        [Test]
        public void ReadRoi_returns_proper_dimensions()
        {
            RoiReader service = new RoiReader();
            var roi = service.RoiDownloader(_testReadFilesPath);

            Assert.AreEqual(actual: roi.Height, expected: _readRoiDataset.Height);
            Assert.AreEqual(actual: roi.Width, expected: _readRoiDataset.Width);
        }

        [Test]
        public void WriteRoi_writes_file_properly()
        {
            RoiWriter service = new RoiWriter(_testDirectoryPath);

            service.RoiUploader(_writeRoiRataset);

            RoiReader checkIfProperlyWrittenService = new RoiReader();

            var writetestroi = checkIfProperlyWrittenService.RoiDownloader(_testWriteFilePath);

            Assert.AreEqual(actual: writetestroi.RoiPixels[0].XCoordinate, expected: _writeRoiRataset.RoiPixels[0].XCoordinate);
            Assert.AreEqual(actual: writetestroi.RoiPixels[0].YCoordinate, expected: _writeRoiRataset.RoiPixels[0].YCoordinate);

            Assert.AreEqual(actual: writetestroi.RoiPixels[1].XCoordinate, expected: _writeRoiRataset.RoiPixels[1].XCoordinate);
            Assert.AreEqual(actual: writetestroi.RoiPixels[1].YCoordinate, expected: _writeRoiRataset.RoiPixels[1].YCoordinate);

            Assert.AreEqual(actual: writetestroi.RoiPixels[2].XCoordinate, expected: _writeRoiRataset.RoiPixels[2].XCoordinate);
            Assert.AreEqual(actual: writetestroi.RoiPixels[2].YCoordinate, expected: _writeRoiRataset.RoiPixels[2].YCoordinate);

            Assert.AreEqual(actual: writetestroi.RoiPixels[3].XCoordinate, expected: _writeRoiRataset.RoiPixels[3].XCoordinate);
            Assert.AreEqual(actual: writetestroi.RoiPixels[3].YCoordinate, expected: _writeRoiRataset.RoiPixels[3].YCoordinate);
        }

        [Test]
        public void Roi_Constructor_Sets_RoiPixels_in_range()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                code: () =>
                {
                    new Roi(
                        "randomname",
                        10,
                        10,
                        new List<RoiPixel>
                        {
                            new RoiPixel(15, 6),
                            new RoiPixel(1, 15)
                        });
                });
        }
    }
 }