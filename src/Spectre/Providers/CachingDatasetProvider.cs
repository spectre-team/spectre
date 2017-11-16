using System.Collections.Generic;
using System.Linq;
using Spectre.Data.Datasets;

namespace Spectre.Providers
{
    /// <summary>
    ///     Provides Dataset from the given path, caching in memory, when possible.
    /// </summary>
    public class CachingDatasetProvider
    {
        private const int LimitOfCachedDatasets = 10;

        private static readonly Dictionary<string, IDataset> Datasets = new Dictionary<string, IDataset>();

        private static readonly List<string> LastUsages = new List<string>();

        /// <summary>
        ///     Reads the specified path or queries the cache.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Dataset from the file or cache.</returns>
        public IDataset Read(string path)
        {
            if (!CachingDatasetProvider.Datasets.ContainsKey(path))
            {
                if (CachingDatasetProvider.LastUsages.Count == CachingDatasetProvider.LimitOfCachedDatasets)
                {
                    var removed = CachingDatasetProvider.LastUsages.First();
                    CachingDatasetProvider.LastUsages.Remove(path);
                    CachingDatasetProvider.Datasets.Remove(removed);
                }
                CachingDatasetProvider.Datasets[path] = new BasicTextDataset(path);
            }
            if (CachingDatasetProvider.LastUsages.Contains(path))
            {
                CachingDatasetProvider.LastUsages.Remove(path);
            }
            CachingDatasetProvider.LastUsages.Add(path);
            return CachingDatasetProvider.Datasets[path];
        }
    }
}
