using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace WhatsMyUrl.Dal.Model
{
    public class MongoHubConnection : IHubConnection<string, string>
    {
        public MongoHubConnection(string sessionId, string hubId, HubState state)
            : this(sessionId, hubId)
        {
            HubState = state;
        }
        public MongoHubConnection(string sessionId, string hubId)
            : this()
        {
            SessionId = sessionId;
            HubId = hubId;
        }
        protected MongoHubConnection()
        {
            Id = Guid.NewGuid().ToString();
            CreatedOn = DateTime.UtcNow;
        }
        [BsonId]
        public string Id { get; protected set; }
        [StringLength(24)]
        public string SessionId { get; protected set; }
        [StringLength(36)]
        public string HubId { get; protected set; }

        public HubState HubState { get; set; }
        public DateTime CreatedOn { get; private set; }
    }
}