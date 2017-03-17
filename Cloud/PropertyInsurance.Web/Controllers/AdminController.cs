using PropertyInsurance.Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropertyInsurance.Web.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Consent()
        {
            var stateMarker = Guid.NewGuid().ToString();
            var consentUrl = AuthorizationHelper.GetUrl(stateMarker);
            return new RedirectResult(consentUrl);
        }
    }
}