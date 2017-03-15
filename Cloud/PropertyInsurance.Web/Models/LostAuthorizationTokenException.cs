using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyInsurance.Web.Models
{
    public class LostAuthorizationTokenException:Exception
    {
        public LostAuthorizationTokenException()
        {
        }

        public LostAuthorizationTokenException(string message)
        : base(message)
        {
        }

        public LostAuthorizationTokenException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}