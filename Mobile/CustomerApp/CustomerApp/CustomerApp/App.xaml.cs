using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Microsoft.Identity.Client;

namespace CustomerApp
{
    public partial class App : Application
    {
        // the app
        public static PublicClientApplication PCApplication { get; set; }

        // app coordinates
        public static string ClientID = "5c548cd0-a737-4ca4-a72a-f852e538ed6b";
        public static string[] Scopes = { ClientID };

        public static string SignUpSignInpolicy = "B2C_1_PropertyInsurance";
        public static string ResetPasswordpolicy = "b2c_1_reset";
        public static string Authority = "https://login.microsoftonline.com/CANVIZPropInsB2C.onmicrosoft.com/";

       // public static object UIContext { get; set; }

        public static double screenWidth = 360;


        public App()
        {
            InitializeComponent();

            PCApplication = new PublicClientApplication(Authority, ClientID);

            MobileServiceHelper.msInstance = new MobileServiceHelper();

            LoginPage loginPage = new LoginPage();
            //MainPage = new NavigationPage(new NewClaimPage(null, null));
            MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Color.FromHex("#2b3151"),
                BarTextColor = Color.White,
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
