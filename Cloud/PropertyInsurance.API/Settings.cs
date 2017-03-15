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
        public static string StorageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
        public static string RedirectUri = ConfigurationManager.AppSettings["StorageConnectionString"];
        public static string MtcsjsonUrl = ConfigurationManager.AppSettings["mtcsjsonUrl"];
        public static string MS_NotificationHubName = ConfigurationManager.AppSettings["MS_NotificationHubName"];
        public static string MS_NotificationHubConnectionString = ConfigurationManager.AppSettings["MS_NotificationHubConnectionString"];
        public static string ApprovePictureLogicApp = "https://prod-31.westus.logic.azure.com:443/workflows/8d76a647ac0e4b32bc07ba5f43506bec/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=DQBj2Q2V26X2tOCNZtJbwbOUgW2mU6Pdo9mL8y6pAvs";
    }
}