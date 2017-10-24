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
