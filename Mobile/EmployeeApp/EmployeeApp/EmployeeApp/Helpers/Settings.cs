// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;

namespace EmployeeApp
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }
        #region Setting Constants

        private const string ClaimImageContainerURLKey = "ClaimImageContainerURL_key";
        private static readonly string DefaultClaimImageContainerURL = "https://propertyinsurancestorage.blob.core.windows.net";


        private const string ClientIDKey = "ClientID_key";
        private static readonly string DefaultClientID = "d13ff0d8-b8d7-4266-bfcc-77faa1182783";

        private const string ReplyURLKey = "ReplyURL_key";
        private static readonly string DefaultReplyURL = "https://EmployeeMobileApp";

        private const string TenantKey = "Tenant_key";
        private static readonly string DefaultTenant = "CANVIZPropInsB2C.onmicrosoft.com";//string.Empty;

        private const string HockeyAppIdiOSKey = "HockeyAppIdiOS_key";
        private const string HockeyAppIdAndroidKey = "HockeyAppIdAndroid_key";
        private static readonly string DefaultHockeyAppIdiOS = "7e2742b8436f4e2b922a860ec100c926";
        private static readonly string DefaultHockeyAppIdAndroid = "52dc55629fc643ae8d2130411f7a386a";

        #endregion

        public static string Tenant
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(TenantKey, DefaultTenant);
            }

            set { AppSettings.AddOrUpdateValue<string>(TenantKey, value); }
        }

        public static string ClaimImageContainerURL
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(ClaimImageContainerURLKey, DefaultClaimImageContainerURL);
            }

            set { AppSettings.AddOrUpdateValue<string>(ClaimImageContainerURLKey, value); }
        }

        public static string ClientID
        {
            get { return AppSettings.GetValueOrDefault<string>(ClientIDKey, DefaultClientID); }

            set { AppSettings.AddOrUpdateValue<string>(ClientIDKey, value); }
        }

        public static string ReplyURL
        {
            get { return AppSettings.GetValueOrDefault<string>(ReplyURLKey, DefaultReplyURL); }

            set { AppSettings.AddOrUpdateValue<string>(ReplyURLKey, value); }
        }
        public static string HockeyAppIdiOS
        {
            get { return AppSettings.GetValueOrDefault<string>(HockeyAppIdiOSKey, DefaultHockeyAppIdiOS); }

            set { AppSettings.AddOrUpdateValue<string>(HockeyAppIdiOSKey, value); }
        }
        public static string HockeyAppIdAndroid
        {
            get { return AppSettings.GetValueOrDefault<string>(HockeyAppIdAndroidKey, DefaultHockeyAppIdAndroid); }

            set { AppSettings.AddOrUpdateValue<string>(HockeyAppIdAndroidKey, value); }
        }
        public static bool CheckAllConfigure()
        {
            return ClaimImageContainerURL.Length > 0 && ClientID.Length > 0 && ReplyURL.Length > 0 && Tenant.Length > 0;
        }
        public static string MtcsjsonUrl {
            get { return ClaimImageContainerURL + "/public/mtcs.json"; }
        }
        public static string ImageContainerUrl {
            get { return ClaimImageContainerURL + "/pictureblobcontainer/"; }
        }
        public static string Authority {
            get { return $"https://login.microsoftonline.com/{Tenant}/"; }
        }

        public static string Resource = "https://graph.windows.net";
    }
}