using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WhatsMyUrl.Dal;
using WhatsMyUrl.Dal.Model;

namespace WhatsMyUrl.Controllers
{
    public class AssistController : ApiController
    {
        [HttpPost]
        public IHttpActionResult HubConnect(string connectionId)
        {
            var hubConnection = new HubConnection
            {
                Id = connectionId,
                SessionId = HttpContext.Current.Session.SessionID
            };

            var shouldConnect = ConnectionsRepository.ShouldConnect(hubConnection.SessionId, connectionId);
            return Ok(shouldConnect);


        }
    }
}