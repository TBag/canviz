using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using PropertyInsurance.Web.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PropertyInsurance.Web.Utils
{
    public class AuthenticationHelper
    {
        public static string token;
        public static async Task<string> AcquireTokenAsync()
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new LostAuthorizationTokenException(Constants.LostTokenErrorMsg);
            }
            return token;
        }

        /// <summary>
        ///     Get Active Directory Client for Application.
        /// </summary>
        /// <returns>ActiveDirectoryClient for Application.</returns>
        public static ActiveDirectoryClient GetActiveDirectoryClient()
        {
            Uri baseServiceUri = new Uri(Constants.ResourceUrl);
            ActiveDirectoryClient activeDirectoryClient =
                new ActiveDirectoryClient(new Uri(baseServiceUri, Constants.AADTenantId),
                    async () => await AcquireTokenAsync());
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

        public static Uri GetWebRootUrl()
        {
            return new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path));
        }
    }
}