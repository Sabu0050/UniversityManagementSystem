using OpenIddict.Client;
using System.Runtime.CompilerServices;
using UniversityManagementSystem.DLL.DbContext;

namespace UniversityManagementSystem.API.StratupExtension
{
    public static class OAuth2ExtensionHelper
    {
        public static IServiceCollection AddOAuth2ExtensionHelper(this IServiceCollection services) {
            services.AddOpenIddict()

            // Register the OpenIddict core components.
            .AddCore(options =>
            {
                // Configure OpenIddict to use the Entity Framework Core stores and models.
                // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
                options.UseEntityFrameworkCore()
                       .UseDbContext<ApplicationDbContext>()
                       .ReplaceDefaultEntities<int>(); ;
            })

            // Register the OpenIddict server components.
            .AddServer(options =>
            {
                // Enable the token endpoint.
                options.SetTokenEndpointUris("connect/token");

                // Enable the password flow.
                options.AllowPasswordFlow();

                // Accept anonymous clients (i.e clients that don't send a client_id).
                options.AcceptAnonymousClients();

                // Register the signing and encryption credentials.
                options.AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate();

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                       .EnableTokenEndpointPassthrough();
            })
            .AddClient(options=>{
                // Allow grant_type=password to be negotiated.
                options.AllowPasswordFlow();

                // Disable token storage, which is not necessary for non-interactive flows like
                // grant_type=password, grant_type=client_credentials or grant_type=refresh_token.
                options.DisableTokenStorage();

                // Register the System.Net.Http integration and use the identity of the current
                // assembly as a more specific user agent, which can be useful when dealing with
                // providers that use the user agent as a way to throttle requests (e.g Reddit).
                options.UseSystemNetHttp()
                       .SetProductInformation(typeof(Program).Assembly);

                // Add a client registration without a client identifier/secret attached.
                options.AddRegistration(new OpenIddictClientRegistration
                {
                    Issuer = new Uri("https://localhost:44360/", UriKind.Absolute)
                });
            })
                // Register the OpenIddict validation components.
            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });
            return services;
        }
    }
}
