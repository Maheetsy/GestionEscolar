using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GestionEscuela.Startup))]
namespace GestionEscuela
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
