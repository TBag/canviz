using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PropertyInsurance.WebAPI
{
    public class Settings
    {
        public static string QueueName = "mobileclaimsqueue";
        public static string BlobContainerName = "pictureblobcontainer";
        public static string ClaimDetailsPageUrl = ConfigurationManager.AppSettings["claimDetailsPageUrl"];
        public static string ClaimsAdjusterEmail = ConfigurationManager.AppSettings["claimsAdjusterEmail"];
        public static string AadInstance = ConfigurationManager.AppSettings["ida:AadInstance"];
        public static string Tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        public static string ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static string SusiPolicyId = ConfigurationManager.AppSettings["ida:SusiPolicyId"];
        public static string StorageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
        public static string RedirectUri = ConfigurationManager.AppSettings["StorageConnectionString"];
    }
}