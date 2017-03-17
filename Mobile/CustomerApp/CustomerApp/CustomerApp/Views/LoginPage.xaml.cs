using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace CustomerApp
{
    public partial class LoginPage : ContentPage
    {
        public IPlatformParameters platformParameters { get; set; }
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            if(Device.OS == TargetPlatform.iOS)
            {
                InitGridView();
            }
            SizeChanged += OnPageSizeChanged;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.OS == TargetPlatform.Android){
                InitGridView();
            }

            //try
            //{
            //    // Look for existing user
            //    var result = await App.PCApplication.AcquireTokenSilentAsync(Settings.Scopes);
            //    await MobileServiceHelper.msInstance.InitMobileService(result);
            //    if (Settings.MTCWebUrl.Length > 0){
            //        await Navigation.PushAsync(new MainPage(result));
            //    }
            //    else {
            //        //await DisplayAlert("Configuration Error", "Invalid URI entered", "OK");
            //    }
                
            //}
            //catch(Exception ex)
            //{
            //    // Do nothing - the user isn't logged in
            //    Utils.TraceException("LoginPage OnAppearing", ex);
            //}

        }

        private void OnPageSizeChanged(object sender, EventArgs args)
        {
            App.screenWidth = Width;
        }

        private void InitGridView()
        {
            if (mainPageGrid.RowDefinitions.Count == 0)
            {
                Display.SetGridRowsHeight(mainPageGrid, new string[] { "25*", "300", "40*", "90", "40" ,"90", "35*" });
            }

        }

        private void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage(), false);
        }
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (!Settings.CheckAllConfigure())
            {
                await DisplayAlert("Configuration", "Please go to settings page and configure, then Sigin In again.", "OK");
                return;
            }
            try
            {
                using (var scope = new ActivityIndicatorScope(activityIndicator, activityIndicatorPanel, true))
                {
                    var result = await App.PCApplication.AcquireTokenAsync(
                                new string[]{ Settings.ClientID },
                                string.Empty,
                                UiOptions.SelectAccount,
                                string.Empty,
                                null,
                                Settings.Authority,
                                Settings.SignUpSignInpolicy);
                    await MobileServiceHelper.msInstance.InitMobileService(result);
                    await Navigation.PushAsync(new MainPage(result));
                }
            }
            catch (MsalException ex)
            {
                if (ex.Message != null && ex.Message.Contains("AADB2C90118"))
                {
                    //await OnForgotPassword();
                    Utils.TraceStatus("Login Failure, ForgotPassword?");
                }
                if (ex.ErrorCode != "authentication_canceled")
                {
                    await DisplayAlert("An error has occurred", "Exception message: " + ex.Message, "Dismiss");
                }
            }
        }
    }
}
