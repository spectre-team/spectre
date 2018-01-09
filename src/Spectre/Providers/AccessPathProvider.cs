using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Abstractions;
using System.Linq;
using Spectre.Algorithms.Parameterization;
using Spectre.Algorithms.Results;
using Spectre.Data.Datasets;

namespace Spectre.Providers
{
    /// <summary>
    ///     Returns paths to data and results.
    /// </summary>
    public class AccessPathProvider
    {
        private readonly IFileSystem _fileSystem = new FileSystem();
        private readonly string _root = ConfigurationManager.AppSettings[name: "LocalDataDirectory"];

        private readonly Dictionary<Type, string> _typesRegister;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccessPathProvider" /> class.
        /// </summary>
        public AccessPathProvider()
        {
            _typesRegister = new Dictionary<Type, string>();
            _typesRegister[key: typeof(IDataset)] = "data.txt";
            _typesRegister[key: typeof(DivikResult)] = "divik.json";
            _typesRegister[key: typeof(DivikOptions)] = "divik_config.json";
        }

        /// <summary>
        ///     Gets the available IDs.
        /// </summary>
        /// <returns>List of available IDs.</returns>
        public IEnumerable<string> GetAvailableDatasets() => _fileSystem.Directory.EnumerateFileSystemEntries(_root)
            .Where(_fileSystem.Directory.Exists);

        /// <summary>
        ///     Gets the IDs of the available datasets.
        /// </summary>
        /// <returns>IDs</returns>
        public IEnumerable<int> GetAvailableIds() => GetAvailableDatasets()
            .Select(selector: name => name.GetHashCode());

        /// <summary>
        ///     Gets the path.
        /// </summary>
        /// <typeparam name="T">Type of the serialized object.</typeparam>
        /// <param name="id">The identifier of dataset.</param>
        /// <returns>
        ///     Path to the object
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown if type has not been registered or ID has not been found.</exception>
        public string GetPath<T>(int id)
        {
            var datasetName = GetAvailableDatasets()
                .First(predicate: name => name.GetHashCode() == id);
            if (!_typesRegister.ContainsKey(key: typeof(T)))
            {
                throw new InvalidOperationException(message: $"Unregistered type: {typeof(T).Name}");
            }
            var fileName = _typesRegister[key: typeof(T)];
            return _fileSystem.Path.Combine(_root, datasetName, fileName);
        }
    }
}
