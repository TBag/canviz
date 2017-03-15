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
                Display.SetGridRowsStarHeight(mainPageGrid, new int[] { 18 });
                Display.SetGridRowsHeight(mainPageGrid, new int[] { 326 });
                Display.SetGridRowsStarHeight(mainPageGrid, new int[] { 72 });
                Display.SetGridRowsHeight(mainPageGrid, new int[] { 90 });
                Display.SetGridRowsStarHeight(mainPageGrid, new int[] { 8 });
            }

        }
        private async void loginButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (var scope = new ActivityIndicatorScope(activityIndicator, activityIndicatorPanel, true))
                {
                   
                    AuthenticationContext ac = new AuthenticationContext("https://login.microsoftonline.com/CANVIZPropInsB2C.onmicrosoft.com");

                    AuthenticationResult ar = await ac.AcquireTokenAsync("https://graph.windows.net",
                                                                        "d13ff0d8-b8d7-4266-bfcc-77faa1182783",
                                                                         new Uri("https://EmployeeMobileApp"),
                                                                         App.PlatformParameters);
                    string token = ar.AccessToken;
                    Utils.TraceStatus("Login Success");

                    //Helpers helpers = new Helpers();
                    //string imageUrl =await helpers.GetUserClaimURL(ar.UserInfo.DisplayableId);
                   await Navigation.PushAsync(new MainPage(ar.UserInfo.DisplayableId, ac));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("An error has occurred", "Exception message: " + ex.Message, "Dismiss");
            }

        }
    }
}
