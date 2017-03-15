using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using CustomerApp;
using CustomerApp.iOS;

//clear select cell color
[assembly: ExportRenderer(typeof(ViewCell), typeof(CustomViewCellRenderer))]
namespace CustomerApp.iOS
{
    class CustomViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;

        }

    }
}