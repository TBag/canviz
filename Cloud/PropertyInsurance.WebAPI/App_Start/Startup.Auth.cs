using System;
using System.IdentityModel.Tokens;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Jwt;
using PropertyInsurance.WebAPI.App_Start;

namespace PropertyInsurance.WebAPI
{
    public partial class Startup
    {
        // B2C policy identifiers
        public static string SusiPolicyId = Settings.SusiPolicyId;

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(CreateBearerOptionsFromPolicy(SusiPolicyId));
        }

        public OAuthBearerAuthenticationOptions CreateBearerOptionsFromPolicy(string policy)
        {
            TokenValidationParameters tvps = new TokenValidationParameters
            {
                // This is where you specify that your API only accepts tokens from its own clients
                ValidAudience = Settings.ClientId,
                AuthenticationType = policy,
            };

            return new OAuthBearerAuthenticationOptions
            {
                // This SecurityTokenProvider fetches the Azure AD B2C metadata & signing keys from the OpenIDConnect metadata endpoint
                AccessTokenFormat = new JwtFormat(tvps, new OpenIdConnectCachingSecurityTokenProvider(String.Format(Settings.AadInstance, Settings.Tenant, policy))),
            };
        }
    }
}
