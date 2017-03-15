using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PropertyInsurance.API.Startup))]

namespace PropertyInsurance.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}