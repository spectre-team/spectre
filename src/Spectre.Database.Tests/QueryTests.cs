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
using System.IO;
using System.Linq;
using Spectre.Database.Entities;
using Spectre.Database.Utils;
using Spectre.Database.Contexts;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Mocks;
using Moq;

namespace Spectre.Database.Tests
{
    [TestFixture]
    public class QueryTests
    {
        [Test]
        public void QueryTest()
        {
            var data = new List<Dataset>
            {
                new Dataset { FriendlyName = "Jacek1", Hash = "1", Location = "path1"},
                new Dataset { FriendlyName = "Bajer2", Hash = "2", Location = "path2"},
                new Dataset { FriendlyName = "Janusz3", Hash = "3", Location = "path3"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Dataset>>();
            mockSet.As<IQueryable<Dataset>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Dataset>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Dataset>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Dataset>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            
            var mockContext = new Mock<Context>();
            mockContext.Setup(c => c.Datasets).Returns(mockSet.Object);

            var service = new PathFinder(mockContext.Object);
            var dataset1 = service.ReturnForHash("1");
            var dataset2 = service.ReturnForFriendlyName("Bajer2");
            var dataset3 = service.ReturnForHash("4");

            NUnit.Framework.Assert.AreEqual("path1", dataset1);
            NUnit.Framework.Assert.AreEqual("path2", dataset2);
            NUnit.Framework.Assert.AreEqual(null, dataset3);
        }
    }
}


