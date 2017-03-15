using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HockeyApp;
using System.Diagnostics;

namespace CustomerApp
{
    public static class Utils
    {
        public static string MobileHockeyAppIdiOS = "f54c9fa2db1b49ceb1d2901f60e9ea3c";
        public static string MobileHockeyAppIdAndroid = "88db49472d2a4b24bb7528db6b1a941a";

        public static void TraceException(string logEvent, Exception ex)
        {
            Debug.WriteLine(logEvent + ex.Message);

            Dictionary<string, string> properties = new Dictionary<string, string>()
            {
                { "LogType", "Error Log"},
               // { "Version", Assembly.GetCallingAssembly().Version.ToString()},
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
               // { "Version", Assembly.GetCallingAssembly().GetName().Version.ToString()},
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
