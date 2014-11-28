using System.Linq.Expressions;
using Microsoft.AspNet.SignalR;
using Owin;
using WhatsMyUrl.Dal;
using WhatsMyUrl.Hubs;

namespace WhatsMyUrl
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(
                typeof(AssistHub),
                () => new AssistHub(new SqlConectionsRepository()));
            app.MapSignalR();
        }
    }
}