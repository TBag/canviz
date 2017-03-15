#r "Newtonsoft.Json"

#load "TemplateNotification.csx"

using Microsoft.Azure;
using Microsoft.Azure.NotificationHubs;

private static readonly string FunctionName = "PushNotification";

public static async Task Run(TemplateNotification notification, TraceWriter log)
{
    var NotificationHubConnection = CloudConfigurationManager.GetSetting("MS_NotificationHubConnectionString");
    var NotificationHubName = CloudConfigurationManager.GetSetting("MS_NotificationHubName");
    var hub = NotificationHubClient.CreateClientFromConnectionString(NotificationHubConnection, NotificationHubName);
    await hub.SendTemplateNotificationAsync(notification.Properties, notification.TagExpression);
}