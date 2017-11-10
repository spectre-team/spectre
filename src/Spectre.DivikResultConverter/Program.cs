/*
 * Program.cs
 * Runs conversion to Spectre format of DiviK algorithm results.
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
using System.IO;
using System.Linq;
using Spectre.Algorithms.Io;

namespace Spectre.DivikResultConverter
{
    /// <summary>
    /// Entry point class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine(value: $"Usage: {AppDomain.CurrentDomain.FriendlyName} path_to_result_file");
                return;
            }

            var inputPath = args.First();
            var outputPath = Path.ChangeExtension(inputPath, extension: "json");

            try
            {
                using (var loader = new DivikResultLoader())
                {
                    var tree = loader.Load(inputPath);
                    tree.Save(outputPath, indentation: false);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
