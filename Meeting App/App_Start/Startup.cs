using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(Meeting_App.Startup))]
namespace Meeting_App
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}