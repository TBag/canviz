using System;
using Xamarin.Forms;

namespace EmployeeApp
{
    public partial class ClaimDetailPage : ContentPage
    {
        private ClaimViewModel model;
            
        public ClaimDetailPage(ClaimViewModel cl)
        {
            InitializeComponent();
            model = cl;
            BindingContext = model;

            ToolbarItem optionbutton = new ToolbarItem
            {
                Text = "Options",
                Order = ToolbarItemOrder.Default,
                Priority = 0
            };
            ToolbarItems.Add(optionbutton);
            optionbutton.Clicked += OptionsClicked;

            if (model.ImageUrl.StartsWith("http:") || model.ImageUrl.StartsWith("https:"))
            {
                claimCellImage.Source = new UriImageSource { CachingEnabled = false, Uri = new Uri(model.ImageUrl) };
            }

            claimHintStackLayout.SetBinding(IsVisibleProperty, new Binding { Source = BindingContext, Path = "Status", Converter = new ClaminShowDetailHintConvert(), Mode = BindingMode.OneWay });
            claimHintIcon.SetBinding(Image.IsVisibleProperty, new Binding { Source = BindingContext, Path = "Status", Converter = new ClaminShowDetailHintIconConvert(), Mode = BindingMode.OneWay });
            claimHintTitle.SetBinding(Label.TextProperty, new Binding { Source = BindingContext, Path = "Status", Converter = new ClaminDetailHintTitleConvert(), Mode = BindingMode.OneWay });
            claimHintMessage.SetBinding(Label.TextProperty, new Binding { Source = BindingContext, Path = "Status", Converter = new ClaminDetailHintMessageConvert(), Mode = BindingMode.OneWay });
            claimHintFrame.SetBinding(Frame.BackgroundColorProperty, new Binding { Source = BindingContext, Path = "Status", Converter = new ClaminDetailHintBkConvert(), Mode = BindingMode.OneWay });
            claimHintMessage.SetBinding(Label.TextColorProperty, new Binding { Source = BindingContext, Path = "Status", Converter = new ClaminDetailHintMsgColorConvert(), Mode = BindingMode.OneWay });

            InitGridView();
            this.Title = "#" + cl.Name;
        }
        private void InitGridView()
        {
            if (mainPageGrid.RowDefinitions.Count == 0)
            {
                claimDetailStackLayout.Padding = new Thickness(0, Display.Convert(40));
                claimHintStackLayout.HeightRequest = Display.Convert(110);
                Display.SetGridRowsHeight(claimHintGrid, new string[] { "32", "36" });

                claimHintIcon.WidthRequest = Display.Convert(32);
                claimHintIcon.HeightRequest = Display.Convert(32);

                Display.SetGridRowsHeight(mainPageGrid, new string[] { "50", "46", "20",  "54", "176", "30", "40", "54", "36", "44", "440", "1*"});
                claimDescription.Margin = new Thickness(0, Display.Convert(14));
            }

        }

        public async void OptionsClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet(null, "Cancel", null, "Approve", "Contact policy holder", "Decline");
            switch (action)
            {
                case "Approve":
                    model.Status = ClaimStatus.Approved;
                    model.isNew = false;
                    break;
                case "Decline":
                    model.Status = ClaimStatus.Declined;
                    model.isNew = false;
                    break;
            }
        }
    }

}
