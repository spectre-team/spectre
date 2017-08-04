using Microsoft.AspNet.Identity.EntityFramework;

namespace Spectre.Models
{
    /// <summary>
    /// Context for the application, providing users
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext{T}" />
    /// <seealso cref="Spectre.Models.ApplicationUser"/>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        public ApplicationDbContext()
            : base(nameOrConnectionString: "DefaultConnection", throwIfV1Schema: false) { }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>The context</returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
