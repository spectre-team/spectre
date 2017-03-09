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
    public struct Metadata
    {
        //TODO: Add more fields as more metadata will appear to be needed

        /// <summary>
        /// Spacial coordinate X.
        /// </summary>
        public int X;
        /// <summary>
        /// Spacial coordinate Y.
        /// </summary>
        public int Y;
        /// <summary>
        /// Spacial coordinate Z.
        /// </summary>
        public int Z;

        public static Metadata Default()
        {
            Metadata metadata;
            metadata.X = 0;
            metadata.Y = 0;
            metadata.Z = 0;
            return metadata;
        }
    }
}
