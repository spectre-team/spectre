namespace Spectre.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Spectre.Database.Contexts;
    using Spectre.Database.Utils;
    using Spectre.Service.Abstract;

    /// <summary>
    /// PathFinder service.
    /// </summary>
    /// <seealso cref="Spectre.Service.Abstract.IFinderService" />
    internal class FinderService : IFinderService
    {
        /// <summary>
        /// The context
        /// </summary>
        private static DatasetsContext _context;

        /// <summary>
        /// The service
        /// </summary>
        private readonly Finder _service = new Finder(FinderService._context);

        /// <summary>
        /// Initializes a new instance of the <see cref="FinderService" /> class.
        /// </summary>
        public FinderService()
        {
            FinderService._context = new DatasetsContext();
        }

        /// <summary>
        /// Finds the hash.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns>
        /// Friendly name basing on hash
        /// </returns>
        public string FindHash(string hash)
        {
            return _service.ReturnForHash(hash);
        }

        /// <summary>
        /// Finds the friendly name.
        /// </summary>
        /// <param name="friendlyname">The friendlyname.</param>
        /// <returns>
        /// Hash basing on riendly name
        /// </returns>
        public string FindFriendlyName(string friendlyname)
        {
            return _service.ReturnForFriendlyName(friendlyname);
        }
    }
}
