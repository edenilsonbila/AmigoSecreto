using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AmigoSecreto.Startup))]
namespace AmigoSecreto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
