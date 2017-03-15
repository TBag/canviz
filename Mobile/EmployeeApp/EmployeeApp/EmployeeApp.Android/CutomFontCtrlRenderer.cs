using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using CustomerApp;
using CustomerApp.Droid;
using Android.Graphics;

[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer))]
[assembly: ExportRenderer(typeof(Button), typeof(CustomButtonRenderer))]
[assembly: ExportRenderer(typeof(Editor), typeof(CustomEditorRenderer))]
namespace CustomerApp.Droid
{
    public class CustomLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (!string.IsNullOrEmpty(e.NewElement?.FontFamily) &&(e.NewElement.FontFamily.Equals("Karla-Regular")
                    || e.NewElement.FontFamily.Equals("Poppins-Bold")
                    || e.NewElement.FontFamily.Equals("Poppins-Regular")))
            {
                var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement?.FontFamily+".ttf");

                Control.Typeface = font;
            }

        }
    }
    public class CustomButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (!string.IsNullOrEmpty(e.NewElement?.FontFamily) && (e.NewElement.FontFamily.Equals("Karla-Regular")
                    || e.NewElement.FontFamily.Equals("Poppins-Bold")
                    || e.NewElement.FontFamily.Equals("Poppins-Regular")))
            {
                var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement?.FontFamily + ".ttf");

                Control.Typeface = font;
            }

        }
    }
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (!string.IsNullOrEmpty(e.NewElement?.FontFamily) && (e.NewElement.FontFamily.Equals("Karla-Regular")
                    || e.NewElement.FontFamily.Equals("Poppins-Bold")
                    || e.NewElement.FontFamily.Equals("Poppins-Regular")))
            {
                var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement?.FontFamily + ".ttf");

                Control.Typeface = font;
            }

        }
    }
}
