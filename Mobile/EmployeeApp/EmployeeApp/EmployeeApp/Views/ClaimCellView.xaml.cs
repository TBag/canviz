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

            Display.SetGridRowsStarHeight(claimCellGrid, new int[] { 1 });
            Display.SetGridRowsHeight(claimCellGrid, new int[] { 12});

            claimCellContentGrid.Padding = new Thickness(Display.Convert(24), 0);

            Display.SetGridRowsStarHeight(newClaimButtonGrid, new int[] { 10, 22, 10 });
        }
    }
}
