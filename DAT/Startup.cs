using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DAT.Startup))]
namespace DAT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
