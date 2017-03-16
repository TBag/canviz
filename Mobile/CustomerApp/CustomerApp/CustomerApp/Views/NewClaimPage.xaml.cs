using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Net.Http;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace CustomerApp
{
    public partial class NewClaimPage : ContentPage
    {
        private MediaFile _mediaFile = null;
        private AuthenticationResult _authenticationResult;
        private string _dateFormatter = "MMMM dd, yyyy";
        private MainPage _parentPage;

        public NewClaimPage(AuthenticationResult result, MainPage parentpage)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);

            datePicker.DateSelected += DatePicker_DateSelected;
            datePicker.MaximumDate = DateTime.Now;
            datePicker.MinimumDate = new DateTime(2000, 1, 1);
            datePicker.Format = _dateFormatter;
            datePicker.IsVisible = false;

            newClaimDate.Text = datePicker.Date.ToString(_dateFormatter);

            _authenticationResult = result;
            _parentPage = parentpage;

            InitGridView();
        }
        private void InitGridView()
        {
            if (newClaimPageGrid.RowDefinitions.Count == 0)
            {
                Display.SetGridRowsHeight(newClaimPageGrid, new string[] { "30", "340", "700", "20", "90", "30", "1*" });
                Display.SetGridRowsHeight(cameraImageGrid, new string[] { "90", "90", "84", "1*" });
                Display.SetGridRowsHeight(tab2ContentGrid, new string[] { "16", "100", "30", "60", "4", "34", "50", "350", "50", "1*" });
                tab2Frame.Margin = new Thickness(0, Display.Convert(-40), 0, 0);
                claimDescriptionEditor.HeightRequest = Display.Convert(340);
            }

        }
        private async void OnSubmitClaimButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (_mediaFile != null) {
                    using (var scope = new ActivityIndicatorScope(activityIndicator, activityIndicatorPanel, true))
                    {
                        HttpResponseMessage response = await HttpUtil.PostImageAsync(_mediaFile, Settings.UploadImageUrl, _authenticationResult.Token);
                        if (response.IsSuccessStatusCode)
                        {
                            string responseURL = await response.Content.ReadAsStringAsync();
                            char[] charsToTrim = { '\"'};
                            responseURL = responseURL.Trim(charsToTrim);

                            Random rnd = new Random();
                            string claimName = rnd.Next(15000, 16000).ToString(); 
                            ClaimModel claim = new ClaimModel
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = claimName,
                                Status = "Submitted",
                                ClaimDateTime = this.datePicker.Date,
                                ClaimDescription = this.claimDescriptionEditor.Text,
                                ImageUrl = responseURL,
                                Tag=""
                            };

                            SubmitCaseRsp ret = await CallQueue(claim);
                            if (ret!=null &&  ret.result)
                            {
                                _parentPage.AddNewClaim(claim);
                                await DisplayAlert("Success", "Claim successfully submitted.", "Ok");
                                await Navigation.PopAsync();
                            }
                            else {
                                await DisplayAlert("Sorry", "The picture was rejected as it doesn’t show any property image. Please submit another picture.", "Ok");
                            }
                        }
                        else
                        {
                            // If the call failed with access denied, show the user an error indicating they might need to sign-in again.
                            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await DisplayAlert("Unauthorized", "Please login again, then new claim works well again", "Ok");
                                await Navigation.PopToRootAsync();
                            }
                            else {
                                await DisplayAlert("Error", response.StatusCode.ToString(), "Ok");
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.TraceException("Image upload failed", ex);
                await DisplayAlert("Image upload failed", "Image upload failed. Please try again later", "Ok");
            }
        }

        private async Task<SubmitCaseRsp> CallQueue(ClaimModel claim)
        {
            string json = JsonConvert.SerializeObject(claim);
            HttpResponseMessage response = await HttpUtil.PostJsonAsync(json, Settings.QueueUrl, _authenticationResult.Token);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = await response.Content.ReadAsStringAsync();
                char[] charsToTrim = { '\"'};
                responseStr = responseStr.Trim(charsToTrim).Replace("\\\"","\"");
                SubmitCaseRsp value = JsonConvert.DeserializeObject<SubmitCaseRsp>(responseStr);
                return value;
            }
            return null;
        }
        public void CalendarBtn_Tapped(object sender, EventArgs e) {

            Device.BeginInvokeOnMainThread(() => {
                if (datePicker.IsFocused)
                    datePicker.Unfocus();

                datePicker.Focus();
            });
        }
        public async void CameraBtn_Tapped(object sender, EventArgs e)
        {
            try
            {
                using (var scope = new ActivityIndicatorScope(activityIndicator, activityIndicatorPanel, true))
                {
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                       await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                        return;
                    }

                    MediaFile file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium
                    });

                    if (file == null)
                    return;

                    _mediaFile = file;

                    foreach (var child in cameraImageGrid.Children.Reverse())
                    {
                        cameraImageGrid.Children.Remove(child);
                    }
                    cameraImageGrid.RowDefinitions.Clear();
                    cameraImageGrid.ColumnDefinitions.Clear();
                    cameraImageGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                    cameraImageGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                    var claimImage = new Image
                    {
                        Aspect = Aspect.AspectFit,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    };
                    claimImage.Source = ImageSource.FromFile(file.Path);
                    cameraImageGrid.Children.Add(claimImage, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utils.TraceException("CameraBtn_Tapped ", ex);
                await DisplayAlert("Image upload failed", "Image upload failed. Please try again later", "Ok");
            }
        }


        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            this.newClaimDate.Text = datePicker.Date.ToString(_dateFormatter);
        }
    }
}
