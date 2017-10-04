/*
 * Range.cs
 * Class representing value range.
 *
   Copyright 2017 Maciej Gamrat

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

using System.Runtime.Serialization;

namespace Spectre.Models.Msi
{
    /// <summary>
    /// Represents a value range.
    /// </summary>
    [DataContract]
    public class Range
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Range"/> class.
        /// </summary>
        /// <param name="min">Minimal value.</param>
        /// <param name="max">Maximal value.</param>
        public Range(int min, int max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Stores range minimum.
        /// </summary>
        /// <value>
        /// Range minimum.
        /// </value>
        [DataMember]
        public int Min { get; set; }

        /// <summary>
        /// Stores range maximum.
        /// </summary>
        /// <value>
        /// Range maximum.
        /// </value>
        [DataMember]
        public int Max { get; set; }
    }
}
