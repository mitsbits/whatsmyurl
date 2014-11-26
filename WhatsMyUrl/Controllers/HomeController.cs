using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Security;
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
        [Route("HubConnect")]
        public ActionResult HubConnect(string connectionId)
        {
            Session["hubId"] = connectionId;
            var hubConnection = ConnectionsRepository.Event(Session.SessionID, connectionId, HubState.Connected);
            return Json(hubConnection);
        }


    }
}