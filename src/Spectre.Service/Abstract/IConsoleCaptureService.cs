/*
 * IConsoleCaptureService.cs
 * Interface of service for capturing stdout.
 * 
   Copyright 2017 Michal Wolny, Grzegorz Mrukwa

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

namespace Spectre.Service.Abstract
{
    /// <summary>
    /// Captures stdout.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IConsoleCaptureService: IDisposable
    {
        /// <summary>
        /// Occurs when anything was written.
        /// </summary>
        event EventHandler<string> Written;

        /// <summary>
        /// Gets the captured content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        string Content { get; }
    }
}