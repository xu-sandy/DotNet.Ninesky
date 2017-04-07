using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ninesky.Web.Startup))]
namespace Ninesky.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
