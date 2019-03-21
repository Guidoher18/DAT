using DAT.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

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
            string MyMail = "procesosbasicos19@gmail.com";
            string MyPassword = "razonamiento333";
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
            }
            catch (Exception ex)
            {
            }

            return Redirect("http://www.google.com");
        }

        /// <summary>
        /// Crea un Sujeto Experimental Nuevo y lo Guarda en Session
        /// </summary>
        /// <param name="Apellido"></param>
        /// <param name="Nombre"></param>
        /// <param name="Mail"></param>
        /// <param name="Genero"></param>
        /// <param name="Edad"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CrearSujeto(string Apellido, string Nombre, string Mail, string Genero, int Edad, string Carrera, string Universidad, string Cuatrimestre, string Año)
        {
            HomeManager Manager = new HomeManager();

            if (Manager.Consultar(Mail) == null) {
                Sujeto Sujeto = new Sujeto ();
                Sujeto.FechayHora = DateTime.Now.ToString(); //Fecha y Hora al momento de Aceptar el Concentimiento Informado
                Sujeto.Apellido = Apellido.ToUpper();
                Sujeto.Nombre = Nombre.ToUpper();
                Sujeto.Mail = Mail;
                Sujeto.Genero = Genero.ToUpper();
                Sujeto.Edad = Edad;
                Sujeto.Carrera = Carrera.ToUpper();
                Sujeto.Universidad = Universidad.ToUpper();
                Sujeto.Cuatrimestre = Cuatrimestre;
                Sujeto.Año = Año;

                Session["Sujeto"] = Sujeto;

                return RedirectToAction("Bloques", "Home");
            }
            else{
                ViewBag.Error = "El Mail ingresado ya existe. Ingrese otra dirección de correo electrónico.";
                return View("~/Views/Home/Consentimiento.cshtml"); 
            }
        }

        public ActionResult Bloques()
        {
            return View("~/Views/Home/Bloques.cshtml");
        }

        [HttpPost]
        public ActionResult CargarBloques(string Respuesta_CS, string Puntaje_CS, string CS_TR, string Respuesta_CI, string Puntaje_CI, string CI_TR)
        {
            var Sujeto = Session["Sujeto"] as Sujeto;

            Sujeto.Respuesta_CS = Respuesta_CS;
            Sujeto.Puntaje_CS = Puntaje_CS;
            Sujeto.CS_TR = CS_TR;

            Sujeto.Respuesta_CI = Respuesta_CI;
            Sujeto.Puntaje_CI = Puntaje_CI;
            Sujeto.CI_TR = CI_TR;

            Sujeto.FechayHoraSalida = DateTime.Now.ToString();

            Sujeto.Abandono = "Si_RM";

            HomeManager Manager = new HomeManager();
            Session["ID_Sujeto"] = Manager.Insertar(Sujeto);

            return RedirectToAction("Consigna_RM", "Home");
        }

        public ActionResult Consigna_RM()
        {
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
        /// <param name="M18"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CargarRespuestas_RM(string M01, string M02, string M03, string M04, string M05, string M06, string M07, string M08, string M09, string M10, string M11, string M12, string M13, string M14, string M15, string M16, string M17, string M18, string RM_TR)
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
            Sujeto.RM_TR = RM_TR;
            Sujeto.FechayHoraSalida = DateTime.Now.ToString();
            Sujeto.Abandono = "Si_RA";

            HomeManager Manager = new HomeManager();
            Manager.ActualizarRM(Sujeto);

            return RedirectToAction("Intermedio", "Home");
        }

        public ActionResult Intermedio()
        {
            return View("~/Views/Home/Intermedio.cshtml");
        }

        public ActionResult Consigna_RA()
        {
            return View("~/Views/Home/Consigna_RA.cshtml");
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
        /// <returns></returns>
        [HttpPost]
        public ActionResult CargarRespuestas_RA(string A01, string A02, string A03, string A04, string A05, string A06, string A07, string A08, string A09, string A10, string RA_TR)
        {
            Sujeto Sujeto = new Sujeto();

            Sujeto.ID = Session["ID_Sujeto"] as string;
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
            Sujeto.RA_TR = RA_TR;
            Sujeto.FechayHoraSalida = DateTime.Now.ToString();
            Sujeto.Abandono = "Si_RV";

            HomeManager Manager = new HomeManager();
            Manager.ActualizarRA(Sujeto);
                       
            return RedirectToAction("Consigna_RV", "Home");
        }

        public ActionResult Consigna_RV()
        {
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
        /// <returns></returns>
        [HttpPost]
        public ActionResult CargarRespuestas_RV(string V01, string V02, string V03, string V04, string V05, string V06, string V07, string V08, string V09, string V10, string RV_TR)
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
            Sujeto.RV_TR = RV_TR;
            Sujeto.FechayHoraSalida = DateTime.Now.ToString();
            Sujeto.Abandono = "No";

            HomeManager Manager = new HomeManager();
            Manager.ActualizarRV(Sujeto);

            return RedirectToAction("Final", "Home");
        }

        public ActionResult Final()
        {
            return View("~/Views/Home/Final.cshtml");
        }

        /// <summary>
        /// Muestra el desempeño obtenido en las pruebas
        /// </summary>
        /// <returns></returns>
        public ActionResult Resultados()
        {
            HomeManager Leer = new HomeManager();
            Sujeto Sujeto = Leer.Leer_un_registro(Session["ID_Sujeto"].ToString());

            ViewBag.Fecha = DateTime.Today.ToString("d");
            ViewBag.ApellidoyNombre = Sujeto.Apellido + " " + Sujeto.Nombre;
            ViewBag.Genero = Sujeto.Genero;
            ViewBag.Edad = Sujeto.Edad;
            ViewBag.Carrera = Sujeto.Carrera;
            ViewBag.Universidad = Sujeto.Universidad;
            ViewBag.Ingreso = Sujeto.Cuatrimestre + " " + Sujeto.Año;

            ViewBag.Aciertos_RA = Porcentaje(Sujeto.Puntaje_RA, 10);
            ViewBag.Aciertos_RM = Porcentaje(Sujeto.Puntaje_RM, 18);
            ViewBag.Aciertos_RV = Porcentaje(Sujeto.Puntaje_RV, 10);
            ViewBag.Aciertos_CS = Porcentaje(int.Parse(Sujeto.Puntaje_CS), 21);
            ViewBag.Aciertos_CI = Porcentaje(int.Parse(Sujeto.Puntaje_CI), 18);

            ViewBag.D_RA = AnalisisCualitativo(ViewBag.Aciertos_RA);
            ViewBag.D_RM = AnalisisCualitativo(ViewBag.Aciertos_RM);
            ViewBag.D_RV = AnalisisCualitativo(ViewBag.Aciertos_RV);
            ViewBag.D_CS = AnalisisCualitativo(ViewBag.Aciertos_CS);
            ViewBag.D_CI = AnalisisCualitativo(ViewBag.Aciertos_CI);

            ViewBag.TR_RA = Sujeto.RA_TR.Trim();
            ViewBag.TR_RM = Sujeto.RM_TR.Trim();
            ViewBag.TR_RV = Sujeto.RV_TR.Trim();
            ViewBag.TR_CS = Sujeto.CS_TR.Trim();
            ViewBag.TR_CI = Sujeto.CI_TR.Trim();

            return View("~/Views/Home/Resultados.cshtml");  
    }
        public Double Porcentaje(int Puntaje, int Divisor)
        {
            Double a = (Double)Puntaje/(Double)Divisor * 100;
            a = Math.Round(a,2);
            return a;
        }


        /// <summary>
    /// Devuelve las calificaciones cualitativas respecto de los desempeños en las pruebas
    /// </summary>
    /// <param name="A"></param>
    /// <returns></returns>
        public string AnalisisCualitativo(Double A)
        {
            if (A < 60)
            {
                if (A < 20) { return "Regular"; }
                else if (A < 40) { return "Bueno"; }
                else { return "Muy Bueno"; }
            }
            else
            {
                if (A < 80) { return "Excelente"; }
                else { return "Sobresaliente"; }
            }
        }
                     
        /// <summary>
        /// Permite Exportar los datos cargados en la BBDD
        /// </summary>
        /// <returns></returns>
        public ActionResult Exportar()
        {
            HomeManager Leer = new HomeManager();
            Procesar(Leer.LeerRegistros());
            return View("~/Views/Home/Descarga.cshtml");
        }

        /// <summary>
        /// Toma los datos de la BBDD y los transfiere a un .xlsx
        /// </summary>
        /// <param name="SujetosBase"></param>
        public void Procesar(Dictionary<int, Sujeto> SujetosBase)
        {
            SLDocument Libro = new SLDocument(Server.MapPath("../Exportar/Documento_Modelo/Base_de_Datos.xlsx"));
            Libro.SelectWorksheet("Original");
            int Cant_Diccionario = SujetosBase.Count() + 1;

            //Setea los datos de cada Sujeto en una fila
            for (int i = 1; i != Cant_Diccionario; i++)
            {
                Sujeto Caso = SujetosBase[i];
                int Fila = i + 1;
                Libro.InsertRow(Fila, 1);
                Fila.ToString();

                //DATOS SOCIODEMOGRÁFICOS
                Libro.SetCellValue("A" + Fila, Caso.FechayHora);
                Libro.SetCellValueNumeric("B" + Fila, Caso.ID);
                Libro.SetCellValue("C" + Fila, Caso.Apellido);
                Libro.SetCellValue("D" + Fila, Caso.Nombre);
                Libro.SetCellValue("E" + Fila, Caso.Mail);
                Libro.SetCellValue("F" + Fila, Caso.Genero.Trim());
                Libro.SetCellValueNumeric("G" + Fila, Caso.Edad.ToString());
                Libro.SetCellValue("H" + Fila, Caso.Carrera);
                Libro.SetCellValue("I" + Fila, Caso.Universidad);
                Libro.SetCellValue("J" + Fila, Caso.Cuatrimestre);
                Libro.SetCellValueNumeric("K" + Fila, Caso.Año);

                //RAZONAMIENTO ABSTRACTO

                Libro.SetCellValue("L" + Fila, Caso.RA_1);
                Libro.SetCellValue("M" + Fila, Caso.RA_2);
                Libro.SetCellValue("N" + Fila, Caso.RA_3);
                Libro.SetCellValue("O" + Fila, Caso.RA_4);
                Libro.SetCellValue("P" + Fila, Caso.RA_5);
                Libro.SetCellValue("Q" + Fila, Caso.RA_6);
                Libro.SetCellValue("R" + Fila, Caso.RA_7);
                Libro.SetCellValue("S" + Fila, Caso.RA_8);
                Libro.SetCellValue("T" + Fila, Caso.RA_9);
                Libro.SetCellValue("U" + Fila, Caso.RA_10);

                //RAZONAMIENTO MECÁNICO

                Libro.SetCellValue("V" + Fila, Caso.RM_1);
                Libro.SetCellValue("W" + Fila, Caso.RM_2);
                Libro.SetCellValue("X" + Fila, Caso.RM_3);
                Libro.SetCellValue("Y" + Fila, Caso.RM_4);
                Libro.SetCellValue("Z" + Fila, Caso.RM_5);
                Libro.SetCellValue("AA" + Fila, Caso.RM_6);
                Libro.SetCellValue("AB" + Fila, Caso.RM_7);
                Libro.SetCellValue("AC" + Fila, Caso.RM_8);
                Libro.SetCellValue("AD" + Fila, Caso.RM_9);
                Libro.SetCellValue("AE" + Fila, Caso.RM_10);
                Libro.SetCellValue("AF" + Fila, Caso.RM_11);
                Libro.SetCellValue("AG" + Fila, Caso.RM_12);
                Libro.SetCellValue("AH" + Fila, Caso.RM_13);
                Libro.SetCellValue("AI" + Fila, Caso.RM_14);
                Libro.SetCellValue("AJ" + Fila, Caso.RM_15);
                Libro.SetCellValue("AK" + Fila, Caso.RM_16);
                Libro.SetCellValue("AL" + Fila, Caso.RM_17);
                Libro.SetCellValue("AM" + Fila, Caso.RM_18); 

                //RAZONAMIENTO VERBAL

                Libro.SetCellValue("AN" + Fila, Caso.RV_1);
                Libro.SetCellValue("AO" + Fila, Caso.RV_2);
                Libro.SetCellValue("AP" + Fila, Caso.RV_3);
                Libro.SetCellValue("AQ" + Fila, Caso.RV_4);
                Libro.SetCellValue("AR" + Fila, Caso.RV_5);
                Libro.SetCellValue("AS" + Fila, Caso.RV_6);
                Libro.SetCellValue("AT" + Fila, Caso.RV_7);
                Libro.SetCellValue("AU" + Fila, Caso.RV_8);
                Libro.SetCellValue("AV" + Fila, Caso.RV_9);
                Libro.SetCellValue("AW" + Fila, Caso.RV_10);

                Libro.SetCellValue("AX" + Fila, Caso.Respuesta_CS.Trim());
                Libro.SetCellValue("AY" + Fila, Caso.Respuesta_CI.Trim());
                
                //TIEMPOS DE REACCIÓN
                Libro.SetCellValueNumeric("AZ" + Fila, Caso.RA_TR.Trim());
                Libro.SetCellValueNumeric("BA" + Fila, Caso.RM_TR.Trim());
                Libro.SetCellValueNumeric("BB" + Fila, Caso.RV_TR.Trim());
                Libro.SetCellValueNumeric("BC" + Fila, Caso.CS_TR.Trim());
                Libro.SetCellValueNumeric("BD" + Fila, Caso.CI_TR.Trim());

                Libro.SetCellValueNumeric("BE" + Fila, Caso.Puntaje_CS);
                Libro.SetCellValueNumeric("BF" + Fila, Caso.Puntaje_CI);

                Libro.SetCellValue("BG" + Fila, Caso.FechayHoraSalida);
                Libro.SetCellValue("BH" + Fila, Caso.Abandono);
            };

            Libro.DeleteRow(Cant_Diccionario + 1, 2);
            Libro.CopyCellStyle(2, 8, 2, 9, Cant_Diccionario, 10); 
            
            //Corrección (Hoja 2 del xls)
            Libro.SelectWorksheet("Corrección");
            Libro.InsertRow(5, Cant_Diccionario - 3);
            Libro.CopyRowStyle(4, 5, Cant_Diccionario - 3);

            Libro.CopyCellFromWorksheet("Original", 2, 1, Cant_Diccionario, 11, 4, 1, 0);    //       Copio A:K
            Libro.CopyCellFromWorksheet("Original", 2, 50, Cant_Diccionario, 51, 4, 50, 0);  //       Copio Respuesta_CS y Respuesta_CI

            Libro.CopyCellFromWorksheet("Original", 2, 52, Cant_Diccionario, 52, 4, 66, 0);  //       RA_TR
            Libro.CopyCellFromWorksheet("Original", 2, 53, Cant_Diccionario, 53, 4, 67, 0);  //       RM_TR
            Libro.CopyCellFromWorksheet("Original", 2, 54, Cant_Diccionario, 54, 4, 68, 0);  //       RV_TR
            Libro.CopyCellFromWorksheet("Original", 2, 55, Cant_Diccionario, 55, 4, 69, 0);  //       CS_TR
            Libro.CopyCellFromWorksheet("Original", 2, 56, Cant_Diccionario, 56, 4, 70, 0);  //       CI_TR

            Libro.CopyCellFromWorksheet("Original", 2, 57, Cant_Diccionario, 57, 4, 56, 0);  //       Puntaje_CS
            Libro.CopyCellFromWorksheet("Original", 2, 58, Cant_Diccionario, 58, 4, 58, 0);  //       Puntaje_CI

            Libro.CopyCellFromWorksheet("Original", 2, 59, Cant_Diccionario, 59, 4, 52, 0);  //       FechayHoraSalida
            
            Libro.CopyCellFromWorksheet("Original", 2, 60, Cant_Diccionario, 60, 4, 72, 0);  //       Abandonó

            //Tiempo Total de la bateria
            string FechayHoraInicio = "";
            string FechayHoraSalida = "";

            for (int A = 4; A < Cant_Diccionario + 3; A++)
            {
                FechayHoraInicio = Libro.GetCellValueAsString(A, 1);
                FechayHoraSalida = Libro.GetCellValueAsString(A, 52);

                string[] J = Restar_FechayHora(FechayHoraInicio, FechayHoraSalida, A);

                Libro.SetCellValue(J[0], J[1]);                                              //      Tiempo Total
            }

            //Completo los Resultados de Polígonos en Corrección
            string Respuesta_CS = "";
            string Respuesta_CI = "";

            for (int A = 4; A < Cant_Diccionario + 3; A++)
            {
                Respuesta_CS = Libro.GetCellValueAsString(A, 50);
                Respuesta_CI = Libro.GetCellValueAsString(A, 51);
                string[] valores = Verificar_Poligonos(Respuesta_CS, Respuesta_CI);

                Libro.SetCellValue("BE" + A, valores[0]);               //  Serie_CS
                Libro.SetCellValue("BG" + A, valores[1]);               //  Serie_CI
                Libro.SetCellValueNumeric("BH" + A, valores[2]);        //  Total_Series
                Libro.SetCellValueNumeric("BJ" + A, valores[3]);        //  Aciertos
                Libro.SetCellValueNumeric("BK" + A, valores[4]);        //  Errores
                Libro.SetCellValueNumeric("BL" + A, valores[5]);        //  Vacios
            }

            //Autocompletar a partir de la fila 3 hasta el final
            for (int Columna = 12; Columna < 73; Columna++)
            {
                if (Columna < 50)
                {
                    Autocompletar(Libro, Columna, Cant_Diccionario);
                }
                               
                else
                {
                    switch (Columna)
                    {
                        case 53:
                            Autocompletar(Libro, Columna, Cant_Diccionario);
                            break;
                        case 54:
                            Autocompletar(Libro, Columna, Cant_Diccionario);
                            break;
                        case 55:
                            Autocompletar(Libro, Columna, Cant_Diccionario);
                            break;
                        case 61:
                            Autocompletar(Libro, Columna, Cant_Diccionario);
                            break;
                        case 65:
                            Autocompletar(Libro, Columna, Cant_Diccionario);
                            break;
                        default:
                            break; 
                    }
                    continue;
                }
            }

            //Homogeinizo el Estilo
            Libro.CopyCellStyle(4, 57, 5, 57, Cant_Diccionario + 2, 57);
            Libro.CopyCellStyle(4, 59, 5, 59, Cant_Diccionario + 2, 59);
            Libro.CopyCellStyle(4, 60, 5, 60, Cant_Diccionario + 2, 60);
            Libro.CopyCellStyle(4, 62, 5, 62, Cant_Diccionario + 2, 62);
            Libro.CopyCellStyle(4, 63, 5, 63, Cant_Diccionario + 2, 63);
            Libro.CopyCellStyle(4, 64, 5, 64, Cant_Diccionario + 2, 64);

            //Bordes en Corrección
            SLStyle style = Libro.CreateStyle();
            style.Border.RightBorder.BorderStyle = BorderStyleValues.Thick;
            Libro.SetCellStyle(4, 11, Cant_Diccionario + 2, 11, style);
            Libro.SetCellStyle(4, 21, Cant_Diccionario + 2, 21, style);
            Libro.SetCellStyle(4, 39, Cant_Diccionario + 2, 39, style);
            Libro.SetCellStyle(4, 49, Cant_Diccionario + 2, 49, style);
            Libro.SetCellStyle(4, 51, Cant_Diccionario + 2, 51, style);
            Libro.SetCellStyle(4, 52, Cant_Diccionario + 2, 52, style);
            Libro.SetCellStyle(4, 59, Cant_Diccionario + 2, 59, style);
            Libro.SetCellStyle(4, 65, Cant_Diccionario + 2, 65, style);
            Libro.SetCellStyle(4, 70, Cant_Diccionario + 2, 70, style);

            Libro.SetCellStyle(4, 72, Cant_Diccionario + 2, 72, style);

            //Nombre del Archivo y Descarga
            string Fecha = DateTime.Now.ToString();
            Fecha = Fecha.Replace("/", "-");
            Fecha = Fecha.Replace(":", ".");
            Fecha = Fecha.Remove(15, 3);
            Fecha = Fecha.Replace(" ", "_");

            string Nombre_Archivo = "Base_de_Datos_" + Fecha + ".xlsx";
            string Ruta = Server.MapPath("~/Exportar/" + Nombre_Archivo);
             
            Libro.SaveAs(Ruta);

            ViewBag.Nombre_Archivo = Nombre_Archivo;

            Session["Nombre_Archivo"] = Nombre_Archivo;
        }
                       
        /// <summary>
        /// Obtiene la diferencia entre dos fechas 
        /// </summary>
        /// <param name="FechayHoraInicio"></param>
        /// <param name="FechayHoraSalida"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public string[] Restar_FechayHora (string FechayHoraInicio, string FechayHoraSalida, int A)
        {
            string Dato = "";
            string Celda = "";

            Celda = "BS" + A.ToString();

            if (FechayHoraInicio != "" && FechayHoraInicio != null && FechayHoraSalida != "" && FechayHoraSalida != null)
            {
                DateTime F = DateTime.Parse(FechayHoraInicio);
                DateTime G = DateTime.Parse(FechayHoraSalida);
                Dato = (G.Subtract(F)).ToString();
            }
            else
            {
                Dato = "";
            }

            string[] J = { Celda, Dato };
            
            return J;            
        }

        /// <summary>
        /// Analiza las respuestas de los Polígonos en Corsi con Interferencia
        /// </summary>
        /// <param name="Respuesta_CS"></param>
        /// <param name="Respuesta_CI"></param>
        /// <returns></returns>
        public string[] Verificar_Poligonos(string Respuesta_CS, string Respuesta_CI)
        {
            string Serie_CS = "";
            string Serie_CI = "";

            //Corsi con Interferencia
            int Total_Series = 0;
            int Aciertos = 0;
            int Errores = 0;
            int Vacios = 0;

            if (Respuesta_CS != "" && Respuesta_CS != null)
            {
                char[] charSeparator = new char[] { ';' };
                string[] Lista_CS1 = Respuesta_CS.Split(charSeparator, StringSplitOptions.RemoveEmptyEntries);
                int a = Lista_CS1.Length;
                string[] Lista_CS2 = Lista_CS1[a - 1].Split(',');
                Serie_CS = Lista_CS2[0];
            }

            if (Respuesta_CI != "" && Respuesta_CI != null)
            {
                //Corsi con Interferencia - Polígonos
                char[] charSeparator = new char[] { ';' };
                string[] Lista_CI1 = Respuesta_CI.Split(charSeparator, StringSplitOptions.RemoveEmptyEntries);
                int b = Lista_CI1.Length;
                string[] Lista_CI2 = Lista_CI1[b - 1].Split(',');
                Serie_CI = Lista_CI2[0];

                switch (Serie_CI)
                {
                    case "Serie 2":
                        Total_Series = 1;
                        break;
                    case "Serie 3":
                        Total_Series = 2;
                        break;
                    case "Serie 4":
                        Total_Series = 3;
                        break;
                    case "Serie 5":
                        Total_Series = 4;
                        break;
                    case "Serie 6":
                        Total_Series = 5;
                        break;
                    case "Serie 7":
                        Total_Series = 6;
                        break;
                }

                //Listas con Polígonos Correctos
                string[] Serie_2 = { "P12", "P10", "P12" };
                string[] Serie_3 = { "P10", "P8", "P10" };
                string[] Serie_4 = { "P12", "P10", "P12" };
                string[] Serie_5 = { "P10", "P8", "P10" };
                string[] Serie_6 = { "P8", "P12", "P10" };
                string[] Serie_7 = { "P8", "P8", "P10" };

                foreach (string x in Lista_CI1)         //Analizo cada Serie 2,28,P10,35,P10,45,P12;
                {
                    string[] A = x.Split(',');

                    string[] B = { A[2], A[4], A[6] };  // Almaceno en una lista los 3 Polígonos de la Serie

                    string[] C = { };                   // Lista de Respuestas Correctas a comparar

                    switch (A[0])
                    {
                        case "Serie 2":
                            C = Serie_2;
                            break;
                        case "Serie 3":
                            C = Serie_3;
                            break;
                        case "Serie 4":
                            C = Serie_4;
                            break;
                        case "Serie 5":
                            C = Serie_5;
                            break;
                        case "Serie 6":
                            C = Serie_6;
                            break;
                        case "Serie 7":
                            C = Serie_7;
                            break;
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        if (B[i] != "")
                        {
                            if (B[i] == C[i])
                            {
                                Aciertos += 1;
                            }
                            else
                            {
                                Errores += 1;
                            }
                        }
                        else
                        {
                            Vacios += 1;
                        }
                    }
                }
            }
            string[] valores = { Serie_CS, Serie_CI, Total_Series.ToString(), Aciertos.ToString(), Errores.ToString(), Vacios.ToString() };
            return valores;
        }

        /// <summary>
        /// Autocompleta las celdas requeridas en Excel desde la Fila 4 hasta el Final 
        /// </summary>
        /// <param name="SujetosBase"></param>
        public void Autocompletar(SLDocument Libro, int Columna, int Cant_Diccionario)
        {
            for (int Fila = 4; Fila < Cant_Diccionario + 2; Fila++)
            {
                Libro.CopyCell(Fila, Columna, Fila + 1, Columna);
            }
        }


        /* FALTA RESOLVER !!! >>
         * public FileResult Descargar()
        {
            string NombreArchivo = Session["Nombre_Archivo"] as string;
            string Ruta = "../Exportar" + NombreArchivo;

            return File(Ruta, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                      
            string Nombre_Archivo = Session["Nombre_Archivo"] as string;
            string Ruta = "ftp://www.procesosbasicos.somee.com/www.procesosbasicos.somee.com/Exportar/" + Nombre_Archivo;



            context.HttpContext.Response.Buffer = true;
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
            context.HttpContext.Response.WriteFile(context.HttpContext.Server.MapPath(Path));

            
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential("Psico_452", "Psico_452");
            client.DownloadFile(Ruta, @"~\Download\"+ Nombre_Archivo);*/

    }
}


                    