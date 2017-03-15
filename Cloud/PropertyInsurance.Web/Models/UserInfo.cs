using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyInsurance.Web.Models
{
    public class UserInfo
    {
        public string Id { get; set; }

        public string GivenName { get; set; }

        public string Surname { get; set; }

        public string Mail { get; set; }

        public string UserPrincipalName { get; set; }

    }
}