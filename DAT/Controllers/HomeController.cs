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
using SpreadsheetLight;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Reflection;

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

        public ActionResult Exportar() {
            HomeManager Leer = new HomeManager();
            Procesar(Leer.LeerRegistros());
            return View("~/Views/Home/Consentimiento.cshtml");
        }

        public void Procesar (Dictionary<int, Sujeto> SujetosBase)
        {
            /*string[] Letras = new string[] {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
                "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY" };

            string[] Campos = {
                "FechayHora", "ID", "Apellido", "Nombre", "Mail", "Sexo", "Edad", "Carrera", "Universidad",
                "RA_1", "RA_2", "RA_3", "RA_4", "RA_5", "RA_6", "RA_7", "RA_8", "RA_9", "RA_10", "RA_11", "RA_12", "RA_13", "RA_14", "RA_15", "RA_16", "RA_17",
                "RM_1", "RM_2", "RM_3", "RM_4", "RM_5", "RM_6", "RM_7", "RM_8", "RM_9", "RM_10", "RM_11", "RM_12", "RM_13", "RM_14", "RM_15", "RM_16", "RM_17", "RM_18", "RM_19", "RM_20", "RM_21", "RM_22", "RM_23", "RM_24", "RM_25", "RM_26", "RM_27", "RM_28", "RM_29", "RM_30",
                "RV_1", "RV_2", "RV_3", "RV_4", "RV_5", "RV_6", "RV_7", "RV_8", "RV_9", "RV_10", "RV_11", "RV_12", "RV_13", "RV_14", "RV_15", "RV_16", "RV_17",
                "RA_TR", "RM_TR", "RV_TR", "Abandono"
                 };*/

            SLDocument Libro = new SLDocument("C:/Users/Guido/Documents/Visual Studio 2015/Projects/DAT2/DAT/Exportar/Documento_Modelo/Base_de_Datos.xlsx");
            Libro.SelectWorksheet("Original");
            int Cant_Diccionario = SujetosBase.Count() + 1;

            for (int i= 1; i<Cant_Diccionario; i++)
            {
                Sujeto Caso = SujetosBase[i];
                int Fila = i + 1;
                Libro.InsertRow(Fila,1);

                Fila.ToString();

                

                //DATOS SOCIODEMOGRÁFICOS
                Libro.SetCellValue("A" + Fila, Caso.FechayHora);
                Libro.SetCellValueNumeric("B" + Fila, Caso.ID);
                Libro.SetCellValue("C" + Fila, Caso.Apellido);
                Libro.SetCellValue("D" + Fila, Caso.Nombre);
                Libro.SetCellValue("E" + Fila, Caso.Mail);
                Libro.SetCellValue("F" + Fila, Caso.Sexo);
                Libro.SetCellValueNumeric("G" + Fila, Caso.Edad.ToString());
                Libro.SetCellValue("H" + Fila, Caso.Carrera);
                Libro.SetCellValue("I" + Fila, Caso.Universidad);
                
                //RAZONAMIENTO ABSTRACTO
                Libro.SetCellValue("J" + Fila, Caso.RA_1);
                Libro.SetCellValue("K" + Fila, Caso.RA_2);
                Libro.SetCellValue("L" + Fila, Caso.RA_3);
                Libro.SetCellValue("M" + Fila, Caso.RA_4);
                Libro.SetCellValue("N" + Fila, Caso.RA_5);
                Libro.SetCellValue("O" + Fila, Caso.RA_6);
                Libro.SetCellValue("P" + Fila, Caso.RA_7);
                Libro.SetCellValue("Q" + Fila, Caso.RA_8);
                Libro.SetCellValue("R" + Fila, Caso.RA_9);
                Libro.SetCellValue("S" + Fila, Caso.RA_10);
                Libro.SetCellValue("T" + Fila, Caso.RA_11);
                Libro.SetCellValue("U" + Fila, Caso.RA_12);
                Libro.SetCellValue("V" + Fila, Caso.RA_13);
                Libro.SetCellValue("W" + Fila, Caso.RA_14);
                Libro.SetCellValue("X" + Fila, Caso.RA_15);
                Libro.SetCellValue("Y" + Fila, Caso.RA_16);
                Libro.SetCellValue("Z" + Fila, Caso.RA_17);

                //RAZONAMIENTO MECÁNICO
                Libro.SetCellValue("AA" + Fila, Caso.RM_1);
                Libro.SetCellValue("AB" + Fila, Caso.RM_2);
                Libro.SetCellValue("AC" + Fila, Caso.RM_3);
                Libro.SetCellValue("AD" + Fila, Caso.RM_4);
                Libro.SetCellValue("AE" + Fila, Caso.RM_5);
                Libro.SetCellValue("AF" + Fila, Caso.RM_6);
                Libro.SetCellValue("AG" + Fila, Caso.RM_7);
                Libro.SetCellValue("AH" + Fila, Caso.RM_8);
                Libro.SetCellValue("AI" + Fila, Caso.RM_9);
                Libro.SetCellValue("AJ" + Fila, Caso.RM_10);
                Libro.SetCellValue("AK" + Fila, Caso.RM_11);
                Libro.SetCellValue("AL" + Fila, Caso.RM_12);
                Libro.SetCellValue("AM" + Fila, Caso.RM_13);
                Libro.SetCellValue("AN" + Fila, Caso.RM_14);
                Libro.SetCellValue("AO" + Fila, Caso.RM_15);
                Libro.SetCellValue("AP" + Fila, Caso.RM_16);
                Libro.SetCellValue("AQ" + Fila, Caso.RM_17);
                Libro.SetCellValue("AR" + Fila, Caso.RM_18);
                Libro.SetCellValue("AS" + Fila, Caso.RM_19);
                Libro.SetCellValue("AT" + Fila, Caso.RM_20);
                Libro.SetCellValue("AU" + Fila, Caso.RM_21);
                Libro.SetCellValue("AV" + Fila, Caso.RM_22);
                Libro.SetCellValue("AW" + Fila, Caso.RM_23);
                Libro.SetCellValue("AX" + Fila, Caso.RM_24);
                Libro.SetCellValue("AY" + Fila, Caso.RM_25);
                Libro.SetCellValue("AZ" + Fila, Caso.RM_26);
                Libro.SetCellValue("BA" + Fila, Caso.RM_27);
                Libro.SetCellValue("BB" + Fila, Caso.RM_28);
                Libro.SetCellValue("BC" + Fila, Caso.RM_29);
                Libro.SetCellValue("BD" + Fila, Caso.RM_30);

                //RAZONAMIENTO VERBAL
                Libro.SetCellValue("BE" + Fila, Caso.RV_1);
                Libro.SetCellValue("BF" + Fila, Caso.RV_2);
                Libro.SetCellValue("BG" + Fila, Caso.RV_3);
                Libro.SetCellValue("BH" + Fila, Caso.RV_4);
                Libro.SetCellValue("BI" + Fila, Caso.RV_5);
                Libro.SetCellValue("BJ" + Fila, Caso.RV_6);
                Libro.SetCellValue("BK" + Fila, Caso.RV_7);
                Libro.SetCellValue("BL" + Fila, Caso.RV_8);
                Libro.SetCellValue("BM" + Fila, Caso.RV_9);
                Libro.SetCellValue("BN" + Fila, Caso.RV_10);
                Libro.SetCellValue("BO" + Fila, Caso.RV_11);
                Libro.SetCellValue("BP" + Fila, Caso.RV_12);
                Libro.SetCellValue("BQ" + Fila, Caso.RV_13);
                Libro.SetCellValue("BR" + Fila, Caso.RV_14);
                Libro.SetCellValue("BS" + Fila, Caso.RV_15);
                Libro.SetCellValue("BT" + Fila, Caso.RV_16);
                Libro.SetCellValue("BU" + Fila, Caso.RV_17);

                //TIEMPOS DE REACCIÓN
                Libro.SetCellValueNumeric("BV" + Fila, Caso.RA_TR);
                Libro.SetCellValueNumeric("BW" + Fila, Caso.RM_TR);
                Libro.SetCellValueNumeric("BX" + Fila, Caso.RV_TR);
                Libro.SetCellValue("BY" + Fila, Caso.Abandono);

            };
            Libro.DeleteRow(Cant_Diccionario + 1, 2);

            string Fecha = DateTime.Now.ToString();
            Fecha = Fecha.Replace("/", "-"); 
            Fecha = Fecha.Replace(":", ".");
            Fecha = Fecha.Remove(15,3);

            Libro.SaveAs("C:/Users/Guido/Documents/Visual Studio 2015/Projects/DAT2/DAT/Exportar/Base_de_Datos" + Fecha + ".xlsx");


            /*for (int i = 1; i == 2; ++i) 
            {
                Libro.SetCellValue(6, 1, "BUCLEEEE");
                Sujeto Caso = SujetosBase[i];
                int Fila = i + 1;
                string Fila_String = Fila.ToString();

                
                Libro.SetCellValue(2,1,(string)Caso.Apellido);//("A" + Fila_String) , Caso.FechayHora);
                Libro.SetCellValue(6,1, "BUCLEEEE");
                /*Libro.SetCellValue(Fila_String + "B", Caso.ID);
                    Libro.SetCellValue(Fila_String + "C", Caso.Apellido);
                    Libro.SetCellValue(Fila_String + "D", Caso.Nombre);
                    Libro.SetCellValue(Fila_String + "E", Caso.Mail);
                    Libro.SetCellValue(Fila_String + "F", Caso.Sexo);
                    Libro.SetCellNumber(Fila_String + "G", Caso.Edad);
                    Libro.SetCellValue(Fila_String + "H", Caso.Carrera);
                    Libro.SetCellValue(Fila_String + "I", Caso.Universidad);
                
                    Libro.SetCellValue(Fila_String + "J", Caso.RA_1);
                    Libro.SetCellValue(Fila_String + "K", Caso.RA_2);
                    Libro.SetCellValue(Fila_String + "L", Caso.RA_3);
                    Libro.SetCellValue(Fila_String + "M", Caso.RA_4);
                    Libro.SetCellValue(Fila_String + "N", Caso.RA_5);
                    Libro.SetCellValue(Fila_String + "O", Caso.RA_6);
                    Libro.SetCellValue(Fila_String + "P", Caso.RA_7);
                    Libro.SetCellValue(Fila_String + "Q", Caso.RA_8);
                    Libro.SetCellValue(Fila_String + "R", Caso.RA_9);
                    Libro.SetCellValue(Fila_String + "S", Caso.RA_10);
                    Libro.SetCellValue(Fila_String + "T", Caso.RA_11);
                    Libro.SetCellValue(Fila_String + "U", Caso.RA_12);
                    Libro.SetCellValue(Fila_String + "V", Caso.RA_13);
                    Libro.SetCellValue(Fila_String + "W", Caso.RA_14);
                    Libro.SetCellValue(Fila_String + "X", Caso.RA_15);
                    Libro.SetCellValue(Fila_String + "Y", Caso.RA_16);
                    Libro.SetCellValue(Fila_String + "Z", Caso.RA_17);

                    Libro.SetCellValue(Fila_String + "AA", Caso.RM_1);
                    Libro.SetCellValue(Fila_String + "AB", Caso.RM_2);
                    Libro.SetCellValue(Fila_String + "AC", Caso.RM_3);
                    Libro.SetCellValue(Fila_String + "AD", Caso.RM_4);
                    Libro.SetCellValue(Fila_String + "AE", Caso.RM_5);
                    Libro.SetCellValue(Fila_String + "AF", Caso.RM_6);
                    Libro.SetCellValue(Fila_String + "AG", Caso.RM_7);
                    Libro.SetCellValue(Fila_String + "AH", Caso.RM_8);
                    Libro.SetCellValue(Fila_String + "AI", Caso.RM_9);
                    Libro.SetCellValue(Fila_String + "AJ", Caso.RM_10);
                    Libro.SetCellValue(Fila_String + "AK", Caso.RM_11);
                    Libro.SetCellValue(Fila_String + "AL", Caso.RM_12);
                    Libro.SetCellValue(Fila_String + "AM", Caso.RM_13);
                    Libro.SetCellValue(Fila_String + "AN", Caso.RM_14);
                    Libro.SetCellValue(Fila_String + "AO", Caso.RM_15);
                    Libro.SetCellValue(Fila_String + "AP", Caso.RM_16);
                    Libro.SetCellValue(Fila_String + "AQ", Caso.RM_17);
                    Libro.SetCellValue(Fila_String + "AR", Caso.RM_18);
                    Libro.SetCellValue(Fila_String + "AS", Caso.RM_19);
                    Libro.SetCellValue(Fila_String + "AT", Caso.RM_20);
                    Libro.SetCellValue(Fila_String + "AU", Caso.RM_21);
                    Libro.SetCellValue(Fila_String + "AV", Caso.RM_22);
                    Libro.SetCellValue(Fila_String + "AW", Caso.RM_23);
                    Libro.SetCellValue(Fila_String + "AX", Caso.RM_24);
                    Libro.SetCellValue(Fila_String + "AY", Caso.RM_25);
                    Libro.SetCellValue(Fila_String + "AZ", Caso.RM_26);
                    Libro.SetCellValue(Fila_String + "BA", Caso.RM_27);
                    Libro.SetCellValue(Fila_String + "BB", Caso.RM_28);
                    Libro.SetCellValue(Fila_String + "BC", Caso.RM_29);
                    Libro.SetCellValue(Fila_String + "BD", Caso.RM_30);
                    
                    Libro.SetCellValue(Fila_String + "BE", Caso.RV_1);
                    Libro.SetCellValue(Fila_String + "BF", Caso.RV_2);
                    Libro.SetCellValue(Fila_String + "BG", Caso.RV_3);
                    Libro.SetCellValue(Fila_String + "BH", Caso.RV_4);
                    Libro.SetCellValue(Fila_String + "BI", Caso.RV_5);
                    Libro.SetCellValue(Fila_String + "BJ", Caso.RV_6);
                    Libro.SetCellValue(Fila_String + "BK", Caso.RV_7);
                    Libro.SetCellValue(Fila_String + "BL", Caso.RV_8);
                    Libro.SetCellValue(Fila_String + "BM", Caso.RV_9);
                    Libro.SetCellValue(Fila_String + "BN", Caso.RV_10);
                    Libro.SetCellValue(Fila_String + "BO", Caso.RV_11);
                    Libro.SetCellValue(Fila_String + "BP", Caso.RV_12);
                    Libro.SetCellValue(Fila_String + "BQ", Caso.RV_13);
                    Libro.SetCellValue(Fila_String + "BR", Caso.RV_14);
                    Libro.SetCellValue(Fila_String + "BS", Caso.RV_15);
                    Libro.SetCellValue(Fila_String + "BT", Caso.RV_16);
                    Libro.SetCellValue(Fila_String + "BU", Caso.RV_17);

                    Libro.SetCellValue(Fila_String + "BV", Caso.RA_TR);
                    Libro.SetCellValue(Fila_String + "BW", Caso.RM_TR);
                    Libro.SetCellValue(Fila_String + "BX", Caso.RV_TR);
                    Libro.SetCellValue(Fila_String + "BY", Caso.Abandono);

                    Libro.SetCellValue("A" + Fila.ToString(), Caso.FechayHora);
                    Libro.SetCellValue("B" + Fila.ToString(), Caso.ID);
                    Libro.SetCellValue("C" + Fila.ToString(), Caso.Apellido);
                    Libro.SetCellValue("D" + Fila.ToString(), Caso.Nombre);
                    Libro.SetCellValue("E" + Fila.ToString(), Caso.Mail);
                    Libro.SetCellValue("F" + Fila.ToString(), Caso.Sexo);
                    Libro.SetCellValue("G" + Fila.ToString(), Caso.Edad);
                    Libro.SetCellValue("H" + Fila.ToString(), Caso.Carrera);
                    Libro.SetCellValue("I" + Fila.ToString(), Caso.Universidad);
                
                    Libro.SetCellValue("J" + Fila.ToString(), Caso.RA_1);
                    Libro.SetCellValue("K" + Fila.ToString(), Caso.RA_2);
                    Libro.SetCellValue("L" + Fila.ToString(), Caso.RA_3);
                    Libro.SetCellValue("M" + Fila.ToString(), Caso.RA_4);
                    Libro.SetCellValue("N" + Fila.ToString(), Caso.RA_5);
                    Libro.SetCellValue("O" + Fila.ToString(), Caso.RA_6);
                    Libro.SetCellValue("P" + Fila.ToString(), Caso.RA_7);
                    Libro.SetCellValue("Q" + Fila.ToString(), Caso.RA_8);
                    Libro.SetCellValue("R" + Fila.ToString(), Caso.RA_9);
                    Libro.SetCellValue("S" + Fila.ToString(), Caso.RA_10);
                    Libro.SetCellValue("T" + Fila.ToString(), Caso.RA_11);
                    Libro.SetCellValue("U" + Fila.ToString(), Caso.RA_12);
                    Libro.SetCellValue("V" + Fila.ToString(), Caso.RA_13);
                    Libro.SetCellValue("W" + Fila.ToString(), Caso.RA_14);
                    Libro.SetCellValue("X" + Fila.ToString(), Caso.RA_15);
                    Libro.SetCellValue("Y" + Fila.ToString(), Caso.RA_16);
                    Libro.SetCellValue("Z" + Fila.ToString(), Caso.RA_17);

                    Libro.SetCellValue("AA" + Fila.ToString(), Caso.RM_1);
                    Libro.SetCellValue("AB" + Fila.ToString(), Caso.RM_2);
                    Libro.SetCellValue("AC" + Fila.ToString(), Caso.RM_3);
                    Libro.SetCellValue("AD" + Fila.ToString(), Caso.RM_4);
                    Libro.SetCellValue("AE" + Fila.ToString(), Caso.RM_5);
                    Libro.SetCellValue("AF" + Fila.ToString(), Caso.RM_6);
                    Libro.SetCellValue("AG" + Fila.ToString(), Caso.RM_7);
                    Libro.SetCellValue("AH" + Fila.ToString(), Caso.RM_8);
                    Libro.SetCellValue("AI" + Fila.ToString(), Caso.RM_9);
                    Libro.SetCellValue("AJ" + Fila.ToString(), Caso.RM_10);
                    Libro.SetCellValue("AK" + Fila.ToString(), Caso.RM_11);
                    Libro.SetCellValue("AL" + Fila.ToString(), Caso.RM_12);
                    Libro.SetCellValue("AM" + Fila.ToString(), Caso.RM_13);
                    Libro.SetCellValue("AN" + Fila.ToString(), Caso.RM_14);
                    Libro.SetCellValue("AO" + Fila.ToString(), Caso.RM_15);
                    Libro.SetCellValue("AP" + Fila.ToString(), Caso.RM_16);
                    Libro.SetCellValue("AQ" + Fila.ToString(), Caso.RM_17);
                    Libro.SetCellValue("AR" + Fila.ToString(), Caso.RM_18);
                    Libro.SetCellValue("AS" + Fila.ToString(), Caso.RM_19);
                    Libro.SetCellValue("AT" + Fila.ToString(), Caso.RM_20);
                    Libro.SetCellValue("AU" + Fila.ToString(), Caso.RM_21);
                    Libro.SetCellValue("AV" + Fila.ToString(), Caso.RM_22);
                    Libro.SetCellValue("AW" + Fila.ToString(), Caso.RM_23);
                    Libro.SetCellValue("AX" + Fila.ToString(), Caso.RM_24);
                    Libro.SetCellValue("AY" + Fila.ToString(), Caso.RM_25);
                    Libro.SetCellValue("AZ" + Fila.ToString(), Caso.RM_26);
                    Libro.SetCellValue("BA" + Fila.ToString(), Caso.RM_27);
                    Libro.SetCellValue("BB" + Fila.ToString(), Caso.RM_28);
                    Libro.SetCellValue("BC" + Fila.ToString(), Caso.RM_29);
                    Libro.SetCellValue("BD" + Fila.ToString(), Caso.RM_30);
                    
                    Libro.SetCellValue("BE" + Fila.ToString(), Caso.RV_1);
                    Libro.SetCellValue("BF" + Fila.ToString(), Caso.RV_2);
                    Libro.SetCellValue("BG" + Fila.ToString(), Caso.RV_3);
                    Libro.SetCellValue("BH" + Fila.ToString(), Caso.RV_4);
                    Libro.SetCellValue("BI" + Fila.ToString(), Caso.RV_5);
                    Libro.SetCellValue("BJ" + Fila.ToString(), Caso.RV_6);
                    Libro.SetCellValue("BK" + Fila.ToString(), Caso.RV_7);
                    Libro.SetCellValue("BL" + Fila.ToString(), Caso.RV_8);
                    Libro.SetCellValue("BM" + Fila.ToString(), Caso.RV_9);
                    Libro.SetCellValue("BN" + Fila.ToString(), Caso.RV_10);
                    Libro.SetCellValue("BO" + Fila.ToString(), Caso.RV_11);
                    Libro.SetCellValue("BP" + Fila.ToString(), Caso.RV_12);
                    Libro.SetCellValue("BQ" + Fila.ToString(), Caso.RV_13);
                    Libro.SetCellValue("BR" + Fila.ToString(), Caso.RV_14);
                    Libro.SetCellValue("BS" + Fila.ToString(), Caso.RV_15);
                    Libro.SetCellValue("BT" + Fila.ToString(), Caso.RV_16);
                    Libro.SetCellValue("BU" + Fila.ToString(), Caso.RV_17);

                    Libro.SetCellValue("BV" + Fila.ToString(), Caso.RA_TR);
                    Libro.SetCellValue("BW" + Fila.ToString(), Caso.RM_TR);
                    Libro.SetCellValue("BX" + Fila.ToString(), Caso.RV_TR);
                    Libro.SetCellValue("BY" + Fila.ToString(), Caso.Abandono);*/
        
        }
    }
}