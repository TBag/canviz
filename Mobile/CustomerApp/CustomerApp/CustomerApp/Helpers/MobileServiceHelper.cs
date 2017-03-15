using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Microsoft.Identity.Client;
using Newtonsoft.Json;


namespace CustomerApp
{
    public class MobileServiceHelper
    {
        public static MobileServiceHelper msInstance;
        public MobileServiceClient Client;
        private string _mobileServiceClient = "https://propertyinsuranceapi.azurewebsites.net/";

        internal async  Task<string> InitMobileService(AuthenticationResult result)
        {

            Client = new MobileServiceClient(_mobileServiceClient);
            IPlatform platform = DependencyService.Get<IPlatform>();

            await platform.RegisterWithMobilePushNotifications();

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://propertyinsuranceapi.azurewebsites.net/api/HubTag?InstallationId=" + Client.InstallationId);

            request.Headers.Add("ZUMO-API-VERSION", "2.0.0");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return "ok " + Client.InstallationId;
            }
            else
            {
                return Client.InstallationId;
            }

                
        }
    }
}
