using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

namespace Spectre.Providers
{
    /// <summary>
    /// OAuth orivider
    /// </summary>
    /// <seealso cref="Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerProvider" />
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationOAuthProvider"/> class.
        /// </summary>
        /// <param name="publicClientId">The public client identifier.</param>
        /// <exception cref="System.ArgumentNullException">publicClientId</exception>
        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException(paramName: nameof(publicClientId));
            }

            _publicClientId = publicClientId;
        }

        /// <summary>
        /// Creates the properties.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Authentication properties</returns>
        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }

        /// <inheritdoc cref="OAuthAuthorizationServerProvider"/>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            var user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError(error: "invalid_grant", errorDescription: "The user name or password is incorrect.");
                return;
            }

#pragma warning disable SA1305 // Field names must not use Hungarian notation
            var oAuthIdentity = await user.GenerateUserIdentityAsync(
#pragma warning restore SA1305 // Field names must not use Hungarian notation
                userManager,
                OAuthDefaults.AuthenticationType);
            var cookiesIdentity = await user.GenerateUserIdentityAsync(
                userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            var properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
            var ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        /// <inheritdoc cref="OAuthAuthorizationServerProvider"/>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(result: null);
        }

        /// <inheritdoc cref="OAuthAuthorizationServerProvider"/>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(result: null);
        }

        /// <inheritdoc cref="OAuthAuthorizationServerProvider"/>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                var expectedRootUri = new Uri(context.Request.Uri, relativeUri: "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(result: null);
        }
    }
}
