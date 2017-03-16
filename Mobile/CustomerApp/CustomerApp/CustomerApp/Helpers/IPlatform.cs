using System.Threading.Tasks;

namespace CustomerApp
{
    public interface IPlatform
    {
        Task RegisterWithMobilePushNotifications();
    }
}
