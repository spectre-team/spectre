/*
 * Program.cs
 * Entry point for DataValidatorApp.
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
using Spectre.Data.Datasets;

namespace Spectre.DataValidatorApp
{
    /// <summary>
    /// This application purpose is to validate input data files.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            if ((args.Length != 1) || ((args.Length == 2) && (args[1] != "--nonblocking")))
            {
                Console.Write(value: $"Usage: {AppDomain.CurrentDomain.FriendlyName} path_to_data_file [--nonblocking]");
            }
            else
            {
                try
                {
                    new BasicTextDataset(textFilePath: args[0]);
                    Console.WriteLine(value: "Validation success.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine(value: "Validation failure.");
                }
            }
            if (args.Length == 1)
            {
                Console.ReadLine();
            }
        }
    }
}
