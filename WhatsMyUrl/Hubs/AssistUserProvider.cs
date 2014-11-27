using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WhatsMyUrl.Hubs
{
    public class AssistUserProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            if (request.Cookies["ASP.NET_SessionId"] != null)
            {
                return request.Cookies["ASP.NET_SessionId"].Value;
            }
            return string.Empty;
        }
    }
}