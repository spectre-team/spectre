/*
 * PreparationsControllerTest.cs
 * Test for proper responses after requests.
 *
   Copyright 2017 Grzegorz Mrukwa, Michał Gallus

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

using System.Linq;
using NUnit.Framework;
using Spectre.Controllers;
using Spectre.Models.Msi;

namespace Spectre.Tests.Controllers
{
    /// <summary>
    /// Tests preparations controller
    /// </summary>
    [TestFixture]
    [Ignore("Not updated to new controllers")]
    public class PreparationsControllerTest
    {
        private PreparationsController _controller;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _controller = new PreparationsController();
        }

        /// <summary>
        /// Tests the get list of preparations.
        /// </summary>
        [Test]
        public void TestGetListOfPreparations()
        {
            var list = _controller.Get();

            Assert.NotNull(list);
            Assert.IsNotEmpty(list);
            Assert.AreEqual(expected: 1, actual: list.Count());
            Assert.AreEqual(
                expected: "Head & neck cancer, patient 1, tumor region only",
                actual: list.First().Name);
        }

        /// <summary>
        /// Tests the get first preparation.
        /// </summary>
        [Test]
        public void TestGetFirstPreparation()
        {
            var first = _controller.Get(id: 1);

            Assert.NotNull(first);
            Assert.IsInstanceOf<Preparation>(first);
            Assert.AreEqual(expected: 1, actual: first.Id);
            Assert.AreEqual(expected: "Head & neck cancer, patient 1, tumor region only", actual: first.Name);
        }
    }
}
