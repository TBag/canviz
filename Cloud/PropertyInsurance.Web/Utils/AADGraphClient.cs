using Microsoft.Azure.ActiveDirectory.GraphClient;
using PropertyInsurance.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyInsurance.Web.Utils
{
    public class AADGraphClient
    {
        private ActiveDirectoryClient activeDirectoryClient;

        public AADGraphClient(ActiveDirectoryClient activeDirectoryClient)
        {
            this.activeDirectoryClient = activeDirectoryClient;
        }

        public async Task<UserInfo> GetCurrentUserAsync()
        {
            try
            {
                var me = await activeDirectoryClient.Me.ExecuteAsync();
                return new UserInfo
                {
                    Id = me.ObjectId,
                    GivenName = me.GivenName,
                    Surname = me.Surname,
                    Mail = me.Mail,
                    UserPrincipalName = me.UserPrincipalName,
                };
            }
            catch (DataServiceClientException ex)
            {

                throw new LostAuthorizationTokenException(Constants.LostTokenErrorMsg);
            }
        }

        public async Task<string> GetThumbnailAsync(string userPrincipal)
        {
            var user = await activeDirectoryClient.Users.Where(x => x.UserPrincipalName == userPrincipal).ExecuteSingleAsync();
            DataServiceStreamResponse photo = await user.ThumbnailPhoto.DownloadAsync();
            using (MemoryStream s = new MemoryStream())
            {
                photo.Stream.CopyTo(s);
                return Constants.Base64Prefix + Convert.ToBase64String(s.ToArray());
            }
        }
        
    }
}