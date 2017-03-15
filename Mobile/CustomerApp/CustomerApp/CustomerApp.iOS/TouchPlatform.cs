using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.Threading.Tasks;
using System.Reflection;

[assembly: Xamarin.Forms.Dependency(typeof(CustomerApp.iOS.TouchPlatform))]
namespace CustomerApp.iOS
{
    class TouchPlatform: IPlatform
    {
        //public string GetCurrentVersion()
        //{
        //    return Assembly.GetCallingAssembly().FullName;
        //}
        public async Task RegisterWithMobilePushNotifications()
        {
            AppDelegate.IsAfterInitClient = true;
            await AppDelegate.RegisterWithMobilePushNotifications();
        }
    }
}