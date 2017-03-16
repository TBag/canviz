using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EmployeeApp
{
    public partial class ClaimCellView : ContentView
    {
        public ClaimCellView()
        {
            InitializeComponent();

            cellStackLayout.HeightRequest = Display.Convert(96);

            Display.SetGridRowsHeight(claimCellGrid, new string[] {"1*", "12"});

            claimCellContentGrid.Padding = new Thickness(Display.Convert(24), 0);

            Display.SetGridRowsHeight(newClaimButtonGrid, new string[] { "10*", "22*", "10*" });
        }
    }
}
