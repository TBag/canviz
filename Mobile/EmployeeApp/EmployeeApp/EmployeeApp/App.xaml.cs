using Xamarin.Forms;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace EmployeeApp
{
    public partial class App : Application
    {
        public static IPlatformParameters PlatformParameters { get; set; }
        public static double screenWidth = 360;

       

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Color.FromHex("#46c3d6"),
                BarTextColor = Color.White,
            };
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
