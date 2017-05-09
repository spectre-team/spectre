using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Spectre.Models.Msi
{
    /// <summary>
    /// Provides details about single spectrum in the dataset.
    /// </summary>
    [DataContract]
    public class Spectrum
    {
        /// <summary>
        /// Gets or sets the identifier of the spectrum.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the m/z values.
        /// </summary>
        /// <value>
        /// The mz.
        /// </value>
        [DataMember]
        public IEnumerable<double> Mz { get; set; }
        /// <summary>
        /// Gets or sets the intensities for all m/z-s.
        /// </summary>
        /// <value>
        /// The intensities.
        /// </value>
        [DataMember]
        public IEnumerable<double> Intensities { get; set; }
        /// <summary>
        /// Gets or sets the x coordinate.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [DataMember]
        public int X { get; set; }
        /// <summary>
        /// Gets or sets the y coordinate.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [DataMember]
        public int Y { get; set; }
    }
}