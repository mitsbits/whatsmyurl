using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WhatsMyUrl.Hubs
{
    public class Assist : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var id = Context.ConnectionId;

            return base.OnDisconnected(stopCalled);
        }
    }
}