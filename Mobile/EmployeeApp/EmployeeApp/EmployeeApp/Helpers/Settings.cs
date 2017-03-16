// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

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

        public static bool CheckAllConfigure()
        {
            return ClaimImageContainerURL.Length > 0 && ClientID.Length > 0 && ReplyURL.Length > 0 && Tenant.Length > 0;
        }

        public static string MtcsjsonUrl = $"{ClaimImageContainerURL}/public/mtcs.json";
        public static string ImageContainerUrl = $"{ClaimImageContainerURL}/pictureblobcontainer/";
        public static string Authority = $"https://login.microsoftonline.com/{Tenant}/";

        public static string Resource = "https://graph.windows.net";
    }
}