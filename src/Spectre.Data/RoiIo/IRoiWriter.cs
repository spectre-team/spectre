/*
 * IRoiWriter.cs
 * Interface for RoiWriter.

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

using Spectre.Data.Datasets;

namespace Spectre.Data.RoiIo
{
    /// <summary>
    /// Interface for RoiWriter.
    /// </summary>
    public interface IRoiWriter
    {
        /// <summary>
        /// Writes roi dataset into a png file.
        /// </summary>
        /// <param name="roidataset">The prototyp.</param>
        void RoiWriter(Roi roidataset);
    }
}
