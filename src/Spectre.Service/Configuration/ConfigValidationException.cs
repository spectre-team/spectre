/*
 * ConfigValidationException.cs
 * Exception thrown by config validation logic when settings are incorrect.
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
using System;

namespace Spectre.Service.Configuration
{
    /// <summary>
    /// Thrown by config validation logic when settings are incorrect.
    /// </summary>
    /// <seealso cref="System.ArgumentException" />
    public class ConfigValidationException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigValidationException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The original exception thrown during validation.</param>
        public ConfigValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
