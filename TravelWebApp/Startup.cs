using Owin;
using Microsoft.Owin;
using TravelWebApp;

[assembly: OwinStartup((typeof(Startup)))]

namespace TravelWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}