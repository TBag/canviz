using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using CustomerApp;
using CustomerApp.iOS;

[assembly: ExportRenderer(typeof(DateEntry), typeof(DateEntryRenderer))]
namespace CustomerApp.iOS
{
    public class DateEntryRenderer: EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = UIColor.White;
                Control.BorderStyle = UITextBorderStyle.None;
               // Control.TextColor = UIColor.FromRGB(31,61,143);
            }
        }
    }
}