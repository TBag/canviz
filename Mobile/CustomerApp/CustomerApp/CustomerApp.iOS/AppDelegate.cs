using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using HockeyApp.iOS;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace CustomerApp.iOS
{
    

    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private string HOCKEYAPP_APPID = Utils.MobileHockeyAppIdiOS;
        private static NSData DeviceToken { get;  set; }
        public static bool IsAfterInitClient = false;
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            try
            {
                var manager = BITHockeyManager.SharedHockeyManager;
                manager.Configure(HOCKEYAPP_APPID);
                manager.DisableUpdateManager = true;
                manager.StartManager();
                //manager.Authenticator.AuthenticateInstallation(); // This line is obsolete in crash only builds
            }
            catch (Exception e)
            {
            }

            var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert
                                                              | UIUserNotificationType.Badge
                                                              | UIUserNotificationType.Sound,
                                                              new NSSet());

            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            var result = base.FinishedLaunching(app, options);
            App.PCApplication.PlatformParameters = new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController);
            return result;
        }

        public static async Task RegisterWithMobilePushNotifications()
        {
            if (DeviceToken != null)
            {

                var apnsBody = new JObject {
                    {
                        "aps",
                        new JObject {
                            { "alert", "$(Message)" }
                        }
                    }
                };

                var template = new JObject {
                    {
                        "genericMessage",
                        new JObject {
                            {"body", apnsBody}
                        }
                    }
                };

                try
                {
                    var push = MobileServiceHelper.msInstance.Client.GetPush();
                    await push.RegisterAsync(DeviceToken, template);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception in RegisterWithMobilePushNotifications: " + ex.Message);

                    UIAlertView avAlert1 = new UIAlertView("Notification", "Exception in RegisterWithMobilePushNotifications", null, "OK", null);
                    avAlert1.Show();
                }
            }
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;

            string alert = string.Empty;
            if (aps.ContainsKey(new NSString("alert")))
                alert = (aps[new NSString("alert")] as NSString).ToString();

            //show alert
            if (!string.IsNullOrEmpty(alert))
            {
                UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
                avAlert.Show();
            }
        }

        public override async void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            DeviceToken = deviceToken;
            if (IsAfterInitClient)
                await RegisterWithMobilePushNotifications();
        }
    }
}
