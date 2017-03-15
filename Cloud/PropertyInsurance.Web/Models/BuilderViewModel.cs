using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyInsurance.Web.Models
{
    public enum Risk
    {
        High,
        Low
    }
    public class BuilderViewModel
    {
        public string Name { get; set; }
        public int TotalNoProperties { get; set;}
        public Risk Risk { get; set; }
    }
}