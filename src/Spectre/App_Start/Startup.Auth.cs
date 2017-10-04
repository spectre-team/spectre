using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Spectre.Models;
using Spectre.Providers;

namespace Spectre
{
    /// <summary>
    ///     Application startup
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Gets the authentication options.
        /// </summary>
        /// <value>
        /// The authentication options.
        /// </value>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        /// <summary>
        /// Gets the public client identifier.
        /// </summary>
        /// <value>
        /// The public client identifier.
        /// </value>
        public static string PublicClientId { get; private set; }

        /// <summary>
        /// Configures the authentication.
        /// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        /// </summary>
        /// <param name="app">The application.</param>
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(options: new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            Startup.PublicClientId = "self";
            Startup.OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString(value: "/Token"),
                Provider = new ApplicationOAuthProvider(Startup.PublicClientId),
                AuthorizeEndpointPath = new PathString(value: "/account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(value: 14),

                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(Startup.OAuthOptions);

            //// Uncomment the following lines to enable logging in with third party login providers
            ////app.UseMicrosoftAccountAuthentication(
            ////    clientId: "",
            ////    clientSecret: "");

            ////app.UseTwitterAuthentication(
            ////    consumerKey: "",
            ////    consumerSecret: "");

            ////app.UseFacebookAuthentication(
            ////    appId: "",
            ////    appSecret: "");

            ////app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            ////{
            ////    ClientId = "",
            ////    ClientSecret = ""
            ////});
        }
    }
}
