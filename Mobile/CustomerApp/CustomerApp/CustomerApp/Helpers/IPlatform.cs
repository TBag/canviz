using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp
{
    public interface IPlatform
    {
        //Task<string> GetCurrentVersion();
        Task RegisterWithMobilePushNotifications();
    }
}
