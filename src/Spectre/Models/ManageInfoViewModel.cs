using System.Collections.Generic;

namespace Spectre.Models
{
    /// <summary>
    /// VM for info management
    /// </summary>
    public class ManageInfoViewModel
    {
        /// <summary>
        /// Gets or sets the local login provider.
        /// </summary>
        /// <value>
        /// The local login provider.
        /// </value>
        public string LocalLoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the logins.
        /// </summary>
        /// <value>
        /// The logins.
        /// </value>
        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        /// <summary>
        /// Gets or sets the external login providers.
        /// </summary>
        /// <value>
        /// The external login providers.
        /// </value>
        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }
}
