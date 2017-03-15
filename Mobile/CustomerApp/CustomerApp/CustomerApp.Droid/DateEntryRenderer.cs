using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using CustomerApp;
using CustomerApp.Droid;

[assembly: ExportRenderer(typeof(DateEntry), typeof(DateEntryRenderer))]
namespace CustomerApp.Droid
{
    public class DateEntryRenderer: EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
            }
        }
    }
}