using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Job_ProtalBd.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Indo()
        {
            return View();
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
        public ActionResult LoginorRegistration()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        public ActionResult tooo()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}