using System.Runtime.Serialization;

namespace Spectre.Models.Msi
{
    /// <summary>
    /// Exhibits general preparation data to API.
    /// </summary>
    [DataContract]
    public class Preparation
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name { get; private set; }
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember]
        public int Id { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Preparation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        public Preparation(string name, int id)
        {
            Name = name;
            Id = id;
        }
    }
}