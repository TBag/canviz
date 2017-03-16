using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyInsurance.Web.Models
{
    public class ClaimViewModel
    {
        public int Id { get; set;}
        public int RatePercent { get; set; }
        public string Payout { get; set; }
        public string Status { get; set;}
        public string Date { get; set;}
        public string Description { get; set; }

        public PolicyHolder PolicyHolder { get; set; }
    }

    public class PolicyHolder
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string RegisterDate { get; set; }
        public List<string> ImagesInventroy { get; set; }
    }

    public class MTCViewModel
    {

    }
}