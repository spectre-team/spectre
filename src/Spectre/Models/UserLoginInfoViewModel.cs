namespace Spectre.Models
{
    /// <summary>
    /// VM for User login info
    /// </summary>
    public class UserLoginInfoViewModel
    {
        /// <summary>
        /// Gets or sets the login provider.
        /// </summary>
        /// <value>
        /// The login provider.
        /// </value>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the provider key.
        /// </summary>
        /// <value>
        /// The provider key.
        /// </value>
        public string ProviderKey { get; set; }
    }
}
