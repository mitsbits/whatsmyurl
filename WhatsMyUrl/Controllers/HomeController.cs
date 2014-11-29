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




    }
}