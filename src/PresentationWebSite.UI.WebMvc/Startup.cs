using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PresentationWebSite.UI.WebMvc.Startup))]
namespace PresentationWebSite.UI.WebMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
