// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CustomerApp
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

        private const string MTCWebUrlKey = "MTCWebUrl_key";
        private static readonly string DefaultMTCWebUrl = "https://propertyinsuranceapi.azurewebsites.net";


        private const string ClientIDKey = "ClientID_key";
        private static readonly string DefaultClientID = "5c548cd0-a737-4ca4-a72a-f852e538ed6b";

        private const string SignUpSignInpolicyKey = "SignUpSignInpolicy_key";
        private static readonly string DefaultSignUpSignInpolicy = "B2C_1_PropertyInsurance";

        private const string TenantKey = "Tenant_key";
        private static readonly string DefaultTenant = "CANVIZPropInsB2C.onmicrosoft.com";//string.Empty;

        #endregion

        public static string Tenant
        {
            get {
                return AppSettings.GetValueOrDefault<string>(TenantKey, DefaultTenant); }

            set { AppSettings.AddOrUpdateValue<string>(TenantKey, value); }
        }

        public static string MTCWebUrl
        {
            get { return AppSettings.GetValueOrDefault<string>(MTCWebUrlKey, DefaultMTCWebUrl); }

            set { AppSettings.AddOrUpdateValue<string>(MTCWebUrlKey, value); }
        }

        public static string ClientID
        {
            get { return AppSettings.GetValueOrDefault<string>(ClientIDKey, DefaultClientID); }

            set { AppSettings.AddOrUpdateValue<string>(ClientIDKey, value); }
        }

        public static string SignUpSignInpolicy
        {
            get { return AppSettings.GetValueOrDefault<string>(SignUpSignInpolicyKey, DefaultSignUpSignInpolicy); }

            set { AppSettings.AddOrUpdateValue<string>(SignUpSignInpolicyKey, value); }
        }

        public static bool CheckAllConfigure()
        {
            return MTCWebUrl.Length > 0 && ClientID.Length > 0 && SignUpSignInpolicy.Length > 0 && Tenant.Length > 0;
        }


        public static string UploadImageUrl = MTCWebUrl + "/api/UploadIncidentPicture";
        public static string QueueUrl = MTCWebUrl + "/api/SubmitCaseForProcessing";
        public static string HubTagUrl = MTCWebUrl+ "/api/HubTag?InstallationId=";

        // app coordinates
        public static string[] Scopes = { ClientID };
        public static string Authority = $"https://login.microsoftonline.com/{Tenant}/";

        /*Hockey App*/
        public static string MobileHockeyAppIdiOS = "f54c9fa2db1b49ceb1d2901f60e9ea3c";
        public static string MobileHockeyAppIdAndroid = "88db49472d2a4b24bb7528db6b1a941a";
    }
}