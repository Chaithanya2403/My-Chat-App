using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SRICHIDACADEMY.Startup))]
namespace SRICHIDACADEMY
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
