using DAT.Models;
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

        public ActionResult Test()
        {
            return View("~/Views/Home/Test.cshtml");
        }

        /// <summary>
        /// Crea un Sujeto Experimental Nuevo
        /// </summary>
        /// <param name="Apellido"></param>
        /// <param name="Nombre"></param>
        /// <param name="Mail"></param>
        /// <param name="Sexo"></param>
        /// <param name="Edad"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult CrearSujeto(string Apellido, string Nombre, string Mail, string Sexo, int Edad)
        {
            Sujeto Sujeto = new Sujeto ();
            Sujeto.Apellido = Apellido;
            Sujeto.Nombre = Nombre;
            Sujeto.Mail = Mail;
            Sujeto.Sexo = Sexo;
            Sujeto.Edad = Edad;

            HomeManager Manager = new HomeManager();
            Manager.Insertar(Sujeto);
            
            return View("~/Views/Home/Consignas.cshtml"); 
        }

        public ActionResult CargarRespuestas(string E01, string E02, string E03, string E04, string E05, string E06, string E07, string E08, string E09, string E10, string E11, string E12, string E13, string E14, string E15, string E16, string E17)
        {
            Abstracto Respuestas = new Abstracto();
            Respuestas.E01 = E01;
            Respuestas.E02 = E02;
            Respuestas.E03 = E03;
            Respuestas.E04 = E04;
            Respuestas.E05 = E05;
            Respuestas.E06 = E06;
            Respuestas.E07 = E07;
            Respuestas.E08 = E08;
            Respuestas.E09 = E09;
            Respuestas.E10 = E10;
            Respuestas.E11 = E11;
            Respuestas.E12 = E12;
            Respuestas.E13 = E13;
            Respuestas.E14 = E14;
            Respuestas.E15 = E15;
            Respuestas.E16 = E16;
            Respuestas.E17 = E17;

            //HomeManager Manager = new HomeManager();
            //Manager.InsertarRespuestas(Respuestas);

            return View("~/Views/Home/Final.cshtml");
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