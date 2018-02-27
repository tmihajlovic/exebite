using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Execom.ClientDemo.Startup))]
namespace Execom.ClientDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
