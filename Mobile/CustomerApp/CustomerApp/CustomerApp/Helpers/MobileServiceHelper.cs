using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using System.Net.Http;
using Microsoft.Identity.Client;

namespace CustomerApp
{
    public class MobileServiceHelper
    {
        public static MobileServiceHelper msInstance;
        public MobileServiceClient Client;

        internal async  Task InitMobileService(AuthenticationResult result)
        {
            Client = new MobileServiceClient(Settings.MTCWebUrl);
            IPlatform platform = DependencyService.Get<IPlatform>();
            await platform.RegisterWithMobilePushNotifications();

            HttpResponseMessage response = await HttpUtil.PostAsync(Settings.HubTagUrl + Client.InstallationId, result.Token);
            if (!response.IsSuccessStatusCode)
            {
                Utils.TraceStatus("InitMobileService Post Failure "+ response.StatusCode);
            }
        }
    }
}
