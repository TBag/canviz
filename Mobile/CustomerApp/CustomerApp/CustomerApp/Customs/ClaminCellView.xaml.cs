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

            Display.SetGridRowsHeight(claimCellContentGrid, new string[] {"1*", "90", "1*" });
            Display.SetGridRowsHeight(claimCellFrameGrid, new string[] { "42", "48" });

        }
    }
}
