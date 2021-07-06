using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DreamTeam.Startup))]
namespace DreamTeam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
