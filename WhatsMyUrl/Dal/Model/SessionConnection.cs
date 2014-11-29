using System;
using System.ComponentModel.DataAnnotations;

namespace WhatsMyUrl.Dal.Model
{
    public class SessionConnection : IHubConnection<string, Guid>
    {
        public SessionConnection(string sessionId, Guid hubId, HubState state)
            : this(sessionId, hubId)
        {
            HubState = state;
        }
        public SessionConnection(string sessionId, Guid hubId)
            : this()
        {
            SessionId = sessionId;
            HubId = hubId;
        }
        protected SessionConnection()
        {
            CreatedOn = DateTime.UtcNow;
        }
        [StringLength(24)]
        public string SessionId { get; set; }
        public Guid HubId { get; set; }
        public HubState HubState { get; set; }
        [Key]
        public DateTime CreatedOn { get; set; }

        public virtual SessionUser SessionUser { get; set; }
    }
}