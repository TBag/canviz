using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using System.Globalization;
using Xamarin.Forms;
using HockeyApp;
using Newtonsoft.Json;


namespace EmployeeApp
{
    class ClaminShowDetailHintConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = (string)value;
            if(status.Equals(ClaimStatus.Approved) || status.Equals(ClaimStatus.Pending))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class ClaminShowDetailHintIconConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = (string)value;
            if (status.Equals(ClaimStatus.Pending))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ClaminDetailHintTitleConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = (string)value;
            if (status.Equals(ClaimStatus.Pending))
            {
                return "CONFIDENCE RATING";
            }
            else if (status.Equals(ClaimStatus.Approved))
            {
                return "SUBMITTED";
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ClaminDetailHintMessageConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = (string)value;
            if (status.Equals(ClaimStatus.Pending))
            {
                return "This claim has a 95% confidence rating.";
            }
            else if (status.Equals(ClaimStatus.Approved))
            {
                return "Your manager is reviewing for final approval";
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ClaminDetailHintBkConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = (string)value;
            if (status.Equals(ClaimStatus.Pending))
            {
                return Color.FromHex("#a0cc51");
            }
            else if (status.Equals(ClaimStatus.Approved))
            {
                return Color.FromHex("#dbf2aa");
            }
            else
            {
                return Color.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ClaminDetailHintMsgColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = (string)value;
            if (status.Equals(ClaimStatus.Pending))
            {
                return Color.FromHex("#ffffff");
            }
            else if (status.Equals(ClaimStatus.Approved))
            {
                return Color.FromHex("#37577d");
            }
            else
            {
                return Color.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Helpers
    {
  
        public class USER {
            public string firstName { set; get; }
            public string lastName { set; get; }
            public string email { set; get; }
        }
        public class MTC
        {
            public string name { set; get; }
            public USER customer { set; get; }
            public USER adjuster { set; get; }
            public USER manager { set; get; }
        }

        public class MTCS
        {
            public List<MTC> mtcs { set; get; }
        }

        public static async Task<string> GetUserClaimURL(string email)
        {
            HttpClient client = new HttpClient();
            try
            {

                string content = await client.GetStringAsync(Settings.MtcsjsonUrl);
                content = content.Replace("\r\n", "");
                MTCS value = JsonConvert.DeserializeObject<MTCS>(content);
                var mtc = value.mtcs.Find(i => i.adjuster.email.Equals(email));
                if (mtc != null && mtc.name.Length > 0)
                {
                    string imageUrl = Settings.ImageContainerUrl + mtc.name + "_Image.jpg";
                    Utils.TraceStatus(imageUrl);
                    return imageUrl;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utils.TraceException("GetUserClaimURL", ex);
                Utils.TraceStatus("GetUserClaimURL " + Settings.MtcsjsonUrl);

                return null;
            }    

        }
    }
    public static class Utils
    {

        public static void TraceException(string logEvent, Exception ex)
        {
            Debug.WriteLine(logEvent + ex.Message);

            Dictionary<string, string> properties = new Dictionary<string, string>()
            {
                { "LogType", "Error Log"},
                { "Description", logEvent + ex.Message}
            };
            /*Hockey APP*/
            try
            {
                MetricsManager.TrackEvent("Failure", properties, null);
            }
            catch (Exception e)
            {
                Debug.WriteLine(logEvent + e);
            }
        }
        public static void TraceStatus(string logEvent)
        {
            Debug.WriteLine(logEvent);

            Dictionary<string, string> properties = new Dictionary<string, string>()
            {
                { "LogType", "Status Log"},
                { "Description", logEvent},
                { "Status", "Success"}
            };
            try
            {
                /*Hockey APP*/
                MetricsManager.TrackEvent(logEvent, properties, null);
            }

            catch (Exception e)
            {
                Debug.WriteLine(logEvent + e);
            }
        }
    }
    public class ActivityIndicatorScope : IDisposable
    {
        private ActivityIndicator indicator;
        private StackLayout indicatorPanel;

        public ActivityIndicatorScope(ActivityIndicator indicator, StackLayout indicatorPanel, bool showIndicator)
        {
            this.indicator = indicator;
            this.indicatorPanel = indicatorPanel;

            SetIndicatorActivity(showIndicator);
        }

        private void SetIndicatorActivity(bool isActive)
        {
            this.indicator.IsVisible = isActive;
            this.indicator.IsRunning = isActive;
            this.indicatorPanel.IsVisible = isActive;
        }

        public void Dispose()
        {
            SetIndicatorActivity(false);
        }
    }
}
