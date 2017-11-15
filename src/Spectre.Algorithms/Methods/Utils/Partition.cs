/*
 * Partition.cs
 * Provides methods simplifying operations on partition vectors.
 *
   Copyright 2017 Grzegorz Mrukwa

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

namespace Spectre.Algorithms.Methods.Utils
{
    /// <summary>
    /// Methods simplifying access and operations on partitioning vectors.
    /// </summary>
    public static class Partition
    {
        /// <summary>
        /// Compares specified partitions.
        /// </summary>
        /// <typeparam name="T1">Type of first labels.</typeparam>
        /// <typeparam name="T2">Type of second labels.</typeparam>
        /// <param name="partition1">The first partition.</param>
        /// <param name="partition2">The second partition.</param>
        /// <param name="tolerance">The tolerance rate of mismatch.</param>
        /// <returns><value>true</value>, if partitions match; <value>false</value> otherwise.</returns>
        /// <exception cref="System.ArgumentException">Lengths of partitions differ.</exception>
        /// <exception cref="ArgumentNullException">Any of partitions is null.</exception>
        public static bool Compare<T1, T2>(IEnumerable<T1> partition1, IEnumerable<T2> partition2, double tolerance)
        {
            if (partition1 == null)
            {
                throw new ArgumentNullException(paramName: nameof(partition1));
            }
            if (partition2 == null)
            {
                throw new ArgumentNullException(paramName: nameof(partition2));
            }

            var typedPartition1 = partition1.ToArray();
            var typedPartition2 = partition2.ToArray();

            if (typedPartition1.Length != typedPartition2.Length)
            {
                throw new ArgumentException(message: "Lengths of partitions differ.");
            }

            var simple1 = Partition.Simplify(typedPartition1);
            var simple2 = Partition.Simplify(typedPartition2);

            var matched = simple1.Zip(
                simple2,
                resultSelector: (assignment1, assignment2) => assignment1 == assignment2 ? 1 : 0);
            var matchesCount = matched.Sum();

            var compatibilityRate = (double)matchesCount / typedPartition1.Length;
            var requiredCompatibilityRate = 1 - tolerance;

            return requiredCompatibilityRate <= compatibilityRate;
        }

        /// <summary>
        /// Simplifies the specified partition, to be defined on integer labels starting from 1.
        /// </summary>
        /// <typeparam name="T">Type of label</typeparam>
        /// <param name="partition">The partition.</param>
        /// <returns>Simplified partition.</returns>
        /// <exception cref="ArgumentNullException">partition is null.</exception>
        public static IEnumerable<int> Simplify<T>(IEnumerable<T> partition)
        {
            var labelsDictionary = new Dictionary<T, int>();

            var currentIntLabel = 1;
            var typedPartition = partition.ToArray();
            foreach (var label in typedPartition)
            {
                if (!labelsDictionary.ContainsKey(label))
                {
                    labelsDictionary.Add(label, currentIntLabel++);
                }
            }

            var newLabeling = new int[typedPartition.Length];
            for (var i = 0; i < typedPartition.Length; ++i)
            {
                newLabeling[i] = labelsDictionary[key: typedPartition[i]];
            }

            return newLabeling;
        }

        /// <summary>
        /// Gets the cluster sizes.
        /// </summary>
        /// <typeparam name="T">Type of class descriptions.</typeparam>
        /// <param name="partition">The partition.</param>
        /// <returns>Numbers of observations in each cluster.</returns>
        public static Dictionary<T, uint> GetClusterSizes<T>(IEnumerable<T> partition)
        {
            var counts = new Dictionary<T, uint>();
            foreach (var assignment in partition)
            {
                if (!counts.ContainsKey(assignment))
                {
                    counts[assignment] = 0;
                }
                ++counts[assignment];
            }
            return counts;
        }
    }
}
