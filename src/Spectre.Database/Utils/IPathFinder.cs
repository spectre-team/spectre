using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectre.Database.Utils
{
    /// <summary>
    /// Interface for the PathFinder class
    /// </summary>
    public interface IPathFinder
    {
        /// <summary>
        /// Returns for hash.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns>Hash</returns>
        string ReturnForHash(string hash);

        /// <summary>
        /// Returns the name of for friendly.
        /// </summary>
        /// <param name="friendlyname">The friendlyname.</param>
        /// <returns>Friendly Name</returns>
        string ReturnForFriendlyName(string friendlyname);
    }
}
