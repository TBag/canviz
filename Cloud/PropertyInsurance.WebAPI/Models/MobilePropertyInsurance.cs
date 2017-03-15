using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyInsurance.WebAPI.Models
{
    public class MobilePropertyInsurance
    {
        public string ImageUrl { get; set; }

        public string ClaimDescription { get; set; }

        public DateTime ClaimDateTime { get; set; }
    }
}