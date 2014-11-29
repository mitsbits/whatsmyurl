using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Security.Provider;
using WhatsMyUrl.Dal;
using WhatsMyUrl.Dal.Model;

namespace WhatsMyUrl.Hubs
{
    [HubName("assistHub")]
    public class AssistHub : Hub
    {
        private readonly IConectionsRepository _repository;
        private string _session = string.Empty;

        private string SessionId
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_session)) return _session;
                if (Context.RequestCookies["ASP.NET_SessionId"] != null)
                {
                    _session = Context.RequestCookies["ASP.NET_SessionId"].Value;

                }
                return _session;
            }
        }

        public AssistHub(IConectionsRepository repository)
        {
            _repository = repository;
        }

        public void Hello()
        {
            Clients.All.hello();
        }
        public SessionConnection SetUser(string userName)
        {
            var hubId = Context.ConnectionId;
            return _repository.SetUser(userName, Guid.Parse(hubId)).Result;
        }
        public IEnumerable<SessionConnection> Alive()
        {
            return _repository.Alive().Result;
        }
        public SessionConnection Current()
        {
            var hubId = Context.ConnectionId;
            return _repository.Event(SessionId, Guid.Parse(hubId), HubState.Connected).Result;
        }
        #region Event Handlers
        public override  Task OnConnected()
        {
            var hubId = Context.ConnectionId;
            if (!string.IsNullOrWhiteSpace(SessionId))
            {
                _repository.OnConnected(SessionId, Guid.Parse(hubId));
            }
            return base.OnConnected();
        }
        public override Task OnReconnected()
        {
            var hubId = Context.ConnectionId;

             _repository.OnReconnected(SessionId, Guid.Parse(hubId));
            return base.OnReconnected();

        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var hubId = Context.ConnectionId;

             _repository.OnDisconnected(SessionId, Guid.Parse(hubId), stopCalled);
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