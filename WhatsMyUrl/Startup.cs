using Owin;

namespace WhatsMyUrl
{
    //http://codetunnel.io/cannot-have-many-tabs-open-with-signalr/
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
} 