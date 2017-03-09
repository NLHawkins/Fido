using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PickMyBeer.Startup))]
namespace PickMyBeer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
