using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gLogin.Startup))]
namespace gLogin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
