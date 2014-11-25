using Owin;

namespace WhatsMyUrl
{
    
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();


        }
    }
} 