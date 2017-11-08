/*
 * TreeProcessor.cs
 * Algorithms for tree processing.
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

using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Algorithms.DataStructures;

namespace Spectre.Algorithms.StructureBoundAlgorithms
{
    /// <summary>
    /// Common algorithms for processing trees.
    /// </summary>
    /// <seealso cref="Spectre.Algorithms.DataStructures.ITree" />
    public static class TreeProcessor
    {
        /// <summary>
        /// Folds the specified fold node.
        /// </summary>
        /// <typeparam name="TOutput">The type of the utput type.</typeparam>
        /// <param name="tree">The tree.</param>
        /// <param name="foldNode">Node folding function.</param>
        /// <param name="initialValue">The initial value of function at leaves.</param>
        /// <returns>Result of tree folding</returns>
        public static TOutput Fold<TOutput>(this ITree tree, Func<ITree, IEnumerable<TOutput>, TOutput> foldNode, TOutput initialValue)
        {
            var children = tree.Children.ToArray();
            if (children.Count(predicate: child => child != null) == 0)
            {
                return initialValue;
            }
            var subresults = children.Select(selector: child => child.Fold(foldNode, initialValue));
            return foldNode(tree, subresults);
        }

        /// <summary>
        /// Depth of the specified tree.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <returns>Tree's depth</returns>
        public static uint Depth(this ITree tree)
        {
            return tree.Fold<uint>(
                foldNode: (subtree, subdepths) => subdepths.Max() + 1,
                initialValue: 1);
        }
    }
}
