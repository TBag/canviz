using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using PropertyInsurance.Web.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PropertyInsurance.Web.Utils
{
    public class AuthenticationHelper
    {

        private const string tokenSessionKey = "AADToken";

        private static string GetSessionTokenKey(string userId)
        {
            return "UserToken_" + userId;
        }

        public static void SetToken(string userId,string token)
        {
            var tokenSessionKey = GetSessionTokenKey(userId);
            HttpContext.Current.Session[tokenSessionKey] = token;
        }

        private static string GetToken()
        {
            var token = string.Empty;
            var signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(signedInUserID))
                throw new LostAuthorizationTokenException(Constants.LostTokenErrorMsg);
            var storedToken = HttpContext.Current.Session[GetSessionTokenKey(signedInUserID)];
            if (storedToken != null)
                token = storedToken.ToString();
            return token;
        }

        /// <summary>
        ///     Get Active Directory Client for Application.
        /// </summary>
        /// <returns>ActiveDirectoryClient for Application.</returns>
        public static ActiveDirectoryClient GetActiveDirectoryClient()
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token))
                throw new LostAuthorizationTokenException(Constants.LostTokenErrorMsg);
            Uri baseServiceUri = new Uri(Constants.GraphResourceUrl);
            ActiveDirectoryClient activeDirectoryClient =
                new ActiveDirectoryClient(new Uri(baseServiceUri, Constants.AADTenantId),
                    async () => token);
            return activeDirectoryClient;
        }

        public static void RemoveAllCookies(HttpRequest request, HttpResponse response)
        {
            string[] myCookies = request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
        }

        public static void Relogin(HttpRequest request,HttpResponse response)
        {
            RemoveAllCookies(request, response);
            //return back to homepage
            HttpContext httpContext = HttpContext.Current;
            httpContext.Response.Redirect("~/home/index");
        }        
    }
}