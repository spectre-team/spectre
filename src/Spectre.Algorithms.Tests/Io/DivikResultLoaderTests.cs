/*
 * DivikResultLoaderTests.cs
 * Tests loading and parsing DiviK results from MAT-files.
 *
   Copyright 2017 Spectre Team

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

using System.IO;
using NUnit.Framework;
using Spectre.Algorithms.Io;
using Spectre.Algorithms.Results;

namespace Spectre.Algorithms.Tests.Io
{
    [TestFixture]
    [Category(name: "Algorithm")]
    public class DivikResultLoaderTests
    {
        private readonly string _testFilesDirectory = TestContext.CurrentContext.TestDirectory + "\\..\\..\\..\\..\\..\\test_files";
        private string _samplePath;

        [SetUp]
        public void SetUp()
        {
            _samplePath = Path.GetFullPath(Path.Combine(_testFilesDirectory, "sample_divik_result.mat"));
    }

        [Test]
        public void InitializesWithMcrBackend()
        {
            Assert.DoesNotThrow(
                code: () =>
                {
                    using (var loader = new DivikResultLoader())
                    { }
                });
        }

        [Test]
        public void LoadsTreeWithoutException()
        {
            using (var loader = new DivikResultLoader())
            {
                Assert.DoesNotThrow(code: () => loader.Load(_samplePath));
            }
        }

        [Test]
        public void LoadedTreeIsNotNull()
        {
            using (var loader = new DivikResultLoader())
            {
                var tree = loader.Load(_samplePath);
                Assert.NotNull(tree);
            }
        }

        [Test]
        public void LoadsAmplitudeThreshold()
        {
            using (var loader = new DivikResultLoader())
            {
                var tree = loader.Load(_samplePath);
                Assert.AreEqual(expected: 12.3806, actual: tree.AmplitudeThreshold, delta: 0.001);
            }
        }

        [Test]
        public void LoadsVarianceThreshold()
        {
            using (var loader = new DivikResultLoader())
            {
                var tree = loader.Load(_samplePath);
                Assert.AreEqual(expected: float.NegativeInfinity, actual: tree.VarianceThreshold);
            }
        }

        [Test]
        public void LoadsAmplitudeFilterOfProperLength()
        {
            using (var loader = new DivikResultLoader())
            {
                var tree = loader.Load(_samplePath);
                Assert.AreEqual(expected: 2398, actual: tree.AmplitudeFilter.Length);
            }
        }

        [Test]
        public void LoadsVarianceFilterOfProperLength()
        {
            using (var loader = new DivikResultLoader())
            {
                var tree = loader.Load(_samplePath);
                Assert.AreEqual(expected: 2263, actual: tree.VarianceFilter.Length);
            }
        }

        [Test]
        public void LoadsMergedPartitionOfProperLength()
        {
            using (var loader = new DivikResultLoader())
            {
                var tree = loader.Load(_samplePath);
                Assert.AreEqual(expected: 7671, actual: tree.Merged.Length);
            }
        }

        [Test]
        public void LoadsSubregions()
        {
            using (var loader = new DivikResultLoader())
            {
                var tree = loader.Load(_samplePath);
                Assert.AreEqual(expected: 2, actual: tree.Subregions.Length);
            }
        }

        [Test]
        public void LoadedSubregionsAreNotNullIfExisted()
        {
            using (var loader = new DivikResultLoader())
            {
                var tree = loader.Load(_samplePath);
                Assert.NotNull(anObject: tree.Subregions[0]);
            }
        }
    }
}
