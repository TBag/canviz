using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HockeyApp;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Settings;


namespace CustomerApp
{
    public class HelperUtil
    {
        public static string ConvertDateToString(DateTime dt)
        {
            string suffix = string.Empty;
            switch (dt.Day)
            {
                case 1:
                case 21:
                case 31:
                    suffix =  "st";
                    break;
                case 2:
                case 22:
                    suffix =  "nd";
                    break;
                case 3:
                case 23:
                    suffix = "rd";
                    break;
                default:
                    suffix = "th";
                    break;
            }

            return string.Format("{0:MMMM} {1}{2}, {0:yyyy}", dt, dt.Day, suffix);
        }
    }
    public class HttpUtil
    {
        public static async Task<HttpResponseMessage> PostImageAsync(MediaFile image, string url, string token)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Add("ZUMO-API-VERSION", "2.0.0");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            StreamContent content = new StreamContent(image.GetStream());
            content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            var formData = new MultipartFormDataContent();
            formData.Add(content, "Image", "Image.jpg");
            request.Content = formData;

            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }

        public static async Task<HttpResponseMessage> PostJsonAsync(string json, string url, string token)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("ZUMO-API-VERSION", "2.0.0");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Content = new StringContent(json);
            Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = Content;

            HttpResponseMessage response = await client.SendAsync(request);
            return response;

        }
        public static async Task<HttpResponseMessage> PostAsync(string url, string token)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("ZUMO-API-VERSION", "2.0.0");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.SendAsync(request);
            return response;

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
            indicator.IsVisible = isActive;
            indicator.IsRunning = isActive;
            indicatorPanel.IsVisible = isActive;
        }

        public void Dispose()
        {
            SetIndicatorActivity(false);
        }
    }

}
