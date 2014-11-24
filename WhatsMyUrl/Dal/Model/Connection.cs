using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace WhatsMyUrl.Dal.Model
{
    public class HubConnection
    {
        [BsonId]
        public string Id { get; set; }
        [StringLength(24)]
        public string SessionId { get; set; }
    }
}