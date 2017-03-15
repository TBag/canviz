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

            SizeChanged += OnPageSizeChanged;

        }
        protected override async void OnAppearing()
        {
            try
            {
                // Look for existing user
                var result = await App.PCApplication.AcquireTokenSilentAsync(App.Scopes);
                await MobileServiceHelper.msInstance.InitMobileService(result);
                await Navigation.PushAsync(new MainPage(result));
            }
            catch(Exception ex)
            {
                // Do nothing - the user isn't logged in
                Utils.TraceException("LoginPage OnAppearing", ex);
            }
            base.OnAppearing();

            InitGridView();
        }

        private void OnPageSizeChanged(object sender, EventArgs args)
        {
            App.screenWidth = Width;
        }

        private void InitGridView()
        {
            if (mainPageGrid.RowDefinitions.Count == 0)
            {
                Display.SetGridRowsStarHeight(mainPageGrid, new int[] { 22 });
                Display.SetGridRowsHeight(mainPageGrid, new int[] { 300 });
                Display.SetGridRowsStarHeight(mainPageGrid, new int[] { 35 });
                Display.SetGridRowsHeight(mainPageGrid, new int[] { 90 });
                Display.SetGridRowsStarHeight(mainPageGrid, new int[] { 43 });
            }

        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            try
            {
                using (var scope = new ActivityIndicatorScope(activityIndicator, activityIndicatorPanel, true))
                {
                    var result = await App.PCApplication.AcquireTokenAsync(
                                App.Scopes,
                                string.Empty,
                                UiOptions.SelectAccount,
                                string.Empty,
                                null,
                                App.Authority,
                                App.SignUpSignInpolicy);
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
