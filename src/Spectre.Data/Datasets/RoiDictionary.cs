/*
    * RoiDictionary.cs
    * Class representing a set of tool to manage ROIs.
    * Allows getting ROIs by name, adding and removing by name.

    Copyright 2017 Roman LIsak

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

using System.Collections.Generic;
using System.Linq;
using Spectre.Data.RoiIo;

namespace Spectre.Data.Datasets
{
    /// <summary>
    /// Class representing a set of tool to manage ROIs.
    /// </summary>
    /// <seealso cref="Spectre.Data.Datasets.IRoiDictionary" />
    public class RoiDictionary : IRoiDictionary
    {
        private List<Roi> _roiDataset;
        private RoiReader _roireader = new RoiReader();

        /// <summary>
        /// Initializes a new instance of the <see cref="RoiDictionary" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public RoiDictionary(string path)
        {
            _roiDataset = _roireader.GetAllRoisFromDirectory(path);
        }

        /// <summary>
        /// Tries to get specified value from list by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// Returns roi with specified name.
        /// Returns null if no roi with specified name found.
        /// </returns>
        public List<Roi> GetRoiOrDefault(string name)
        {
            var dataset = _roiDataset.Where(r => r.Name == name).ToList();

            if (dataset.Count() != 0)
            {
                return dataset;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="roi">The roi.</param>
        public void Add(Roi roi)
        {
            _roiDataset.Add(roi);
        }

        /// <summary>
        /// Removes the record with specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void Remove(string name)
        {
            var query = _roiDataset.Where(r => r.Name == name);
            var dataset = query.ToList();
            _roiDataset.Remove(dataset.First());
        }
    }
}
