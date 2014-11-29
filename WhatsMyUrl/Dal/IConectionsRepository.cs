using System;
using System.Threading.Tasks;
using WhatsMyUrl.Dal.Model;

namespace WhatsMyUrl.Dal
{
    public interface IConectionsRepository
    {
        Task<SessionConnection> Event(string sessionId, Guid hubId, HubState state = HubState.Unknown);
        Task<SessionConnection[]> Alive();
        Task<SessionConnection[]> History(string sessionId);
        Task<SessionConnection> SetUser(string userName, Guid connectionId);
        Task<SessionConnection> OnConnected(string sessionId, Guid connectionId);
        Task OnReconnected(string sessionId, Guid connectionId);
        Task OnDisconnected(string sessionId, Guid connectionId, bool stopCalled);
    }
}