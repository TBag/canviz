using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CustomerApp
{
    public partial class ClaminCellView : ContentView
    {
        public ClaminCellView()
        {
            InitializeComponent();
            cellStackLayout.HeightRequest = Display.Convert(140);

            Display.SetGridRowsStarHeight(claimCellContentGrid, new int[] { 1 });
            Display.SetGridRowsHeight(claimCellContentGrid, new int[] { 90 });
            Display.SetGridRowsStarHeight(claimCellContentGrid, new int[] { 1 });


            Display.SetGridRowsHeight(claimCellFrameGrid, new int[] { 42, 48 });

        }
    }
}
