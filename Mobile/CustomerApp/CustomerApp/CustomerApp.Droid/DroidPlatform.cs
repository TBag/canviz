using System;
using Android.Content;
using System.Threading.Tasks;
using System.Reflection;


[assembly: Xamarin.Forms.Dependency(typeof(CustomerApp.Droid.DroidPlatform))]
namespace CustomerApp.Droid
{
    public class DroidPlatform : IPlatform
    {
        //public Task<string> GetCurrentVersion() {
        //    return Assembly.GetCallingAssembly().Version.ToString();
        //}
        public async Task RegisterWithMobilePushNotifications()
        {
            await GcmService.RegisterWithMobilePushNotifications();
        }
    }
}