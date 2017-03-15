using Xamarin.Forms;
using System;
using System.Collections;

namespace CustomerApp
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
                //convert px to dp android
                //return (int)((px / App.DisplayMetricsDensity) + 0.5);
                int v = (int)((px * App.screenWidth / 750) );
                return v;
            }
        }

        //mainPageGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
        //mainPageGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        //mainPageGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(200) });
        //mainPageGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });

        public static void SetGridRowsHeight(Grid grid, Array rows) {
            foreach (int rowheight in rows) {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(Display.Convert(rowheight)) });
            }
        }
        public static void SetGridRowsStarHeight(Grid grid, Array starrows)
        {
            foreach (int star in starrows)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(star, GridUnitType.Star) });
            }
        }
        public static void SetGridRowHeight(Grid grid, double iosHeight, double androidHeight)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(Device.OS == TargetPlatform.iOS ? iosHeight/2: androidHeight) });
        }
    }
}
