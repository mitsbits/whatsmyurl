using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WhatsMyUrl.Dal;

namespace WhatsMyUrl.Hubs
{
    [HubName("assistHub")]
    public class AssistHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        #region Event Handlers
        public override Task OnConnected()
        {
            var hubId = Context.ConnectionId;
            if (Context.RequestCookies["ASP.NET_SessionId"] != null)
            {
                var sessionId = Context.RequestCookies["ASP.NET_SessionId"].Value;
                ConnectionsRepository.OnConnected(hubId, sessionId);
                Groups.Add(hubId, sessionId);
            }
            return base.OnConnected();
        }
        public override Task OnReconnected()
        {
            var hubId = Context.ConnectionId;
            var sessionId = Context.RequestCookies["ASP.NET_SessionId"].Value;
            ConnectionsRepository.OnReconnected(hubId, sessionId);
            return base.OnReconnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var hubId = Context.ConnectionId;
            var sessionId = Context.RequestCookies["ASP.NET_SessionId"].Value;
            ConnectionsRepository.OnDisconnected(hubId, sessionId, stopCalled);
            Groups.Remove(hubId, sessionId);
            return base.OnDisconnected(stopCalled);
        } 
        #endregion
    }
}