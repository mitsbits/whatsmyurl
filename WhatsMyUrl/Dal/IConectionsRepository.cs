using System;
using System.Threading.Tasks;
using WhatsMyUrl.Dal.DTO;
using WhatsMyUrl.Dal.Model;

namespace WhatsMyUrl.Dal
{
    public interface IConectionsRepository
    {
        Task<SessionConnection> Event(string sessionId, Guid hubId, HubState state = HubState.Unknown);
        Task<SessionUserRef[]> Alive();
        Task<SessionConnection[]> History(string sessionId);
        Task<SessionConnection> SetUser(string userName, Guid connectionId);
        Task<SessionConnection> OnConnected(string sessionId, Guid connectionId);
        Task<SessionGroupMessage> GroupMessage(string sender, string body, params string[] recipients);
        Task OnReconnected(string sessionId, Guid connectionId);
        Task<SessionConnection> OnDisconnected(string sessionId, Guid connectionId, bool stopCalled);
    }
}