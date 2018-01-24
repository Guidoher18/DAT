using DAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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
            if (Request.Browser.IsMobileDevice) 
            {
                return View("~/Views/Home/Mobile.cshtml");
            }
            else
            {
                return View("~/Views/Home/Consentimiento.cshtml");
            }
                
        }

        /// <summary>
        /// Envía un Mail con el Link para participar de la Investigación
        /// </summary>
        /// <param name="MailMobile"></param>
        /// <returns> Redirige a Google </returns>
        [HttpPost]
        public ActionResult EnviarMail(string MailMobile)
        {
            string MyName = "Procesos Básicos";
            string MyMail = "guidoh181193@gmail.com";
            string MyPassword = "mglmcp1521805187";
            string TheirMail = MailMobile;
            string Subject = "Investigación sobre Razonamiento Mecánico";
            string Message = "El Link para participar de la Investigación es: https://goo.gl/gdpExZ. Recuerde ingresar desde una Computadora de Escritorio o Portatill. Desde ya, Muchas Gracias.";
            try
            {
                var smtp = new SmtpClient();
                smtp.Host = "Smtp.Gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Timeout = 10000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(MyMail, MyPassword);
                
                MailAddress from = new MailAddress(MyMail, MyName);
                MailAddress to = new MailAddress(TheirMail);
                MailMessage message = new MailMessage(from, to);
                message.Body = Message;
                message.BodyEncoding = Encoding.UTF8;
                message.Subject = Subject;

                smtp.Send(message);
                //Si todo sale bien configuro un mensaje
                MessageBox.Show("Email has been sent successfully.");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
            return Redirect("http://www.google.com"); 
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
        public ActionResult CrearSujeto(string Apellido, string Nombre, string Mail, string Sexo, int Edad, string Carrera, string Universidad)
        {
            HomeManager Manager = new HomeManager();

            if (Manager.Consultar(Mail) == null) {
                Sujeto Sujeto = new Sujeto ();
                Sujeto.Apellido = Apellido;
                Sujeto.Nombre = Nombre;
                Sujeto.Mail = Mail;
                Sujeto.Sexo = Sexo;
                Sujeto.Edad = Edad;
                Sujeto.Carrera = Carrera;
                Sujeto.Universidad = Universidad;

                Session["Sujeto"] = Sujeto; 
            
                return View("~/Views/Home/Consigna_RA.cshtml"); 
            }
            else{
                MessageBox.Show("El Mail ingresado ya existe. Por favor ingrese otra Dirección de Correo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show("El Mail ingresado ya existe. Por favor ingrese otra Dirección de Correo");
                //ScriptManager.RegisterClientScriptBlock(Page '' ,typeof(Page), "ClientScript", "alert('El Mail ingresado ya existe. Por favor ingrese otra Dirección de Correo')", true);
                return View("~/Views/Home/Consentimiento.cshtml"); 
            }
        }

        public ActionResult Test_RA()
        {
            return View("~/Views/Home/Test_RA.cshtml");
        }

        /// <summary>
        /// Toma las Respuestas de RA con los Datos del sujeto almacenados en session y los pasa por el Método Insertar [en BBDD]
        /// </summary>
        /// <param name="A01"></param>
        /// <param name="A02"></param>
        /// <param name="A03"></param>
        /// <param name="A04"></param>
        /// <param name="A05"></param>
        /// <param name="A06"></param>
        /// <param name="A07"></param>
        /// <param name="A08"></param>
        /// <param name="A09"></param>
        /// <param name="A10"></param>
        /// <param name="A11"></param>
        /// <param name="A12"></param>
        /// <param name="A13"></param>
        /// <param name="A14"></param>
        /// <param name="A15"></param>
        /// <param name="A16"></param>
        /// <param name="A17"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CargarRespuestas_RA(string A01, string A02, string A03, string A04, string A05, string A06, string A07, string A08, string A09, string A10, string A11, string A12, string A13, string A14, string A15, string A16, string A17)
        {
            var Sujeto = Session["Sujeto"] as Sujeto;

            Sujeto.RA_1 = A01;
            Sujeto.RA_2 = A02;
            Sujeto.RA_3 = A03;
            Sujeto.RA_4 = A04;
            Sujeto.RA_5 = A05;
            Sujeto.RA_6 = A06;
            Sujeto.RA_7 = A07;
            Sujeto.RA_8 = A08;
            Sujeto.RA_9 = A09;
            Sujeto.RA_10 = A10;
            Sujeto.RA_11 = A11;
            Sujeto.RA_12 = A12;
            Sujeto.RA_13 = A13;
            Sujeto.RA_14 = A14;
            Sujeto.RA_15 = A15;
            Sujeto.RA_16 = A16;
            Sujeto.RA_17 = A17;

            HomeManager Manager = new HomeManager();
            Manager.Insertar(Sujeto);

            Session["ID_Sujeto"] = Sujeto.ID;

            return View("~/Views/Home/Consigna_RM.cshtml");
        }

        public ActionResult Test_RM()
        {
            return View("~/Views/Home/Test_RM.cshtml");
        }

        /// <summary>
        /// Toma las Respuestas de RM y los pasa por el Método Insertar [en BBDD]
        /// </summary>
        /// <param name="M01"></param>
        /// <param name="M02"></param>
        /// <param name="M03"></param>
        /// <param name="M04"></param>
        /// <param name="M05"></param>
        /// <param name="M06"></param>
        /// <param name="M07"></param>
        /// <param name="M08"></param>
        /// <param name="M09"></param>
        /// <param name="M10"></param>
        /// <param name="M11"></param>
        /// <param name="M12"></param>
        /// <param name="M13"></param>
        /// <param name="M14"></param>
        /// <param name="M15"></param>
        /// <param name="M16"></param>
        /// <param name="M17"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CargarRespuestas_RM(string M01, string M02, string M03, string M04, string M05, string M06, string M07, string M08, string M09, string M10, string M11, string M12, string M13, string M14, string M15, string M16, string M17, string M18, string M19, string M20, string M21, string M22, string M23, string M24, string M25, string M26, string M27, string M28, string M29, string M30)
        {
            Sujeto Sujeto = new Sujeto();

            Sujeto.ID = Session["ID_Sujeto"] as string;
            Sujeto.RM_1 = M01;
            Sujeto.RM_2 = M02;
            Sujeto.RM_3 = M03;
            Sujeto.RM_4 = M04;
            Sujeto.RM_5 = M05;
            Sujeto.RM_6 = M06;
            Sujeto.RM_7 = M07;
            Sujeto.RM_8 = M08;
            Sujeto.RM_9 = M09;
            Sujeto.RM_10 = M10;
            Sujeto.RM_11 = M11;
            Sujeto.RM_12 = M12;
            Sujeto.RM_13 = M13;
            Sujeto.RM_14 = M14;
            Sujeto.RM_15 = M15;
            Sujeto.RM_16 = M16;
            Sujeto.RM_17 = M17;
            Sujeto.RM_18 = M18;
            Sujeto.RM_19 = M19;
            Sujeto.RM_20 = M20;
            Sujeto.RM_21 = M21;
            Sujeto.RM_22 = M22;
            Sujeto.RM_23 = M23;
            Sujeto.RM_24 = M24;
            Sujeto.RM_25 = M25;
            Sujeto.RM_26 = M26;
            Sujeto.RM_27 = M27;
            Sujeto.RM_28 = M28;
            Sujeto.RM_29 = M29;
            Sujeto.RM_30 = M30;
            
            HomeManager Manager = new HomeManager();
            Manager.ActualizarRM(Sujeto);

            return View("~/Views/Home/Consigna_RV.cshtml");
        }

        public ActionResult Test_RV()
        {
            return View("~/Views/Home/Test_RV.cshtml");
        }

        /// <summary>
        /// Toma las Respuestas de RV y los pasa por el Método Insertar [en BBDD]
        /// </summary>
        /// <param name="V01"></param>
        /// <param name="V02"></param>
        /// <param name="V03"></param>
        /// <param name="V04"></param>
        /// <param name="V05"></param>
        /// <param name="V06"></param>
        /// <param name="V07"></param>
        /// <param name="V08"></param>
        /// <param name="V09"></param>
        /// <param name="V10"></param>
        /// <param name="V11"></param>
        /// <param name="V12"></param>
        /// <param name="V13"></param>
        /// <param name="V14"></param>
        /// <param name="V15"></param>
        /// <param name="V16"></param>
        /// <param name="V17"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CargarRespuestas_RV(string V01, string V02, string V03, string V04, string V05, string V06, string V07, string V08, string V09, string V10, string V11, string V12, string V13, string V14, string V15, string V16, string V17)
        {
            Sujeto Sujeto = new Sujeto();

            Sujeto.ID = Session["ID_Sujeto"] as string;
            Sujeto.RV_1 = V01;
            Sujeto.RV_2 = V02;
            Sujeto.RV_3 = V03;
            Sujeto.RV_4 = V04;
            Sujeto.RV_5 = V05;
            Sujeto.RV_6 = V06;
            Sujeto.RV_7 = V07;
            Sujeto.RV_8 = V08;
            Sujeto.RV_9 = V09;
            Sujeto.RV_10 = V10;
            Sujeto.RV_11 = V11;
            Sujeto.RV_12 = V12;
            Sujeto.RV_13 = V13;
            Sujeto.RV_14 = V14;
            Sujeto.RV_15 = V15;
            Sujeto.RV_16 = V16;
            Sujeto.RV_17 = V17;

            HomeManager Manager = new HomeManager();
            Manager.ActualizarRV(Sujeto);

            return View("~/Views/Home/Final.cshtml");
        }


 

    }
}