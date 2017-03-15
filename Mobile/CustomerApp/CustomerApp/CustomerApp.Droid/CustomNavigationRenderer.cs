using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using CustomerApp;
using CustomerApp.Droid;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.App;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationRenderer))]
namespace CustomerApp.Droid
{
    class CustomNavigationRenderer: NavigationRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            RemoveAppIconFromActionBar();
        }

        void RemoveAppIconFromActionBar()
        {
            if(MainActivity.UIContext != null)
            {
                // http://stackoverflow.com/questions/14606294/remove-icon-logo-from-action-bar-on-android
                var actionBar = ((Activity)MainActivity.UIContext).ActionBar;
                actionBar.SetIcon(new ColorDrawable(Android.Graphics.Color.Transparent));
            }
        }
    }
}