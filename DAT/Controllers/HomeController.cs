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
using System.IO;
using System.Diagnostics;

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
            
                return View("~/Views/Home/Consigna_RA.cshtml"); 
            }
            else{
                MessageBox.Show("El Mail ingresado ya existe. Por favor ingrese otra Dirección de Correo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        public ActionResult CargarRespuestas_RA(string A01, string A02, string A03, string A04, string A05, string A06, string A07, string A08, string A09, string A10, string A11, string A12, string A13, string A14, string A15, string A16, string A17, string RA_TR)
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
            Sujeto.RA_TR = RA_TR;
            Sujeto.FechayHoraSalida = DateTime.Now.ToString(); 

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
        public ActionResult CargarRespuestas_RM(string M01, string M02, string M03, string M04, string M05, string M06, string M07, string M08, string M09, string M10, string M11, string M12, string M13, string M14, string M15, string M16, string M17, string M18, string M19, string M20, string M21, string M22, string M23, string M24, string M25, string M26, string M27, string M28, string M29, string M30, string RM_TR)
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
            Sujeto.RM_TR = RM_TR;
            Sujeto.FechayHoraSalida = DateTime.Now.ToString();

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
        public ActionResult CargarRespuestas_RV(string V01, string V02, string V03, string V04, string V05, string V06, string V07, string V08, string V09, string V10, string V11, string V12, string V13, string V14, string V15, string V16, string V17, string RV_TR)
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
            Sujeto.RV_TR = RV_TR;
            Sujeto.FechayHoraSalida = DateTime.Now.ToString();

            HomeManager Manager = new HomeManager();
            Manager.ActualizarRV(Sujeto);

            return View("~/Views/Home/Bloques.cshtml");
        }

        [HttpPost]
        public ActionResult CargarBloques(string Respuesta_CS, string Puntaje_CS, string CS_TR, string Respuesta_CI, string Puntaje_CI, string CI_TR)
        {
            Sujeto Sujeto = new Sujeto();

            Sujeto.ID = Session["ID_Sujeto"] as string;
            Sujeto.Respuesta_CS = Respuesta_CS;
            Sujeto.Puntaje_CS = Puntaje_CS;
            Sujeto.CS_TR = CS_TR;

            Sujeto.Respuesta_CI = Respuesta_CI;
            Sujeto.Puntaje_CI = Puntaje_CI;
            Sujeto.CI_TR = CI_TR;

            Sujeto.FechayHoraSalida = DateTime.Now.ToString();

            HomeManager Manager = new HomeManager();
            Manager.ActualizarBloques(Sujeto);

            return View("~/Views/Home/Final.cshtml");
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
        /// Toma los datos de la BBDD y los transfiere a un .xls
        /// </summary>
        /// <param name="SujetosBase"></param>
        public void Procesar(Dictionary<int, Sujeto> SujetosBase)
        {
            SLDocument Libro = new SLDocument("C:/Users/Guido/Documents/Visual Studio 2015/Projects/DAT2/DAT/Exportar/Documento_Modelo/Base_de_Datos.xlsx");
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
                Libro.SetCellValue("K" + Fila, Caso.Año);

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
                Libro.SetCellValue("V" + Fila, Caso.RA_11);
                Libro.SetCellValue("W" + Fila, Caso.RA_12);
                Libro.SetCellValue("X" + Fila, Caso.RA_13);
                Libro.SetCellValue("Y" + Fila, Caso.RA_14);
                Libro.SetCellValue("Z" + Fila, Caso.RA_15);
                Libro.SetCellValue("AA" + Fila, Caso.RA_16);
                Libro.SetCellValue("AB" + Fila, Caso.RA_17);
                
                //RAZONAMIENTO MECÁNICO

                Libro.SetCellValue("AC" + Fila, Caso.RM_1);
                Libro.SetCellValue("AD" + Fila, Caso.RM_2);
                Libro.SetCellValue("AE" + Fila, Caso.RM_3);
                Libro.SetCellValue("AF" + Fila, Caso.RM_4);
                Libro.SetCellValue("AG" + Fila, Caso.RM_5);
                Libro.SetCellValue("AH" + Fila, Caso.RM_6);
                Libro.SetCellValue("AI" + Fila, Caso.RM_7);
                Libro.SetCellValue("AJ" + Fila, Caso.RM_8);
                Libro.SetCellValue("AK" + Fila, Caso.RM_9);
                Libro.SetCellValue("AL" + Fila, Caso.RM_10);
                Libro.SetCellValue("AM" + Fila, Caso.RM_11);
                Libro.SetCellValue("AN" + Fila, Caso.RM_12);
                Libro.SetCellValue("AO" + Fila, Caso.RM_13);
                Libro.SetCellValue("AP" + Fila, Caso.RM_14);
                Libro.SetCellValue("AQ" + Fila, Caso.RM_15);
                Libro.SetCellValue("AR" + Fila, Caso.RM_16);
                Libro.SetCellValue("AS" + Fila, Caso.RM_17);
                Libro.SetCellValue("AT" + Fila, Caso.RM_18);
                Libro.SetCellValue("AU" + Fila, Caso.RM_19);
                Libro.SetCellValue("AV" + Fila, Caso.RM_20);
                Libro.SetCellValue("AW" + Fila, Caso.RM_21);
                Libro.SetCellValue("AX" + Fila, Caso.RM_22);
                Libro.SetCellValue("AY" + Fila, Caso.RM_23);
                Libro.SetCellValue("AZ" + Fila, Caso.RM_24);
                Libro.SetCellValue("BA" + Fila, Caso.RM_25);
                Libro.SetCellValue("BB" + Fila, Caso.RM_26);
                Libro.SetCellValue("BC" + Fila, Caso.RM_27);
                Libro.SetCellValue("BD" + Fila, Caso.RM_28);
                Libro.SetCellValue("BE" + Fila, Caso.RM_29);
                Libro.SetCellValue("BF" + Fila, Caso.RM_30);
                
                //RAZONAMIENTO VERBAL

                Libro.SetCellValue("BG" + Fila, Caso.RV_1);
                Libro.SetCellValue("BH" + Fila, Caso.RV_2);
                Libro.SetCellValue("BI" + Fila, Caso.RV_3);
                Libro.SetCellValue("BJ" + Fila, Caso.RV_4);
                Libro.SetCellValue("BK" + Fila, Caso.RV_5);
                Libro.SetCellValue("BL" + Fila, Caso.RV_6);
                Libro.SetCellValue("BM" + Fila, Caso.RV_7);
                Libro.SetCellValue("BN" + Fila, Caso.RV_8);
                Libro.SetCellValue("BO" + Fila, Caso.RV_9);
                Libro.SetCellValue("BP" + Fila, Caso.RV_10);
                Libro.SetCellValue("BQ" + Fila, Caso.RV_11);
                Libro.SetCellValue("BR" + Fila, Caso.RV_12);
                Libro.SetCellValue("BS" + Fila, Caso.RV_13);
                Libro.SetCellValue("BT" + Fila, Caso.RV_14);
                Libro.SetCellValue("BU" + Fila, Caso.RV_15);
                Libro.SetCellValue("BV" + Fila, Caso.RV_16);
                Libro.SetCellValue("BW" + Fila, Caso.RV_17);

                Libro.SetCellValue("BX" + Fila, Caso.Respuesta_CS.Trim());
                Libro.SetCellValue("BY" + Fila, Caso.Respuesta_CI.Trim());

                //TIEMPOS DE REACCIÓN
                Libro.SetCellValueNumeric("BZ" + Fila, Caso.RA_TR.Trim());
                Libro.SetCellValueNumeric("CA" + Fila, Caso.RM_TR.Trim());
                Libro.SetCellValueNumeric("CB" + Fila, Caso.RV_TR.Trim());
                Libro.SetCellValueNumeric("CC" + Fila, Caso.CS_TR.Trim());
                Libro.SetCellValueNumeric("CD" + Fila, Caso.CI_TR.Trim());

                Libro.SetCellValueNumeric("CE" + Fila, Caso.Puntaje_CS);
                Libro.SetCellValueNumeric("CF" + Fila, Caso.Puntaje_CI);

                Libro.SetCellValue("CG" + Fila, Caso.FechayHoraSalida);
                Libro.SetCellValue("CH" + Fila, Caso.Abandono);

            };

            Libro.DeleteRow(Cant_Diccionario + 1, 2);

            //Corrección (Hoja 2 del xls)
            Libro.SelectWorksheet("Corrección");
            Libro.InsertRow(5, Cant_Diccionario - 3);
            Libro.CopyRowStyle(4, 5, Cant_Diccionario - 3);

            //                                     Fila, Columna   A4 =  4, 1
            //                         | Hoja     | Desde|     Hasta          | Destino | Tipo de Pegado    

            Libro.CopyCellFromWorksheet("Original", 2, 1, Cant_Diccionario, 11, 4, 1, 0);    //       Copio A:K
            Libro.CopyCellFromWorksheet("Original", 2, 76, Cant_Diccionario, 77, 4, 76, 0);  //       Copio Respuesta_CS y Respuesta_CI

            Libro.CopyCellFromWorksheet("Original", 2, 78, Cant_Diccionario, 78, 4, 92, 0);  //       RA_TR
            Libro.CopyCellFromWorksheet("Original", 2, 79, Cant_Diccionario, 79, 4, 93, 0);  //       RM_TR
            Libro.CopyCellFromWorksheet("Original", 2, 80, Cant_Diccionario, 80, 4, 94, 0);  //       RV_TR
            Libro.CopyCellFromWorksheet("Original", 2, 81, Cant_Diccionario, 81, 4, 95, 0);  //       CS_TR
            Libro.CopyCellFromWorksheet("Original", 2, 82, Cant_Diccionario, 82, 4, 96, 0);  //       CI_TR

            Libro.CopyCellFromWorksheet("Original", 2, 83, Cant_Diccionario, 83, 4, 82, 0);  //       Puntaje_CS
            Libro.CopyCellFromWorksheet("Original", 2, 84, Cant_Diccionario, 84, 4, 84, 0);  //       Puntaje_CI

            Libro.CopyCellFromWorksheet("Original", 2, 85, Cant_Diccionario, 85, 4, 78, 0);  //       FechayHoraSalida
            
            Libro.CopyCellFromWorksheet("Original", 2, 86, Cant_Diccionario, 86, 4, 98, 0);  //       Abandonó

            //POLÍGONOS
            //Respuesta_CS 4, 76        CI 4,77

            
            
            
            //Autocompletar a partir de la fila 3 hasta el final
            for (int Columna = 12; Columna < 99; Columna++)
            {
                if (Columna < 76)
                {
                    Autocompletar(Libro, Columna, Cant_Diccionario);
                }
                               
                else
                {
                    switch (Columna)
                    {
                        case 79:
                            Autocompletar(Libro, Columna, Cant_Diccionario);
                            break;
                        case 80:
                            Autocompletar(Libro, Columna, Cant_Diccionario);
                            break;
                        case 81:
                            Autocompletar(Libro, Columna, Cant_Diccionario);
                            break;
                        case 87:
                            Autocompletar(Libro, Columna, Cant_Diccionario);
                            break;
                        case 91:
                            Autocompletar(Libro, Columna, Cant_Diccionario);
                            break;
                        case 97:
                            Autocompletar(Libro, Columna, Cant_Diccionario);
                            break;
                        default:
                            break; 
                    }
                    continue;
                }
            }

            //Nombre del Archivo y Descarga
            string Fecha = DateTime.Now.ToString();
            Fecha = Fecha.Replace("/", "-");
            Fecha = Fecha.Replace(":", ".");
            Fecha = Fecha.Remove(15, 3);
            Fecha = Fecha.Replace(" ", "_");

            string Nombre_Archivo = "Base_de_Datos_" + Fecha + ".xlsx";
            string Ruta = Server.MapPath("~/Exportar/" + Nombre_Archivo);
             
            Libro.SaveAs(Ruta);

            ViewBag.Ruta = Ruta;
            ViewBag.Nombre_Archivo = Nombre_Archivo;
        }

        /// <summary>
        /// Autocompleta las celdas requeridas en Excel desde la Fila 4 hasta el Final 
        /// </summary>
        /// <param name="SujetosBase"></param>
        public void Autocompletar(SLDocument Libro,int Columna, int Cant_Diccionario)
        {
            for (int Fila = 4; Fila < Cant_Diccionario + 2; Fila++)
            {
                Libro.CopyCell(Fila, Columna, Fila + 1, Columna);
            }
        }
    }
}


                    