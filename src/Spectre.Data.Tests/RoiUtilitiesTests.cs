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

        [SetUp]
        public void SetUp()
        {
            _testpath = Path.GetFullPath(_path);
        }

        [Test]
        public void ListRoisFromDirectory_returns_proper_names()
        {
            RoiUtilities service = new RoiUtilities();

            var names = service.ListRoisFromDirectory(_testpath);
            
            Assert.AreEqual("image1.png",Path.GetFileName(names[0]));
            Assert.AreEqual("image2.png",Path.GetFileName(names[1]));
        }

        [Test]
        public void ReadRoi_returns_proper_Rois_object()
        {
            RoiUtilities service = new RoiUtilities();

            string readpath1 = _testpath + "\\image1.png";

            var roi1 = service.RoiReader(readpath1);
        }
    }
}
