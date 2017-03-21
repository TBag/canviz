using System;
using Xamarin.Forms;

namespace CustomerApp
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            ToolbarItem savebutton = new ToolbarItem
            {
                Text = "Save",
                Order = ToolbarItemOrder.Default,
                Priority = 0
            };
            savebutton.Clicked += SaveClicked;
            ToolbarItems.Add(savebutton);
            InitGridView();

            webApiURL.Text = Settings.MTCWebUrl;
            tenant.Text = Settings.Tenant;
            clientId.Text = Settings.ClientID;
            signUpSignInpolicy.Text = Settings.SignUpSignInpolicy;
            hockeyAppId.Text = Settings.HockeyAppId;
            gcmSenderId.Text = Settings.MobileGcmSenderId;
        }
        private void InitGridView()
        {
            if (settingsPageGrid.RowDefinitions.Count == 0)
            {
                settingsPageGrid.Padding = new Thickness(0, Display.Convert(40));
                Display.SetGridRowsHeight(settingsPageGrid, new string[] { "50", "10", "70", "50", "10", "70", "50", "10", "70", "50", "10", "70", "50","10", "70", "50", "10", "70", "1*" });
            }

        }
        public async void SaveClicked(object sender, EventArgs e)
        {
            string newUri = string.Empty;
            if (!GetHttpsUri(webApiURL.Text, out newUri))
            {
                await DisplayAlert("Configuration Error", "Invalid URI entered", "OK");
                return;
            }
            if (Settings.MTCWebUrl != newUri)
            {
                Settings.MTCWebUrl = newUri;
            }

            if(tenant.Text.Length == 0)
            {
                await DisplayAlert("Configuration Error", "Invalid Tenant entered", "OK");
                return;
            }
            if (Settings.Tenant != tenant.Text)
            {
                Settings.Tenant = tenant.Text;
            }

            if (clientId.Text.Length == 0)
            {
                await DisplayAlert("Configuration Error", "Invalid Client Id entered", "OK");
                return;
            }
            if (Settings.ClientID != clientId.Text)
            {
                Settings.ClientID = clientId.Text;
            }

            if (signUpSignInpolicy.Text.Length == 0)
            {
                await DisplayAlert("Configuration Error", "Invalid SignUpSignInpolicy entered", "OK");
                return;
            }
            if (Settings.SignUpSignInpolicy != signUpSignInpolicy.Text)
            {
                Settings.SignUpSignInpolicy = signUpSignInpolicy.Text;
            }

            if (hockeyAppId.Text.Length == 0)
            {
                await DisplayAlert("Configuration Error", "Invalid Hockey App Id entered", "OK");
                return;
            }
            if (Settings.HockeyAppId != hockeyAppId.Text)
            {
                Settings.HockeyAppId = hockeyAppId.Text;
                await DisplayAlert("Configuration Hint", "Restart the App to enable Hockey App.", "OK");
            }
            if(Device.OS == TargetPlatform.Android)
            {
                if (gcmSenderId.Text.Length == 0)
                {
                    await DisplayAlert("Configuration Error", "Invalid Google Cloud Messaging Id entered", "OK");
                    return;
                }
                if (Settings.MobileGcmSenderId != gcmSenderId.Text)
                {
                    Settings.MobileGcmSenderId = gcmSenderId.Text;
                    await DisplayAlert("Configuration Hint", "Restart the App to enable Google Cloud Messaging.", "OK");
                }
            }

            await Navigation.PopAsync(false);
        }

        private bool GetHttpsUri(string inputString, out string httpsUri)
        {
            if (!Uri.IsWellFormedUriString(inputString, UriKind.Absolute))
            {
                httpsUri = "";
                return false;
            }

            var uriBuilder = new UriBuilder(inputString)
            {
                Scheme = "https",
                Port = -1
            };

            httpsUri = uriBuilder.ToString();
            return true;
        }

    }
}
