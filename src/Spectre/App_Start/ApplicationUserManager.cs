using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Spectre.Models;

namespace Spectre
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    /// <summary>
    /// Manages application users
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.Identity.UserManager{T}" />
    /// <seealso cref="Spectre.Models.ApplicationUser"/>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserManager"/> class.
        /// </summary>
        /// <param name="store">User store</param>
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store) { }

        /// <summary>
        /// Creates the user manager from specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <returns>User manager</returns>
        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager =
                new ApplicationUserManager(
                    store: new UserStore<ApplicationUser>(context: context.Get<ApplicationDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(
                        protector: dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
