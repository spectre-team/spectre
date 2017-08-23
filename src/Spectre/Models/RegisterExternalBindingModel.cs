using System.ComponentModel.DataAnnotations;

namespace Spectre.Models
{
    /// <summary>
    /// External registration model
    /// </summary>
    public class RegisterExternalBindingModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
