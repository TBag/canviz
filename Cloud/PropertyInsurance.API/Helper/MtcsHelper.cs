using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PropertyInsurance.API.Helper
{
    public class MtcsHelper
    {
        public static async Task<JToken> GetMtcByCustomerEmail(string customerEmail)
        {
            HttpClient client = new HttpClient();
            
            JObject json = JObject.Parse(await client.GetStringAsync(Settings.MtcsjsonUrl));

            return json["mtcs"].ToList().Where(i => ((JObject)i)["customer"]["email"].ToString() == customerEmail).FirstOrDefault();
        }
    }
}