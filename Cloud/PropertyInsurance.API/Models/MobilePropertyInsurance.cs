using System;

namespace PropertyInsurance.API.Models
{
    public class MobilePropertyInsurance
    {
        public string ImageUrl { get; set; }

        public string ClaimDescription { get; set; }

        public DateTime ClaimDateTime { get; set; }

        public string Tag { get; set; }
    }
}