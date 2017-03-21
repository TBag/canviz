using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace EmployeeApp
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<ClaimViewModel> _claimListViewSource = new ObservableCollection<ClaimViewModel>();
        public AuthenticationContext authenticationContext = null;

        public MainPage(string userid, AuthenticationContext ac)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            InitClaimList();
            listView.ItemTapped += async (s, e) => {
                listView.SelectedItem = null;//clear android select color
                ClaimViewModel vmodel = (ClaimViewModel)e.Item;
                if (vmodel.Status == ClaimStatus.Pending && userid.Length > 0)
                {
                    using (var scope = new ActivityIndicatorScope(activityIndicator, activityIndicatorPanel, true))
                    {
                        string imageUrl = await Helpers.GetUserClaimURL(userid);
                        vmodel.ImageUrl = imageUrl;
                    }
                }
                await Navigation.PushAsync(new ClaimDetailPage(vmodel));
            };

            authenticationContext = ac;
            InitGridView();
        }

        private void InitGridView() {
            if(mainPageGrid.RowDefinitions.Count == 0)
            {
                listView.SeparatorVisibility = SeparatorVisibility.None;
                listView.ItemsSource = _claimListViewSource;
                listView.ItemTemplate = new DataTemplate(typeof(ClaimCell));
                listView.RowHeight = Display.Convert(96);

                //android has status bar
                Display.SetGridRowHeightByDevice(mainPageGrid, 30, 1);
                Display.SetGridRowsHeight(mainPageGrid, new string[] { "126", "570", "40", "134", "40", "134", "40", "1*" });

                claimListGrid.Padding = new Thickness(Display.Convert(52), Display.Convert(12),
                    Display.Convert(52), Display.Convert(40));

                Display.SetGridRowsHeight(claimListGrid, new string[] { "100", "1*" });
            }

        }
        private void InitClaimList()
        {
            _claimListViewSource.Add(
                new ClaimViewModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "1201707",
                    Status = ClaimStatus.Pending,
                    ClaimDateTime = DateTime.Now,
                    isNew = true,
                    ImageUrl = "demo1.png",
                    ClaimDescription = "Our city was hit by an intense rainstorm on Sunday afternoon and our drainage pipe backed up. As a result, over a foot of water flooded our home, causing extensive damage to the floors as well as some furniture."
                });
            _claimListViewSource.Add(new ClaimViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "1141698",
                Status = ClaimStatus.Approved,
                ImageUrl = "demo1.png",
                ClaimDateTime = DateTime.Now,
                ClaimDescription = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor manuris condimentum nibh, ut fermentum massa justo sit amet risus."
            });
            _claimListViewSource.Add(new ClaimViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "1021710",
                Status = ClaimStatus.Approved,
                ImageUrl = "demo2.png",
                ClaimDateTime = DateTime.Now,
                ClaimDescription = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor manuris condimentum nibh, ut fermentum massa justo sit amet risus."
            });
            _claimListViewSource.Add(new ClaimViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "1011810",
                Status = ClaimStatus.Approved,
                ImageUrl = "demo3.png",
                ClaimDateTime = DateTime.Now,
                ClaimDescription = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor manuris condimentum nibh, ut fermentum massa justo sit amet risus."
            });
            
        }
        public async void LogoutBtn_Tapped(object sender, EventArgs e)
        {
            authenticationContext.TokenCache.Clear();
            await Navigation.PopAsync();
        }
    }
}
