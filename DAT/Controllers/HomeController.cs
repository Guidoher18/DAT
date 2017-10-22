using DAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Windows.Forms;

namespace DAT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Home/Consentimiento.cshtml");
        }

        /// <summary>
        /// Crea un Sujeto Experimental Nuevo y lo Guarda en Session
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
            HomeManager Manager = new HomeManager();

            if (Manager.Consultar(Mail) == null) {
                Sujeto Sujeto = new Sujeto ();
                Sujeto.Apellido = Apellido;
                Sujeto.Nombre = Nombre;
                Sujeto.Mail = Mail;
                Sujeto.Sexo = Sexo;
                Sujeto.Edad = Edad;

                Session["Sujeto"] = Sujeto; 
            
                return View("~/Views/Home/Consignas.cshtml"); 
            }
            else{
                //MessageBox.Show("Error Message", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show("El Mail ingresado ya existe. Por favor ingrese otra Dirección de Correo");
                //ScriptManager.RegisterClientScriptBlock(Page '' ,typeof(Page), "ClientScript", "alert('El Mail ingresado ya existe. Por favor ingrese otra Dirección de Correo')", true);
                return View("~/Views/Home/Consentimiento.cshtml"); 
            }
        }

        /// <summary>
        /// Consulta Duplicado permite chequear en BBDD si el mail ya existe
        /// </summary>
        /// <param name="Mail"></param>
        /// <returns></returns>
        public string ConsultaDuplicado(string Mail) {
            HomeManager Manager = new HomeManager();
            var Consulta = Manager.Consultar(Mail);
            return Consulta; 
        }

        public ActionResult Test()
        {
            return View("~/Views/Home/Test.cshtml");
        }

        /// <summary>
        /// Toma las Respuestas del Sujeto con sus Datos previamente almacenados en session y los pasa por el Método Insertar [en BBDD]
        /// </summary>
        /// <param name="E01"></param>
        /// <param name="E02"></param>
        /// <param name="E03"></param>
        /// <param name="E04"></param>
        /// <param name="E05"></param>
        /// <param name="E06"></param>
        /// <param name="E07"></param>
        /// <param name="E08"></param>
        /// <param name="E09"></param>
        /// <param name="E10"></param>
        /// <param name="E11"></param>
        /// <param name="E12"></param>
        /// <param name="E13"></param>
        /// <param name="E14"></param>
        /// <param name="E15"></param>
        /// <param name="E16"></param>
        /// <param name="E17"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CargarRespuestas(string E01, string E02, string E03, string E04, string E05, string E06, string E07, string E08, string E09, string E10, string E11, string E12, string E13, string E14, string E15, string E16, string E17)
        {
            var Sujeto = Session["Sujeto"] as Sujeto;
            Sujeto.RA_1 = E01;
            Sujeto.RA_2 = E02;
            Sujeto.RA_3 = E03;
            Sujeto.RA_4 = E04;
            Sujeto.RA_5 = E05;
            Sujeto.RA_6 = E06;
            Sujeto.RA_7 = E07;
            Sujeto.RA_8 = E08;
            Sujeto.RA_9 = E09;
            Sujeto.RA_10 = E10;
            Sujeto.RA_11 = E11;
            Sujeto.RA_12 = E12;
            Sujeto.RA_13 = E13;
            Sujeto.RA_14 = E14;
            Sujeto.RA_15 = E15;
            Sujeto.RA_16 = E16;
            Sujeto.RA_17 = E17;
            
            HomeManager Manager = new HomeManager();
            Manager.Insertar(Sujeto);

            return View("~/Views/Home/Final.cshtml");
        }

    }
}