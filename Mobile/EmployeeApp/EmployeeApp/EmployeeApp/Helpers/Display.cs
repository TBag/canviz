using Xamarin.Forms;
using System;

namespace EmployeeApp
{
    public class Display
    {
        public static int Convert(int px)
        {
            if (Device.OS == TargetPlatform.iOS)
            {
                return px / 2;
            }
            else
            {
                int v = (int)((px * App.screenWidth / 750));
                return v;
            }
        }

        public static void SetGridRowsHeight(Grid grid, Array rows)
        {
            foreach (string rowheight in rows)
            {
                if (rowheight.EndsWith("*"))
                {
                    int starH = 0;
                    int.TryParse(rowheight.TrimEnd('*'), out starH);
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(starH, GridUnitType.Star) });
                }
                else
                {
                    int normalH = 0;
                    int.TryParse(rowheight, out normalH);
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(Display.Convert(normalH)) });
                }
            }
        }

        public static void SetGridRowHeightByDevice(Grid grid, double iosHeight, double androidHeight)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(Device.OS == TargetPlatform.iOS ? iosHeight / 2 : androidHeight) });
        }
    }
}
