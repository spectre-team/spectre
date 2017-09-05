/*
 * MetricTests.cs
 * Checks, whether serialization and deserialization of enum Metric is properly.
 * 
   Copyright 2017 Sebastian Pustelnik

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
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using Spectre.Algorithms.Parameterization;

namespace Spectre.Algorithms.Tests.Parameterization
{
    /// <summary>
    /// Tests for enum Metric
    /// </summary>
    [TestFixture]
    internal class MetricTests
    {
        public class SampleMetric
        {
            [JsonConverter(typeof(StringEnumConverter))]
            public Metric Metric { get; set; }
        }

        /// <summary>
        /// Tests deserialize Metric.
        /// </summary>
        [Test]
        public void DeserializeMetricTest()
        {
            Dictionary<string, Metric> expectations = GetMetricMapping();

            foreach (var kv in expectations)
            {
                var jsonString = string.Format("{{ \"Metric\" : \"{0}\" }}", kv.Key);
                var deserializedObject = JsonConvert.DeserializeObject<SampleMetric>(jsonString);
                Assert.AreEqual(kv.Value, deserializedObject.Metric);
            }
        }

        /// <summary>
        /// Tests serializable Metric.
        /// </summary>
        [Test]
        public void SerializeMetricTest()
        {
            Dictionary<string, Metric> expectations = GetMetricMapping();

            foreach (var kv in expectations)
            {
                var obj = new SampleMetric() {Metric = kv.Value};

                var expectedJsonString = string.Format("{{\"Metric\":\"{0}\"}}", kv.Key);
                var actualJsonString = JsonConvert.SerializeObject(obj);

                Assert.AreEqual(expectedJsonString, actualJsonString);
            }
        }

        private Dictionary<string, Metric> GetMetricMapping() => new Dictionary<string, Metric>()
        {
            {"Chebychev", Metric.Chebychev},
            {"Cityblock", Metric.Cityblock},
            {"Cosine", Metric.Cosine},
            {"Euclidean", Metric.Euclidean},
            {"Hamming", Metric.Hamming},
            {"Jaccard", Metric.Jaccard},
            {"Mahalanobis", Metric.Mahalanobis},
            {"Minkowski", Metric.Minkowski},
            {"Pearson", Metric.Pearson},
            {"Spearman", Metric.Spearman}
        };
    }
}
