using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomerApp
{
    public partial class ClaimDetailPage : ContentPage
    {
        public ClaimDetailPage(ClaimModel model)
        {
            InitializeComponent();
            this.BindingContext = model;
            //SizeChanged += OnPageSizeChanged;
            InitGridView();
        }
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    //set grid view height
        //    InitGridView();
        //}
        //void OnPageSizeChanged(object sender, EventArgs args)
        //{
        //    App.screenWidth = Width;
        //}
        private void InitGridView()
        {
            if (detailPageGrid.RowDefinitions.Count == 0)
            {
                detailPageGrid.Padding = new Thickness(0, Display.Convert(40));
                Display.SetGridRowsHeight(detailPageGrid, new int[] { 50, 46, 20, 54, 176, 30, 40, 54, 36, 44, 550});
                Display.SetGridRowsStarHeight(detailPageGrid, new int[] { 1 });
                claimDescription.Margin = new Thickness(0, Display.Convert(14), 0, 0);
            }

        }
    }

}
