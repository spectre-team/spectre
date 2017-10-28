/*
 * QueryTests.cs
 * Testing queries for translation
 *
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
using System.Linq;
using Spectre.Database.Entities;
using Spectre.Database.Utils;
using Spectre.Database.Contexts;
using System.Data.Entity;
using NUnit.Framework;
using Moq;


namespace Spectre.Database.Tests
{
    [TestFixture]
    public class QueryTests
    {
        private Mock<DatasetsContext> _mockContext = new Mock<DatasetsContext>();

        [SetUp]
        public void SetUp()
        {
            var data = new List<Dataset>
            {
                new Dataset { FriendlyName = "FriendlyName1", Hash = "Hash1", UploadNumber = "UploadNumber1"},
                new Dataset { FriendlyName = "FriendlyName2", Hash = "Hash2", UploadNumber = "UploadNumber2"},
                new Dataset { FriendlyName = "FriendlyName3", Hash = "Hash3", UploadNumber = "UploadNumber3"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Dataset>>();
            mockSet.As<IQueryable<Dataset>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Dataset>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Dataset>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Dataset>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(c => c.Datasets).Returns(mockSet.Object);
        }

        [Test]
        public void Return_for_hash_Test()
        {
            var service = new Finder(_mockContext.Object);

            var dataset = service.ReturnForHash("Hash1");            

            Assert.AreEqual("UploadNumber1", dataset);
        }

        [Test]
        public void Return_for_friendly_name_test()
        {
            var service = new Finder(_mockContext.Object);

            var dataset = service.ReturnForFriendlyName("FriendlyName2");

            Assert.AreEqual("UploadNumber2", dataset);
        }

        [Test]
        public void Return_for_not_existing_hash_test()
        {
            var service = new Finder(_mockContext.Object);

            var dataset = service.ReturnForHash("NotExistingHash");

            Assert.AreEqual(null, dataset);
        }

        [Test]
        public void Return_for_not_existing_friendly_name_test()
        {
            var service = new Finder(_mockContext.Object);

            var dataset = service.ReturnForFriendlyName("NotExistingFriendlyName");

            Assert.AreEqual(null, dataset);
        }
    }
}