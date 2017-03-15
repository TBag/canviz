using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PropertyInsurance.API.App_Start;
using System;
using System.IdentityModel.Tokens;
using System.Web.Http;

namespace PropertyInsurance.API
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            new MobileAppConfiguration()
                .AddMobileAppHomeController()
                .MapApiControllers()
                .AddPushNotifications()
                .ApplyTo(config);

            app.UseOAuthBearerAuthentication(CreateBearerOptionsFromPolicy(Settings.SusiPolicyId));

            app.UseWebApi(config);
        }

        public static OAuthBearerAuthenticationOptions CreateBearerOptionsFromPolicy(string policy)
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

