using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace WhatsMyUrl.Dal.Model
{
    public class UserSession
    {
        [BsonId]
        public string Id { get; protected set; }
        [StringLength(24)]
        public string SessionId { get; protected set; }
        [StringLength(36)]
        public string HubId { get; protected set; }

        public HubState HubState { get; set; }
        public DateTime UpdatedOn { get; private set; }
    }
}