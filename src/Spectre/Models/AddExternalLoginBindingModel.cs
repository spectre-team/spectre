using System.ComponentModel.DataAnnotations;

namespace Spectre.Models
{
    // Models used as parameters to AccountController actions.

    /// <summary>
    /// Adds External Access Login
    /// </summary>
    public class AddExternalLoginBindingModel
    {
        /// <summary>
        /// Gets or sets the external access token.
        /// </summary>
        /// <value>
        /// The external access token.
        /// </value>
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}
