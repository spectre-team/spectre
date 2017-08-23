using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Spectre.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Description of a parameter
    /// </summary>
    public class ParameterDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterDescription"/> class.
        /// </summary>
        public ParameterDescription()
        {
            Annotations = new Collection<ParameterAnnotation>();
        }

        /// <summary>
        /// Gets the annotations.
        /// </summary>
        /// <value>
        /// The annotations.
        /// </value>
        public Collection<ParameterAnnotation> Annotations { get; private set; }

        /// <summary>
        /// Gets or sets the documentation.
        /// </summary>
        /// <value>
        /// The documentation.
        /// </value>
        public string Documentation { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type description.
        /// </summary>
        /// <value>
        /// The type description.
        /// </value>
        public ModelDescription TypeDescription { get; set; }
    }
}