using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TSAR.Startup))]
namespace TSAR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
