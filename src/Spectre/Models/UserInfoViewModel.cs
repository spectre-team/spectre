namespace Spectre.Models
{
    /// <summary>
    /// VM for user info
    /// </summary>
    public class UserInfoViewModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has registered.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has registered; otherwise, <c>false</c>.
        /// </value>
        public bool HasRegistered { get; set; }

        /// <summary>
        /// Gets or sets the login provider.
        /// </summary>
        /// <value>
        /// The login provider.
        /// </value>
        public string LoginProvider { get; set; }
    }
}
