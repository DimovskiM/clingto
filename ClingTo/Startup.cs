using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClingTo.Startup))]
namespace ClingTo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
