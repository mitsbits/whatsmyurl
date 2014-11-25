using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using WhatsMyUrl.Dal;
using WhatsMyUrl.Dal.Model;

namespace WhatsMyUrl.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult HubConnect(string connectionId)
        {
            Session["hubId"] = connectionId;
            var hubConnection = new HubConnection
            {
                Id = connectionId,
                SessionId = Session.SessionID
            };

            var shouldConnect = ConnectionsRepository.ShouldConnect(hubConnection.SessionId, connectionId);
            return Json(shouldConnect);
        }
    }
}