using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectSoccerPoo.Startup))]
namespace ProjectSoccerPoo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
