using Microsoft.AspNet.SignalR;
using Owin;
using WhatsMyUrl.Hubs;

namespace WhatsMyUrl
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new AssistUserProvider());

        }
    }
}