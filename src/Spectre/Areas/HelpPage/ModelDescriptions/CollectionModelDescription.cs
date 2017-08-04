namespace Spectre.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Description of collection model
    /// </summary>
    /// <seealso cref="Spectre.Areas.HelpPage.ModelDescriptions.ModelDescription" />
    public class CollectionModelDescription : ModelDescription
    {
        /// <summary>
        /// Gets or sets the element description.
        /// </summary>
        /// <value>
        /// The element description.
        /// </value>
        public ModelDescription ElementDescription { get; set; }
    }
}