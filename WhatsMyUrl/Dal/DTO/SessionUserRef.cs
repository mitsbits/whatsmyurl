using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsMyUrl.Dal.DTO
{
    public class SessionUserRef
    {
        private HashSet<string> _sessionIds = new HashSet<string>();
        private HashSet<Guid> _hubIds = new HashSet<Guid>(); 
        public string UserName { get; set; }
        public ICollection<string> SessionIds {
            get { return _sessionIds; }
        }
        public ICollection<Guid> HubIds { get { return _hubIds; } }
        public DateTime LastActivity { get; set; }
    }
}