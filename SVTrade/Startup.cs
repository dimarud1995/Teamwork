using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SVTrade.Startup))]
namespace SVTrade
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
