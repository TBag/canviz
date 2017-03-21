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
        }

        private void OnPageSizeChanged(object sender, EventArgs args)
        {
            App.screenWidth = Width;
        }

        private void InitGridView()
        {
            if (mainPageGrid.RowDefinitions.Count == 0)
            {
                Display.SetGridRowsHeight(mainPageGrid, new string[] { "25*", "300", "40*", "90", "64" ,"32", "35*" });
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
                    App.PCApplication = new PublicClientApplication(Settings.Authority, Settings.ClientID);
                    App.PCApplication.PlatformParameters = App.PlatformParameters;

                    MobileServiceHelper.msInstance = new MobileServiceHelper();

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
