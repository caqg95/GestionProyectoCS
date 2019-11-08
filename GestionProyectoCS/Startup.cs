using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GestionProyectoCS.Startup))]
namespace GestionProyectoCS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
