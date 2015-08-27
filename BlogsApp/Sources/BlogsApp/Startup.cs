using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogsApp.Startup))]
namespace BlogsApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
