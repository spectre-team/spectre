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

using Moq;
using Spectre.Database.Utils;
using Spectre.Database.Contexts;
using NUnit.Framework;
using Spectre.Dependencies;
using Spectre.Dependencies.Modules;

namespace Spectre.Database.Tests
{
    [TestFixture]
    public class QueryTests
    {
        private DatasetsContext _mockContext;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            DependencyResolver.AddModule(new MockModule());
        }

        [SetUp]
        public void SetUp()
        {
           _mockContext = DependencyResolver.GetService<DatasetsContext>();
        }

        [Test]
        public void HashToUploadNumber_finds_proper_uploadnumber_for_given_hash()
        {
            var service = new DatasetDetailsFinder(_mockContext);

            var dataset = service.HashToUploadNumberOrDefault("Hash1");

            Assert.AreEqual("UploadNumber1", dataset);
        }

        [Test]
        public void FriendlyNameToUploadNumber_finds_proper_uploadnumber_for_given_friendlyname()
        {
            var service = new DatasetDetailsFinder(_mockContext);

            var dataset = service.FriendlyNameToUploadNumberOrDefault("FriendlyName2");

            Assert.AreEqual("UploadNumber2", dataset);
        }

        [Test]
        public void HashToUploadNumer_returns_null_for_not_existing_hash()
        {
            var service = new DatasetDetailsFinder(_mockContext);

            var dataset = service.HashToUploadNumberOrDefault("NotExistingHash");

            Assert.IsNull(dataset);
        }

        [Test]
        public void FriendlyNameToUploadNumer_returns_null_for_not_existing_friendly_name()
        {
            var service = new DatasetDetailsFinder(_mockContext);

            var dataset = service.FriendlyNameToUploadNumberOrDefault("NotExistingFriendlyName");

            Assert.IsNull(dataset);
        }

        [Test]
        public void UploadNumberToHash_finds_proper_hash_for_given_uploadnumber()
        {
            var service = new DatasetDetailsFinder(_mockContext);

            var dataset = service.UploadNumberToHashOrDefault("UploadNumber3");

            Assert.AreEqual(dataset, "Hash3");
        }

        [Test]
        public void UploadNumberToHash_returns_null_for_not_existing_Upload_Name()
        {
            var service = new DatasetDetailsFinder(_mockContext);

            var dataset = service.UploadNumberToHashOrDefault("NotExistingUploadNumber");

            Assert.IsNull(dataset);
        }

        [Test]
        public void HashToFriendlyName_finds_proper_friendlyname_for_given_hash()
        {
            var service = new DatasetDetailsFinder(_mockContext.Object);

            var dataset = service.HashToFriendlyNameOrDefault("Hash1");

            Assert.AreEqual(dataset, "FriendlyName1");
        }

        [Test]
        public void HashToFriendlyName_returns_null_for_not_existing_Hash()
        {
            var service = new DatasetDetailsFinder(_mockContext.Object);

            var dataset = service.HashToFriendlyNameOrDefault("NotExistingHash");

            Assert.IsNull(dataset);
        }

        [Test]
        public void UploadNumberToFriendlyName_finds_proper_friendlyname_for_given_uploadnumber()
        {
            var service = new DatasetDetailsFinder(_mockContext.Object);

            var dataset = service.UploadNumberToFriendlyNameOrDefault("UploadNumber2");

            Assert.AreEqual(dataset, "FriendlyName2");
        }

        [Test]
        public void UploadNumberToFriendlyName_returns_null_for_not_existing_uploadnumber()
        {
            var service = new DatasetDetailsFinder(_mockContext.Object);

            var dataset = service.HashToFriendlyNameOrDefault("NotExistingUploadNumber");

            Assert.IsNull(dataset);
        }
    }
}