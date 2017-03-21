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
        public static PublicClientApplication PCApplication { get; set; }
        public static IPlatformParameters PlatformParameters { get; set; }

        public static double screenWidth = 360;

        public App()
        {
            InitializeComponent();
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
