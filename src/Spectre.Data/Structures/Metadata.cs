/*
 * Metadata.cs
 * Contains struct for storing metadata of associated dataset.
 * 
   Copyright 2017 Dariusz Kuchta

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
namespace Spectre.Data.Structures
{
    /// <summary>
    /// Contains struct for storing metadata of associated dataset.
    /// </summary>
    public struct Metadata
    {
        public string Description;  // dummy

        /// <summary>
        /// Default template instance for <see cref="Metadata"/> class.
        /// </summary>
        /// <returns>Default metadata.</returns>
        public static Metadata Default()
        {
            Metadata metadata;
            metadata.Description = "default";
            return metadata;
        }
    }
}
