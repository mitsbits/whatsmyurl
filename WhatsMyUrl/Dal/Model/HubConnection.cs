using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace WhatsMyUrl.Dal.Model
{
    public class HubConnection
    {
        public HubConnection(string sessionId, string hubId, HubState state)
            : this(sessionId, hubId)
        {
            HubState = state;
        }
        public HubConnection(string sessionId, string hubId)
            : this()
        {
            SessionId = sessionId;
            HubId = hubId;
        }
        protected HubConnection()
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


    public enum HubState
    {
        Unknown = 0,
        Connected = 1,
        Disconnected = 2
    }
}