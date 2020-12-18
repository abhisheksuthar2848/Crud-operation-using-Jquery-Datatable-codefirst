using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoginService.Startup))]
namespace LoginService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
