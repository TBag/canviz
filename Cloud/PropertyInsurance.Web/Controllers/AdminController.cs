using PropertyInsurance.Web.Utils;
using System;
using System.Web.Mvc;

namespace PropertyInsurance.Web.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Consent()
        {
            var stateMarker = Guid.NewGuid().ToString();
            var nonce = Guid.NewGuid().ToString();
            var redirectUrl = new Uri(Request.Url, Url.Action("Consented"));

            var consentUrl = string.Format("{0}{1}/oauth2/authorize?response_type=id_token&client_id={2}&resource={3}&redirect_uri={4}&state={5}&nonce={6}&prompt=admin_consent",
                 Constants.AADInstance,
                 Constants.AADTenant,
                 Uri.EscapeDataString(Constants.AADClientId),
                 Uri.EscapeDataString(Constants.GraphResourceUrl),
                 Uri.EscapeDataString(redirectUrl.ToString()),
                 Uri.EscapeDataString(stateMarker),
                 Uri.EscapeDataString(nonce)
             );
            return new RedirectResult(consentUrl);
        }

        public ActionResult Consented()
        {
            return Content("Consented!");
        }
    }
}