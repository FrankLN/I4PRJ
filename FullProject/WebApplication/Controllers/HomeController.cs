using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Description";

            return View("About");
        }

        public ActionResult Contact()
        {
            ViewBag.MessageP1 = "Visit our help pages for support and answers to commonly asked questions."; 
            ViewBag.MessageP2 = "Use the email link below to contact us for related enquiries.";

            return View("Contact");
        }
    }
}