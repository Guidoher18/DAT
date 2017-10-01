using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Home/Consentimiento.cshtml");
        }

        public ActionResult Consignas()
        {
            //llamar a Home Manager Crear para conectar con BBDD 
            return View();
        }

        public ActionResult Test()
        {
            return View("~/Views/Home/Test.cshtml");
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