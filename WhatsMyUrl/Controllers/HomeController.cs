using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.AspNet.SignalR;
using WhatsMyUrl.Dal;
using WhatsMyUrl.Dal.Model;
using WhatsMyUrl.Hubs;

namespace WhatsMyUrl.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class HomeController : Controller
    {
        private readonly IConectionsRepository _repo;

        public HomeController()
        {
            _repo = new SqlConectionsRepository();
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<AssistHub>();
            hubContext.Clients.All.hello();
        }

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
        public JsonResult HubConnect(string connectionId)
        {
            Session["hubId"] = connectionId;
            var hubConnection = _repo.Event(Session.SessionID, Guid.Parse(connectionId), HubState.Connected);
            return Json(hubConnection.Result);
        }


    }
}