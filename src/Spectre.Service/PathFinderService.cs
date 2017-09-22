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
    /// <seealso cref="Spectre.Service.Abstract.IPathFinderService" />
    internal class PathFinderService : IPathFinderService
    {
        /// <summary>
        /// The context
        /// </summary>
        private static Context _context;

        /// <summary>
        /// The service
        /// </summary>
        private readonly PathFinder _service = new PathFinder(PathFinderService._context);

        /// <summary>
        /// Initializes a new instance of the <see cref="PathFinderService" /> class.
        /// </summary>
        public PathFinderService()
        {
            PathFinderService._context = new Context();
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
