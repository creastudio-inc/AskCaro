using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AskCaro_Web_MVC.Startup))]
namespace AskCaro_Web_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
