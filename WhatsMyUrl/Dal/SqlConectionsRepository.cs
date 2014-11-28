using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhatsMyUrl.Dal.Model;

namespace WhatsMyUrl.Dal
{
    public class SqlConectionsRepository : IConectionsRepository
    {
        public  Task<SessionConnection> Event(string sessionId, Guid hubId, HubState state = HubState.Unknown)
        {
            var connection = new SessionConnection(sessionId, hubId, state);
            using (var db = new SessionConnectionContext())
            {
                db.SessionConnections.Add(connection);
                db.SaveChanges();
            }

            return Task.FromResult(connection);
        }



        public  Task OnConnected(string sessionId, Guid connectionId)
        {
            return Event(sessionId, connectionId, HubState.Connected);
        }

        public  Task OnReconnected(string sessionId, Guid connectionId)
        {
            return Event(sessionId, connectionId, HubState.Connected);
        }

        public  Task OnDisconnected(string sessionId, Guid connectionId, bool stopCalled)
        {
            return Event(sessionId, connectionId, HubState.Disconnected);
        }
    }
}