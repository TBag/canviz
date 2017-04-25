using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security;
using PropertyInsurance.Web.Utils;

namespace PropertyInsurance.Web.Controllers
{
    public class AccountController : Controller
    {
        public void SignIn()
        {
            // Send an OpenID Connect sign-in request.
            if (!Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        public ActionResult SignOut()
        {
            string callbackUrl = Url.Action("SignOutCallback", "Account", routeValues: null, protocol: Request.Url.Scheme);
            AuthenticationHelper.RemoveAllCookies(System.Web.HttpContext.Current.Request, System.Web.HttpContext.Current.Response);
            return RedirectToAction("SignOutCallback");
            //HttpContext.GetOwinContext().Authentication.SignOut(
            //    new AuthenticationProperties { RedirectUri = callbackUrl },
            //    OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
        }

        public ActionResult SignOutCallback()
        {
            //if (Request.IsAuthenticated)
            //{
            //    // Redirect to home page if the user is authenticated.
            //    return RedirectToAction("Index", "Home");
            //}
            ViewBag.SignedOut = true;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
