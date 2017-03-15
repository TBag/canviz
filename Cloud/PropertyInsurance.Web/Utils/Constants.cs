using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace PropertyInsurance.Web.Utils
{
    public static class Constants
    {
        public static readonly string AADClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static readonly string AADClientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];
        public static string AADTenantId
        {
            get { return ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value; }
        }

        public static readonly string ClaimApproverUrl = ConfigurationManager.AppSettings["ClaimApproverUrl"];

        public static readonly string AADInstance = "https://login.microsoftonline.com/";

        public static string ResourceUrl = ConfigurationManager.AppSettings["ida:GraphUrl"];
        public const string GraphResourceRootUrl = "https://graph.microsoft.com/";
        public const string GraphResourceAADRootUrl = "https://graph.microsoft.net/";
        public static string ConsentRedirectUrl
        {
            get { return AuthenticationHelper.GetWebRootUrl().ToString(); }
        }

        public static readonly string BingMapKey = ConfigurationManager.AppSettings["BingMapKey"];

        public static readonly string SourceCodeRepositoryUrl = ConfigurationManager.AppSettings["SourceCodeRepositoryUrl"];

        public static readonly string O365GroupConversationsUrl = "https://outlook.office.com/owa/?path=/group/{0}/mail&exsvurl=1&ispopout=0";

        public const string Base64Prefix = "data:image/jpeg;base64,";
        public const string ThumbnailExceptionCode = "Request_ResourceNotFound";
        public const string AnonymPhotoPath = "~/Content/images/Contoso-Insurance-Misc-Icons-PROFILE.svg";

        public const int BlobReadExpireMinitues = 5;
        public static readonly string StorageConnectionString = ConfigurationManager.AppSettings["AzureStorageAccountConnectionString"];
        public static readonly string pictureBlobcontainer = ConfigurationManager.AppSettings["blobcontainer"];
        public static readonly string blobImageName = ConfigurationManager.AppSettings["blobImageName"];

        public const string LostTokenErrorMsg = "Authorization Required.";

    }
}