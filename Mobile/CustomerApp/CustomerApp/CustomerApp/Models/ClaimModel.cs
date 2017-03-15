using System;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace CustomerApp
{
    public class ClaimModel
    {

        public DateTime ClaimDateTime { set; get; }
        public string ClaimDescription { set; get; }
        public string ImageUrl { set; get; }
        public string Tag { set; get; }

        [JsonIgnore]
        public string Id { get; set; }

        [JsonIgnore]
        public string Name { set; get; }

        [JsonIgnore]
        public string Status { set; get; }

    }
    public class SubmitCaseRsp
    {
        public bool result { set; get; }
    }
}
