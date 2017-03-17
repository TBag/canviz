using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyInsurance.Web.Utils
{
    public static class AuthorizationHelper
    {

        public static string GetUrl(string state)
        {
            var url = string.Format("{0}/{1}/oauth2/authorize?response_type=code&client_id={2}&resource={3}&redirect_uri={4}&state={5}&prompt=admin_consent",
                Constants.AADInstance,
                Constants.AADTenant,
                Uri.EscapeDataString(Constants.AADClientId),
                Uri.EscapeDataString(Constants.GraphResourceAADRootUrl),
                Uri.EscapeDataString(Constants.ConsentRedirectUrl),
                Uri.EscapeDataString(state)
            );
            return url;
        }
    }
}