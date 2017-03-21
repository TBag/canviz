using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmployeeApp
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

            claimImageContainerURL.Text = Settings.ClaimImageContainerURL;
            tenant.Text = Settings.Tenant;
            clientId.Text = Settings.ClientID;
            replyURL.Text = Settings.ReplyURL;
            if(Device.OS == TargetPlatform.iOS)
            {
                hockeyAppId.Text = Settings.HockeyAppIdiOS;
            }
            else
            {
                hockeyAppId.Text = Settings.HockeyAppIdAndroid;
            }
        }

        private void InitGridView()
        {
            if (settingsPageGrid.RowDefinitions.Count == 0)
            {
                settingsPageGrid.Padding = new Thickness(0, Display.Convert(40));
                Display.SetGridRowsHeight(settingsPageGrid, new string[] { "50", "10", "70", "50", "10", "70", "50", "10", "70", "50", "10", "70", "50", "10", "70", "1*" });
            }

        }
        public async void SaveClicked(object sender, EventArgs e)
        {
            string containerUri = string.Empty;
            if (!GetHttpsUri(claimImageContainerURL.Text, out containerUri))
            {
                await DisplayAlert("Configuration Error", "Invalid Claim Image Container URI entered", "OK");
                return;
            }
            if (Settings.ClaimImageContainerURL != containerUri)
            {
                Settings.ClaimImageContainerURL = containerUri;
            }

            if (tenant.Text.Length == 0)
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

            string replyUri = string.Empty;
            if (!GetHttpsUri(replyURL.Text, out replyUri))
            {
                await DisplayAlert("Configuration Error", "Invalid Reply URI entered", "OK");
                return;
            }
            if (Settings.ReplyURL != replyUri)
            {
                Settings.ReplyURL = replyUri;
            }

            if (hockeyAppId.Text.Length == 0)
            {
                await DisplayAlert("Configuration Error", "Invalid Hockey App Id entered", "OK");
                return;
            }
            if(Device.OS == TargetPlatform.iOS)
            {
                if (Settings.HockeyAppIdiOS != hockeyAppId.Text)
                {
                    Settings.HockeyAppIdiOS = hockeyAppId.Text;
                    await DisplayAlert("Configuration Hint", "Restart the App to enable Hockey App.", "OK");
                }
            }
            else
            {
                if (Settings.HockeyAppIdAndroid != hockeyAppId.Text)
                {
                    Settings.HockeyAppIdAndroid = hockeyAppId.Text;
                    await DisplayAlert("Configuration Hint", "Restart the App to enable Hockey App.", "OK");
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
