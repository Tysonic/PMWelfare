using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PMWelfare.Startup))]
namespace PMWelfare
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
