using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace PropertyInsurance.API.Controllers
{
    [Authorize, MobileAppController]
    public class HubTagController : ApiController
    {
        public async Task<HttpResponseMessage> Post(string InstallationId)
        {
            //Retrieve current user's email 
            var customerEmail = ClaimsPrincipal.Current.FindFirst("emails").Value;

            // Create the notification hub client.
            NotificationHubClient hubClient = NotificationHubClient
                .CreateClientFromConnectionString(Settings.MS_NotificationHubConnectionString,
                    Settings.MS_NotificationHubName);

            // Return the installation for the specific ID.
            var installation = await hubClient.GetInstallationAsync(InstallationId);

            // Define a collection of PartialUpdateOperations. Note that 
            // only one '/tags' path is permitted in a given collection.
            var updates = new List<PartialUpdateOperation>();

            if (installation.Tags != null)
            {

                foreach (var removeTag in installation.Tags)
                {
                    if (removeTag == "uid:" + customerEmail) continue;

                    // Add a update operation for the tag.
                    updates.Add(new PartialUpdateOperation
                    {
                        Operation = UpdateOperationType.Remove,
                        Path = "/tags/" + removeTag
                    });
                }
            }

            if (installation.Tags == null || installation.Tags.Where(i => i == "uid:" + customerEmail).Count() == 0)
            {
                // Verify that the tags are a valid JSON array.
                var addTag = JArray.Parse("[\"uid:" + customerEmail + "\"]");

                // Add a update operation for the tag.
                updates.Add(new PartialUpdateOperation
                {
                    Operation = UpdateOperationType.Add,
                    Path = "/tags",
                    Value = addTag.ToString()
                });

                if (updates.Count > 0)
                {
                    try
                    {
                        // Add the requested tag to the installation.
                        await hubClient.PatchInstallationAsync(InstallationId, updates);
                    }
                    catch (MessagingException)
                    {
                        // When an error occurs, return a failure status.
                        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    }
                }
            }

            // Return success status.
            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        public async Task<HttpResponseMessage> Delete(string InstallationId)
        {
            // Create the notification hub client.
            NotificationHubClient hubClient = NotificationHubClient
                .CreateClientFromConnectionString(Settings.MS_NotificationHubConnectionString,
                    Settings.MS_NotificationHubName);

            // Return the installation for the specific ID.
            var installation = await hubClient.GetInstallationAsync(InstallationId);

            // Define a collection of PartialUpdateOperations. Note that 
            // only one '/tags' path is permitted in a given collection.
            var updates = new List<PartialUpdateOperation>();

            if (installation.Tags != null)
            {
                foreach (var removeTag in installation.Tags)
                {
                    // Add a update operation for the tag.
                    updates.Add(new PartialUpdateOperation
                    {
                        Operation = UpdateOperationType.Remove,
                        Path = "/tags/" + removeTag
                    });
                }

                try
                {
                    // Add the requested tag to the installation.
                    await hubClient.PatchInstallationAsync(InstallationId, updates);
                }
                catch (MessagingException)
                {
                    // When an error occurs, return a failure status.
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            }

            // Return success status.
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
