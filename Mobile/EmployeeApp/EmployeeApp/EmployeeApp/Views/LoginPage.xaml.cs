using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace EmployeeApp
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            SizeChanged += OnPageSizeChanged;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //set grid view height
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
                Display.SetGridRowsHeight(mainPageGrid, new string[] { "18*", "326", "72*", "90", "40", "90", "8*" });
            }

        }
        private async void loginButton_Clicked(object sender, EventArgs e)
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
                   
                    AuthenticationContext ac = new AuthenticationContext(Settings.Authority);

                    AuthenticationResult ar = await ac.AcquireTokenAsync(Settings.Resource,
                                                                        Settings.ClientID,
                                                                         new Uri(Settings.ReplyURL),
                                                                         App.PlatformParameters);
                    string token = ar.AccessToken;
                    Utils.TraceStatus("Login Success");
                   await Navigation.PushAsync(new MainPage(ar.UserInfo.DisplayableId, ac));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("An error has occurred", "Exception message: " + ex.Message, "Dismiss");
            }

        }
        private void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage(), false);
        }
    }
}
