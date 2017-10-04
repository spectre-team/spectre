using System;

namespace Spectre.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Annotation of a parameter
    /// </summary>
    public class ParameterAnnotation
    {
        /// <summary>
        /// Gets or sets the annotation attribute.
        /// </summary>
        /// <value>
        /// The annotation attribute.
        /// </value>
        public Attribute AnnotationAttribute { get; set; }

        /// <summary>
        /// Gets or sets the documentation.
        /// </summary>
        /// <value>
        /// The documentation.
        /// </value>
        public string Documentation { get; set; }
    }
}