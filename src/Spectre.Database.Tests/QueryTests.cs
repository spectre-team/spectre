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
        private Mock<Context> _mockContext = new Mock<Context>();

        [SetUp]
        public void SetUp()
        {
            var data = new List<Dataset>
            {
                new Dataset { FriendlyName = "Jacek1", Hash = "1", UploadNumber = "1"},
                new Dataset { FriendlyName = "Bajer2", Hash = "2", UploadNumber = "2"},
                new Dataset { FriendlyName = "Janusz3", Hash = "3", UploadNumber = "3"},
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

            var dataset = service.ReturnForHash("1");            

            NUnit.Framework.Assert.AreEqual("1", dataset);
        }

        [Test]
        public void Return_for_friendly_name_test()
        {
            var service = new Finder(_mockContext.Object);

            var dataset = service.ReturnForFriendlyName("Bajer2");

            NUnit.Framework.Assert.AreEqual("2", dataset);
        }

        [Test]
        public void Return_for_not_existing_hash_test()
        {
            var service = new Finder(_mockContext.Object);

            var dataset = service.ReturnForHash("4");

            NUnit.Framework.Assert.AreEqual(null, dataset);
        }

        [Test]
        public void Return_for_not_existing_friendly_name_test()
        {
            var service = new Finder(_mockContext.Object);

            var dataset = service.ReturnForFriendlyName("TestName");

            NUnit.Framework.Assert.AreEqual(null, dataset);
        }
    }
}