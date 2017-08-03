using System.ComponentModel.DataAnnotations;

namespace Spectre.Models
{
    /// <summary>
    /// Model for binding removal
    /// </summary>
    public class RemoveLoginBindingModel
    {
        /// <summary>
        /// Gets or sets the login provider.
        /// </summary>
        /// <value>
        /// The login provider.
        /// </value>
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the provider key.
        /// </summary>
        /// <value>
        /// The provider key.
        /// </value>
        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }
}
