using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using HockeyApp.iOS;

namespace EmployeeApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private string HOCKEYAPP_APPID = Utils.MobileHockeyAppIdiOS;
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

            var result = base.FinishedLaunching(app, options);
             App.PlatformParameters = new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController);

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

            return result;
        }
    }
}
