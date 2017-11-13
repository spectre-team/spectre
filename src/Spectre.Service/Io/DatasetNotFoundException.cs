/*
 * DatasetNotFoundException.cs
 * Exception thrown when dataset loader fails to find file with dataset.
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

using System;
using System.IO;

namespace Spectre.Service.Io
{
    /// <summary>
    /// Thrown when dataset loader fails to find file with dataset.
    /// </summary>
    public class DatasetNotFoundException : FileNotFoundException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="fileName">Name of file.</param>
        public DatasetNotFoundException(string message, string fileName) : base(message, fileName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="fileName">Name of file.</param>
        /// <param name="innerException">The original exception thrown during validation.</param>
        public DatasetNotFoundException(string message, string fileName, Exception innerException) : base(message, fileName, innerException)
        {
        }
    }
}
