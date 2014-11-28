using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace WhatsMyUrl.Dal.Model
{
    public enum HubState
    {
        Unknown = 0,
        Connected = 1,
        Disconnected = 2
    }
}