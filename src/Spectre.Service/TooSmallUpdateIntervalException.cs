using System;

namespace Spectre.Service
{
    /// <summary>
    /// Thrown when update interval is unreasonably small, to avoid congestion.
    /// </summary>
    /// <seealso cref="System.ArgumentOutOfRangeException" />
    public class TooSmallUpdateIntervalException: ArgumentOutOfRangeException
    {
        /// <summary>
        /// Gets the update interval.
        /// </summary>
        /// <value>
        /// The update interval.
        /// </value>
        public double UpdateInterval { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooSmallUpdateIntervalException"/> class.
        /// </summary>
        /// <param name="updateInterval">The update interval.</param>
        public TooSmallUpdateIntervalException(double updateInterval)
        {
            UpdateInterval = updateInterval;
        }
    }
}
