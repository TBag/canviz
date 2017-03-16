using System.Configuration;

namespace PropertyInsurance.API
{
    public class Settings
    {
        public static string QueueName = "mobileclaimsqueue";
        public static string BlobContainerName = "pictureblobcontainer";
        public static string ClaimDetailsPageUrl = ConfigurationManager.AppSettings["claimDetailsPageUrl"];
        public static string AadInstance = ConfigurationManager.AppSettings["ida:AadInstance"];
        public static string Tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        public static string ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static string SusiPolicyId = ConfigurationManager.AppSettings["ida:SusiPolicyId"];
        public static string StorageConnectionString = ConfigurationManager.AppSettings["MS_AzureStorageAccountConnectionString"];
        public static string MtcsjsonUrl = ConfigurationManager.AppSettings["mtcsjsonUrl"];
        public static string MS_NotificationHubName = ConfigurationManager.AppSettings["MS_NotificationHubName"];
        public static string MS_NotificationHubConnectionString = ConfigurationManager.AppSettings["MS_NotificationHubConnectionString"];
        public static string ApprovePictureLogicApp = ConfigurationManager.AppSettings["ApprovePictureLogicApp"];
    }
}