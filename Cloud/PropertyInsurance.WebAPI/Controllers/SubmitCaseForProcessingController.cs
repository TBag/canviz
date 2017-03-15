using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System;
using System.Security.Claims;

namespace PropertyInsurance.WebAPI.Controllers
{
    [Authorize]
    public class SubmitCaseForProcessingController : ApiController
    {
        public HttpResponseMessage Post([FromBody] Models.MobilePropertyInsurance value)
        {
            var properties = new Dictionary<string, string>();

            properties.Add("ClaimDescription", value.ClaimDescription == null 
                ? string.Empty : value.ClaimDescription);

            properties.Add("ClaimDateTime", value.ClaimDateTime.ToString());

            //Retrieve current user's email 
            var customerEmail = ClaimsPrincipal.Current.FindFirst("emails");

            var PropertyInsurance = new Models.PropertyInsurance
            {
                claimDetailsPageUrl = Settings.ClaimDetailsPageUrl,
                claimsAdjusterEmail = Settings.ClaimsAdjusterEmail,
                CustomerEmail = customerEmail != null 
                    ? customerEmail.Value : string.Empty,
                CorrelationId = "0",
                ImageUrl = value.ImageUrl,
                BlobFilePath = new Uri(value.ImageUrl).PathAndQuery,
                Properties = properties,
                TagExpression = ""
            };

            var json = new JavaScriptSerializer().Serialize(PropertyInsurance);

            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                Settings.StorageConnectionString);

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference(Settings.QueueName);

            // Create the queue if it doesn't already exist.
            queue.CreateIfNotExists();

            // Create a message and add it to the queue.
            CloudQueueMessage message = new CloudQueueMessage(json);
            queue.AddMessage(message);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
