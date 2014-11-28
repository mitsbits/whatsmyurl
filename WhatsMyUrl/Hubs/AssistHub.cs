using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Security.Provider;
using WhatsMyUrl.Dal;

namespace WhatsMyUrl.Hubs
{
    [HubName("assistHub")]
    public class AssistHub : Hub
    {
        private readonly IConectionsRepository _repository;

        public AssistHub(IConectionsRepository repository)
        {
            _repository = repository;
        }

        public void Hello()
        {
            Clients.All.hello();
        }
        #region Event Handlers
        public  override Task OnConnected()
        {
            var hubId = Context.ConnectionId;
            if (Context.RequestCookies["ASP.NET_SessionId"] != null)
            {
                var sessionId = Context.RequestCookies["ASP.NET_SessionId"].Value;
               return   _repository.OnConnected(sessionId, Guid.Parse(hubId));
            }
            return base.OnConnected();
        }
        public override  Task OnReconnected()
        {
            var hubId = Context.ConnectionId;
            if (Context.RequestCookies["ASP.NET_SessionId"] != null)
            {
                var sessionId = Context.RequestCookies["ASP.NET_SessionId"].Value;
               return  _repository.OnReconnected(sessionId, Guid.Parse(hubId));
            }
            return base.OnReconnected();
        }
        public override  Task OnDisconnected(bool stopCalled)
        {
            var hubId = Context.ConnectionId;
            if (Context.RequestCookies["ASP.NET_SessionId"] != null)
            {
                var sessionId = Context.RequestCookies["ASP.NET_SessionId"].Value;
              return   _repository.OnDisconnected(sessionId, Guid.Parse(hubId), stopCalled);
            }
            return base.OnDisconnected(stopCalled);
        }
        #endregion
        //#region Event Handlers
        //public override Task OnConnected()
        //{
        //    var hubId = Context.ConnectionId;
        //    if (Context.RequestCookies["ASP.NET_SessionId"] != null)
        //    {
        //        var sessionId = Context.RequestCookies["ASP.NET_SessionId"].Value;
        //        MongoConnectionsRepository.OnConnected(hubId, sessionId);
        //        Groups.Add(hubId, sessionId);
        //    }
        //    return base.OnConnected();
        //}
        //public override Task OnReconnected()
        //{
        //    var hubId = Context.ConnectionId;
        //    var sessionId = Context.RequestCookies["ASP.NET_SessionId"].Value;
        //    MongoConnectionsRepository.OnReconnected(hubId, sessionId);
        //    return base.OnReconnected();
        //}
        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    var hubId = Context.ConnectionId;
        //    var sessionId = Context.RequestCookies["ASP.NET_SessionId"].Value;
        //    MongoConnectionsRepository.OnDisconnected(hubId, sessionId, stopCalled);
        //    Groups.Remove(hubId, sessionId);
        //    return base.OnDisconnected(stopCalled);
        //} 
        //#endregion
    }
}