using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PharmacySolution.Web.Startup))]
namespace PharmacySolution.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
