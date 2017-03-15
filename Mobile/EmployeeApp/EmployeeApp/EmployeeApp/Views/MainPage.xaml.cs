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
            SizeChanged += OnPageSizeChanged;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //set grid view height
            InitGridView();
        }
        void OnPageSizeChanged(object sender, EventArgs args)
        {
            App.screenWidth = Width;
        }
        private void InitGridView() {
            if(mainPageGrid.RowDefinitions.Count == 0)
            {
                listView.SeparatorVisibility = SeparatorVisibility.None;
                listView.ItemsSource = _claimListViewSource;
                listView.ItemTemplate = new DataTemplate(typeof(ClaimCell));
                listView.RowHeight = Display.Convert(96);

                //android has status bar
                Display.SetGridRowHeight(mainPageGrid, 30, 0.5);
                int[] rowsHeight = new int[] { 126, 570, 40, 134, 40, 134, 40 };
                Display.SetGridRowsHeight(mainPageGrid, rowsHeight);
                Display.SetGridRowsStarHeight(mainPageGrid, new int[] { 1 });
                claimListGrid.Padding = new Thickness(Display.Convert(52), Display.Convert(12),
                    Display.Convert(52), Display.Convert(40));

                Display.SetGridRowsHeight(claimListGrid, new int[] { 100 });
                Display.SetGridRowsStarHeight(claimListGrid, new int[] { 1 });
            }

        }
        private void InitClaimList()
        {
            _claimListViewSource.Add(
                new ClaimViewModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "15798",
                    Status = ClaimStatus.Pending,
                    ClaimDateTime = DateTime.Now,
                    isNew = true,
                    ImageUrl = "demo1.png",
                    ClaimDescription = "Our city was hit by an intense rainstorm on Sunday afternoon and our drainage pipe backed up. As a result, over a foot of water flooded our home, causing extensive damage to the floors as well as some furniture."
                });
            _claimListViewSource.Add(new ClaimViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "15797",
                Status = ClaimStatus.Approved,
                ImageUrl = "demo1.png",
                ClaimDateTime = DateTime.Now,
                ClaimDescription = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor manuris condimentum nibh, ut fermentum massa justo sit amet risus."
            });
            _claimListViewSource.Add(new ClaimViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "15796",
                Status = ClaimStatus.Approved,
                ImageUrl = "demo2.png",
                ClaimDateTime = DateTime.Now,
                ClaimDescription = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor manuris condimentum nibh, ut fermentum massa justo sit amet risus."
            });
            _claimListViewSource.Add(new ClaimViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "15795",
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
