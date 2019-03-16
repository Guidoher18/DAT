using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace DAT.Models
{
    public class HomeManager
    {
        /// <summary>
        /// Consulta si el Email ya fue insertado en la BBDD
        /// </summary>
        /// <param name="Mail"></param>
        /// <returns></returns>
        [HttpPost]
        public string Consultar(string Mail)

        {
            var Email = Mail;
            try
            {
                SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBase"]);
                Conexion.Open();
                SqlCommand Sentencia = Conexion.CreateCommand();
                Sentencia.CommandText = "SELECT * FROM DAT_RA WHERE Mail = '" + Email + "'";
                string Consulta = Sentencia.ExecuteScalar().ToString();
                Conexion.Close();
                return Consulta;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        /// <summary>
        /// Inserta al Sujeto y sus Respuestas en la Base de Datos
        /// </summary>
        /// <param name="Sujeto"></param>
        /// <returns>Sujeto</returns>
        [HttpPost]
        public Sujeto Insertar(Sujeto Sujeto)
        {
            //Conexion a DAT (BBDD)
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBase"]);

            //Inicio la Conexion
            Conexion.Open();

            //Creo el Objeto que me permite ingresar la instancia
            SqlCommand Sentencia = Conexion.CreateCommand();

            //Escribo la Sentencia SQL
            Sentencia.CommandText = "INSERT INTO DAT_RA (FechayHora, Apellido, Nombre, Mail, Genero, Edad, Carrera, Universidad, Cuatrimestre, Año, RA_1, RA_2, RA_3, RA_4, RA_5, RA_6, RA_7, RA_8, RA_9, RA_10, RA_TR, FechayHoraSalida, Abandono) OUTPUT INSERTED.ID VALUES(@FechayHora, @Apellido, @Nombre, @Mail, @Genero, @Edad, @Carrera, @Universidad, @Cuatrimestre, @Año, @RA_1, @RA_2, @RA_3, @RA_4, @RA_5, @RA_6, @RA_7, @RA_8, @RA_9, @RA_10, @RA_TR, @FechayHoraSalida, @Abandono)";

            //Vinculo las variables con los parámetros
            Sentencia.Parameters.AddWithValue("@FechayHora", Sujeto.FechayHora);
            Sentencia.Parameters.AddWithValue("@Apellido", Sujeto.Apellido);
            Sentencia.Parameters.AddWithValue("@Nombre", Sujeto.Nombre);
            Sentencia.Parameters.AddWithValue("@Mail", Sujeto.Mail);
            Sentencia.Parameters.AddWithValue("@Genero", Sujeto.Genero);
            Sentencia.Parameters.AddWithValue("@Edad", Sujeto.Edad);
            Sentencia.Parameters.AddWithValue("@Carrera", Sujeto.Carrera);
            Sentencia.Parameters.AddWithValue("@Universidad", Sujeto.Universidad);
            Sentencia.Parameters.AddWithValue("@Cuatrimestre", Sujeto.Cuatrimestre);
            Sentencia.Parameters.AddWithValue("@Año", Sujeto.Año);
            Sentencia.Parameters.AddWithValue("@RA_1", Sujeto.RA_1);
            Sentencia.Parameters.AddWithValue("@RA_2", Sujeto.RA_2);
            Sentencia.Parameters.AddWithValue("@RA_3", Sujeto.RA_3);
            Sentencia.Parameters.AddWithValue("@RA_4", Sujeto.RA_4);
            Sentencia.Parameters.AddWithValue("@RA_5", Sujeto.RA_5);
            Sentencia.Parameters.AddWithValue("@RA_6", Sujeto.RA_6);
            Sentencia.Parameters.AddWithValue("@RA_7", Sujeto.RA_7);
            Sentencia.Parameters.AddWithValue("@RA_8", Sujeto.RA_8);
            Sentencia.Parameters.AddWithValue("@RA_9", Sujeto.RA_9);
            Sentencia.Parameters.AddWithValue("@RA_10", Sujeto.RA_10);
            Sentencia.Parameters.AddWithValue("@RA_TR", Sujeto.RA_TR);
            Sentencia.Parameters.AddWithValue("@FechayHoraSalida", Sujeto.FechayHoraSalida);
            Sentencia.Parameters.AddWithValue("@Abandono", Sujeto.Abandono);

            //Ejecuto
            Sujeto.ID = Sentencia.ExecuteScalar().ToString();

            //Cierro la Conexión
            Conexion.Close();

            return Sujeto;
        }

        /// <summary>
        /// Actualiza al Sujeto Creado con Insertar, agregando las respuestas en RM
        /// </summary>
        /// <param name="Sujeto"></param>
        /// <returns>Sujeto</returns>
        [HttpPost]
        public Sujeto ActualizarRM(Sujeto Sujeto)
        {
            //Conexion a DAT (BBDD)
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBase"]);

            //Inicio la Conexion
            Conexion.Open();

            //Creo el Objeto que me permite ingresar la instancia
            SqlCommand Sentencia = Conexion.CreateCommand();

            //Escribo la Sentencia SQL
            Sentencia.CommandText = "UPDATE DAT_RA SET RM_1= @RM_1, RM_2= @RM_2, RM_3=@RM_3, RM_4=@RM_4, RM_5=@RM_5, RM_6=@RM_6, RM_7=@RM_7, RM_8=@RM_8, RM_9=@RM_9, RM_10=@RM_10, RM_11=@RM_11, RM_12=@RM_12, RM_13=@RM_13, RM_14=@RM_14, RM_15=@RM_15, RM_16=@RM_16, RM_17=@RM_17, RM_18=@RM_18, RM_TR=@RM_TR, FechayHoraSalida=@FechayHoraSalida, Abandono=@Abandono WHERE ID=@ID;";

            //Convierto Sujeto.ID (string) en Int 
            var a = Sujeto.ID;
            var ID = int.Parse(a);

            //Vinculo las variables con los parámetros
            Sentencia.Parameters.AddWithValue("@ID", ID);
            Sentencia.Parameters.AddWithValue("@RM_1", Sujeto.RM_1);
            Sentencia.Parameters.AddWithValue("@RM_2", Sujeto.RM_2);
            Sentencia.Parameters.AddWithValue("@RM_3", Sujeto.RM_3);
            Sentencia.Parameters.AddWithValue("@RM_4", Sujeto.RM_4);
            Sentencia.Parameters.AddWithValue("@RM_5", Sujeto.RM_5);
            Sentencia.Parameters.AddWithValue("@RM_6", Sujeto.RM_6);
            Sentencia.Parameters.AddWithValue("@RM_7", Sujeto.RM_7);
            Sentencia.Parameters.AddWithValue("@RM_8", Sujeto.RM_8);
            Sentencia.Parameters.AddWithValue("@RM_9", Sujeto.RM_9);
            Sentencia.Parameters.AddWithValue("@RM_10", Sujeto.RM_10);
            Sentencia.Parameters.AddWithValue("@RM_11", Sujeto.RM_11);
            Sentencia.Parameters.AddWithValue("@RM_12", Sujeto.RM_12);
            Sentencia.Parameters.AddWithValue("@RM_13", Sujeto.RM_13);
            Sentencia.Parameters.AddWithValue("@RM_14", Sujeto.RM_14);
            Sentencia.Parameters.AddWithValue("@RM_15", Sujeto.RM_15);
            Sentencia.Parameters.AddWithValue("@RM_16", Sujeto.RM_16);
            Sentencia.Parameters.AddWithValue("@RM_17", Sujeto.RM_17);
            Sentencia.Parameters.AddWithValue("@RM_18", Sujeto.RM_18);
            Sentencia.Parameters.AddWithValue("@RM_TR", Sujeto.RM_TR);
            Sentencia.Parameters.AddWithValue("@FechayHoraSalida", Sujeto.FechayHoraSalida);
            Sentencia.Parameters.AddWithValue("@Abandono", Sujeto.Abandono);

            //Ejecuto
            Sentencia.ExecuteNonQuery();

            //Cierro la Conexión
            Conexion.Close();

            return Sujeto;
        }

        /// <summary>
        /// Actualiza al Sujeto Creado con Insertar, agregando las respuestas en RV
        /// </summary>
        /// <param name="Sujeto"></param>
        /// <returns>Sujeto</returns>
        [HttpPost]
        public Sujeto ActualizarRV(Sujeto Sujeto)
        {
            //Conexion a DAT (BBDD)
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBase"]);

            //Inicio la Conexion
            Conexion.Open();

            //Creo el Objeto que me permite ingresar la instancia
            SqlCommand Sentencia = Conexion.CreateCommand();

            //Escribo la Sentencia SQL
            Sentencia.CommandText = "UPDATE DAT_RA SET RV_1=@RV_1, RV_2=@RV_2, RV_3=@RV_3, RV_4=@RV_4, RV_5=@RV_5, RV_6=@RV_6, RV_7=@RV_7, RV_8=@RV_8, RV_9=@RV_9, RV_10=@RV_10, RV_TR=@RV_TR, FechayHoraSalida=@FechayHoraSalida, Abandono=@Abandono  WHERE ID=@ID;";

            //Convierto Sujeto.ID (string) en Int 
            var a = Sujeto.ID;
            var ID = int.Parse(a);

            //Vinculo las variables con los parámetros
            Sentencia.Parameters.AddWithValue("@ID", ID);
            Sentencia.Parameters.AddWithValue("@RV_1", Sujeto.RV_1);
            Sentencia.Parameters.AddWithValue("@RV_2", Sujeto.RV_2);
            Sentencia.Parameters.AddWithValue("@RV_3", Sujeto.RV_3);
            Sentencia.Parameters.AddWithValue("@RV_4", Sujeto.RV_4);
            Sentencia.Parameters.AddWithValue("@RV_5", Sujeto.RV_5);
            Sentencia.Parameters.AddWithValue("@RV_6", Sujeto.RV_6);
            Sentencia.Parameters.AddWithValue("@RV_7", Sujeto.RV_7);
            Sentencia.Parameters.AddWithValue("@RV_8", Sujeto.RV_8);
            Sentencia.Parameters.AddWithValue("@RV_9", Sujeto.RV_9);
            Sentencia.Parameters.AddWithValue("@RV_10", Sujeto.RV_10);
            Sentencia.Parameters.AddWithValue("@RV_TR", Sujeto.RV_TR);
            Sentencia.Parameters.AddWithValue("@FechayHoraSalida", Sujeto.FechayHoraSalida);
            Sentencia.Parameters.AddWithValue("@Abandono", Sujeto.Abandono);

            //Ejecuto
            Sentencia.ExecuteNonQuery();

            //Cierro la Conexión
            Conexion.Close();

            return Sujeto;
        }

        [HttpPost]
        public Sujeto ActualizarBloques(Sujeto Sujeto)
        {
            //Conexion a DAT (BBDD)
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBase"]);

            //Inicio la Conexion
            Conexion.Open();

            //Creo el Objeto que me permite ingresar la instancia
            SqlCommand Sentencia = Conexion.CreateCommand();

            //Escribo la Sentencia SQL
            Sentencia.CommandText = "UPDATE DAT_RA SET Respuesta_CS=@Respuesta_CS, Puntaje_CS=@Puntaje_CS, CS_TR=@CS_TR, Respuesta_CI=@Respuesta_CI, Puntaje_CI=@Puntaje_CI, CI_TR=@CI_TR, FechayHoraSalida=@FechayHoraSalida, Abandono=@Abandono WHERE ID=@ID;";

            //Convierto Sujeto.ID (string) en Int 
            var a = Sujeto.ID;
            var ID = int.Parse(a);

            //Vinculo las variables con los parámetros
            Sentencia.Parameters.AddWithValue("@ID", ID);
            Sentencia.Parameters.AddWithValue("@Respuesta_CS", Sujeto.Respuesta_CS);
            Sentencia.Parameters.AddWithValue("@Puntaje_CS", Sujeto.Puntaje_CS);
            Sentencia.Parameters.AddWithValue("@CS_TR", Sujeto.CS_TR);
            Sentencia.Parameters.AddWithValue("@Respuesta_CI", Sujeto.Respuesta_CI);
            Sentencia.Parameters.AddWithValue("@Puntaje_CI", Sujeto.Puntaje_CI);
            Sentencia.Parameters.AddWithValue("@CI_TR", Sujeto.CI_TR);
            Sentencia.Parameters.AddWithValue("@FechayHoraSalida", Sujeto.FechayHoraSalida);
            Sentencia.Parameters.AddWithValue("@Abandono", Sujeto.Abandono);

            //Ejecuto
            Sentencia.ExecuteNonQuery();

            //Cierro la Conexión
            Conexion.Close();

            return Sujeto;
        }

        /// <summary>
        /// Consulta los registros cargados en la BBDD
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPost]
        public Dictionary<int, Sujeto> LeerRegistros()
        {
            //Almacena los datos de cada Sujeto 
            Dictionary<int, Sujeto> SujetosBase = new Dictionary<int, Sujeto>();
            int Key = 0;

            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBase"]);
            Conexion.Open();
            SqlCommand Sentencia = Conexion.CreateCommand();
            Sentencia.CommandText = "SELECT * FROM DAT_RA ORDER BY ID";

            SqlDataReader reader = Sentencia.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    //Almacena un Sujeto sin propiedades Null luego de la ConsultaSql
                    Dictionary<string, string> SinNull = new Dictionary<string, string>();

                    string[] Propiedades = {
                        "RA_1", "RA_2", "RA_3", "RA_4", "RA_5", "RA_6", "RA_7", "RA_8", "RA_9", "RA_10", "RA_11", "RA_12", "RA_13", "RA_14", "RA_15", "RA_16", "RA_17",
                        "RM_1", "RM_2", "RM_3", "RM_4", "RM_5", "RM_6", "RM_7", "RM_8", "RM_9", "RM_10", "RM_11", "RM_12", "RM_13", "RM_14", "RM_15", "RM_16", "RM_17", "RM_18", "RM_19", "RM_20", "RM_21", "RM_22", "RM_23", "RM_24", "RM_25", "RM_26", "RM_27", "RM_28", "RM_29", "RM_30",
                        "RV_1", "RV_2", "RV_3", "RV_4", "RV_5", "RV_6", "RV_7", "RV_8", "RV_9", "RV_10", "RV_11", "RV_12", "RV_13", "RV_14", "RV_15", "RV_16", "RV_17",
                        "Respuesta_CS", "Respuesta_CI", "RA_TR", "RM_TR", "RV_TR", "CS_TR", "CI_TR", "Puntaje_CS", "Puntaje_CI", "FechayHoraSalida", "Abandono"
                    };

                    //Reemplaza los Null por "" y los almacena en el diccionario SinNull
                    foreach (string X in Propiedades)
                    {
                        if (reader[X] != null && reader[X] != DBNull.Value)
                        {
                            SinNull.Add(X, (string)reader[X]);
                        }
                        else
                        {                        
                            SinNull.Add(X, "");
                        }
                    }

                    Sujeto Sujeto = new Sujeto();

                    Sujeto.FechayHora = reader["FechayHora"].ToString();
                    Sujeto.ID = reader["ID"].ToString();
                    Sujeto.Apellido = (string)reader["Apellido"];
                    Sujeto.Nombre = (string)reader["Nombre"];
                    Sujeto.Mail = (string)reader["Mail"];
                    Sujeto.Genero = (string)reader["Genero"];
                    Sujeto.Edad = (int)reader["Edad"];
                    Sujeto.Carrera = (string)reader["Carrera"];
                    Sujeto.Universidad = (string)reader["Universidad"];
                    Sujeto.Cuatrimestre = (string)reader["Cuatrimestre"];
                    Sujeto.Año = (string)reader["Año"];

                    Sujeto.RA_1 = SinNull["RA_1"];
                    Sujeto.RA_2 = SinNull["RA_2"];
                    Sujeto.RA_3 = SinNull["RA_3"];
                    Sujeto.RA_4 = SinNull["RA_4"];
                    Sujeto.RA_5 = SinNull["RA_5"];
                    Sujeto.RA_6 = SinNull["RA_6"];
                    Sujeto.RA_7 = SinNull["RA_7"];
                    Sujeto.RA_8 = SinNull["RA_8"];
                    Sujeto.RA_9 = SinNull["RA_9"];
                    Sujeto.RA_10 = SinNull["RA_10"];
                    /*Sujeto.RA_11 = SinNull["RA_11"];
                    Sujeto.RA_12 = SinNull["RA_12"];
                    Sujeto.RA_13 = SinNull["RA_13"];
                    Sujeto.RA_14 = SinNull["RA_14"];  ARREglaR
                    Sujeto.RA_15 = SinNull["RA_15"];
                    Sujeto.RA_16 = SinNull["RA_16"];
                    Sujeto.RA_17 = SinNull["RA_17"];*/

                    Sujeto.RM_1 = SinNull["RM_1"];
                    Sujeto.RM_2 = SinNull["RM_2"];
                    Sujeto.RM_3 = SinNull["RM_3"];
                    Sujeto.RM_4 = SinNull["RM_4"];
                    Sujeto.RM_5 = SinNull["RM_5"];
                    Sujeto.RM_6 = SinNull["RM_6"];
                    Sujeto.RM_7 = SinNull["RM_7"];
                    Sujeto.RM_8 = SinNull["RM_8"];
                    Sujeto.RM_9 = SinNull["RM_9"];
                    Sujeto.RM_10 = SinNull["RM_10"];
                    Sujeto.RM_11 = SinNull["RM_11"];
                    Sujeto.RM_12 = SinNull["RM_12"];
                    Sujeto.RM_13 = SinNull["RM_13"];
                    Sujeto.RM_14 = SinNull["RM_14"];
                    Sujeto.RM_15 = SinNull["RM_15"];
                    Sujeto.RM_16 = SinNull["RM_16"];
                    Sujeto.RM_17 = SinNull["RM_17"];
                    Sujeto.RM_18 = SinNull["RM_18"];
                   /* Sujeto.RM_19 = SinNull["RM_19"];
                    Sujeto.RM_20 = SinNull["RM_20"];
                    Sujeto.RM_21 = SinNull["RM_21"];
                    Sujeto.RM_22 = SinNull["RM_22"];
                    Sujeto.RM_23 = SinNull["RM_23"];
                    Sujeto.RM_24 = SinNull["RM_24"];
                    Sujeto.RM_25 = SinNull["RM_25"];
                    Sujeto.RM_26 = SinNull["RM_26"];      ARREGLAR
                    Sujeto.RM_27 = SinNull["RM_27"];
                    Sujeto.RM_28 = SinNull["RM_28"];
                    Sujeto.RM_29 = SinNull["RM_29"];
                    Sujeto.RM_30 = SinNull["RM_30"]; */

                    Sujeto.RV_1 = SinNull["RV_1"];
                    Sujeto.RV_2 = SinNull["RV_2"];
                    Sujeto.RV_3 = SinNull["RV_3"];
                    Sujeto.RV_4 = SinNull["RV_4"];
                    Sujeto.RV_5 = SinNull["RV_5"];
                    Sujeto.RV_6 = SinNull["RV_6"];
                    Sujeto.RV_7 = SinNull["RV_7"];
                    Sujeto.RV_8 = SinNull["RV_8"];
                    Sujeto.RV_9 = SinNull["RV_9"];
                    Sujeto.RV_10 = SinNull["RV_10"];
                   /* Sujeto.RV_11 = SinNull["RV_11"];
                    Sujeto.RV_12 = SinNull["RV_12"];
                    Sujeto.RV_13 = SinNull["RV_13"];
                    Sujeto.RV_14 = SinNull["RV_14"];    ARREGLAR
                    Sujeto.RV_15 = SinNull["RV_15"];
                    Sujeto.RV_16 = SinNull["RV_16"];
                    Sujeto.RV_17 = SinNull["RV_17"]; */

                    Sujeto.Respuesta_CS = SinNull["Respuesta_CS"];
                    Sujeto.Respuesta_CI = SinNull["Respuesta_CI"];

                    Sujeto.RA_TR = SinNull["RA_TR"];
                    Sujeto.RM_TR = SinNull["RM_TR"];
                    Sujeto.RV_TR = SinNull["RV_TR"];
                    Sujeto.CS_TR = SinNull["CS_TR"];
                    Sujeto.CI_TR = SinNull["CI_TR"];

                    Sujeto.Puntaje_CS = SinNull["Puntaje_CS"];
                    Sujeto.Puntaje_CI = SinNull["Puntaje_CI"];
                    Sujeto.FechayHoraSalida = SinNull["FechayHoraSalida"];
                    Sujeto.Abandono = SinNull["Abandono"];

                    Key = Key + 1;
                    SujetosBase.Add(Key, Sujeto);
                }

                catch (NullReferenceException)
                {
                    break;
                }
            }
            reader.Close();
            Conexion.Close();
            return SujetosBase;
        }

        /// <summary>
        /// Consulta un registro particular según ID, cargado en la BBDD
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPost]
        public Sujeto Leer_un_registro(string ID)
        {
            Sujeto Sujeto = new Sujeto();

            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBase"]);
            Conexion.Open();
            SqlCommand Sentencia = Conexion.CreateCommand();
            Sentencia.CommandText = "SELECT * FROM DAT_RA WHERE ID=" + ID.ToString();

            SqlDataReader reader = Sentencia.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    //Almacena un Sujeto sin propiedades Null luego de la ConsultaSql
                    Dictionary<string, string> SinNull = new Dictionary<string, string>();

                    string[] Propiedades = {
                        "RA_1", "RA_2", "RA_3", "RA_4", "RA_5", "RA_6", "RA_7", "RA_8", "RA_9", "RA_10", "RA_11", "RA_12", "RA_13", "RA_14", "RA_15", "RA_16", "RA_17",
                        "RM_1", "RM_2", "RM_3", "RM_4", "RM_5", "RM_6", "RM_7", "RM_8", "RM_9", "RM_10", "RM_11", "RM_12", "RM_13", "RM_14", "RM_15", "RM_16", "RM_17", "RM_18", "RM_19", "RM_20", "RM_21", "RM_22", "RM_23", "RM_24", "RM_25", "RM_26", "RM_27", "RM_28", "RM_29", "RM_30",
                        "RV_1", "RV_2", "RV_3", "RV_4", "RV_5", "RV_6", "RV_7", "RV_8", "RV_9", "RV_10", "RV_11", "RV_12", "RV_13", "RV_14", "RV_15", "RV_16", "RV_17",
                        "Respuesta_CS", "Respuesta_CI", "RA_TR", "RM_TR", "RV_TR", "CS_TR", "CI_TR", "Puntaje_CS", "Puntaje_CI"// Sin "FechayHoraSalida" ni "Abandono"
                    };

                    //Reemplaza los Null por "" y los almacena en el diccionario SinNull
                    foreach (string X in Propiedades)
                    {
                        if (reader[X] != null && reader[X] != DBNull.Value)
                        {
                            SinNull.Add(X, (string)reader[X]);
                        }
                        else
                        {                        
                            SinNull.Add(X, "");
                        }
                    }

                    Sujeto.ID = ID;
                    Sujeto.Apellido = (string)reader["Apellido"];
                    Sujeto.Nombre = (string)reader["Nombre"];
                    Sujeto.Genero = (string)reader["Genero"];
                    Sujeto.Edad = (int)reader["Edad"];
                    Sujeto.Carrera = (string)reader["Carrera"];
                    Sujeto.Universidad = (string)reader["Universidad"];
                    Sujeto.Cuatrimestre = (string)reader["Cuatrimestre"];
                    Sujeto.Año = (string)reader["Año"];
                    
                    int Puntaje_RA = 0;
                    int Puntaje_RM = 0;
                    int Puntaje_RV = 0;
                    
                    //CORRECCIÓN RA
                    if (SinNull["RA_1"] == "D"){Puntaje_RA+=1;}
                    if (SinNull["RA_2"] == "D"){Puntaje_RA+=1;}
                    if (SinNull["RA_3"] == "C"){Puntaje_RA+=1;}
                    if (SinNull["RA_4"] == "D"){Puntaje_RA+=1;}
                    
                    if (SinNull["RA_5"] == "C"){Puntaje_RA+=1;}
                    if (SinNull["RA_6"] == "D"){Puntaje_RA+=1;}
                    if (SinNull["RA_7"] == "A"){Puntaje_RA+=1;}
                    if (SinNull["RA_8"] == "B"){Puntaje_RA+=1;}
                    
                    if (SinNull["RA_9"] == "E"){Puntaje_RA+=1;}
                    if (SinNull["RA_10"] == "B"){Puntaje_RA+=1;}
                    if (SinNull["RA_11"] == "A"){Puntaje_RA+=1;}
                    if (SinNull["RA_12"] == "A"){Puntaje_RA+=1;}
                    
                    if (SinNull["RA_13"] == "A"){Puntaje_RA+=1;}
                    if (SinNull["RA_14"] == "D"){Puntaje_RA+=1;}
                    if (SinNull["RA_15"] == "A"){Puntaje_RA+=1;}
                    if (SinNull["RA_16"] == "E"){Puntaje_RA+=1;}
                    
                    if (SinNull["RA_17"] == "B"){Puntaje_RA+=1;}

                    //Corrección RM
                    if (SinNull["RM_1"] == "C"){Puntaje_RM+=1;}
                    if (SinNull["RM_2"] == "C"){Puntaje_RM+=1;}
                    if (SinNull["RM_3"] == "A"){Puntaje_RM+=1;}
                    if (SinNull["RM_4"] == "B"){Puntaje_RM+=1;}
                    
                    if (SinNull["RM_5"] == "C"){Puntaje_RM+=1;}
                    if (SinNull["RM_6"] == "B"){Puntaje_RM+=1;}
                    if (SinNull["RM_7"] == "A"){Puntaje_RM+=1;}
                    if (SinNull["RM_8"] == "B"){Puntaje_RM+=1;}
                    
                    if (SinNull["RM_9"] == "C"){Puntaje_RM+=1;}
                    if (SinNull["RM_10"] == "B"){Puntaje_RM+=1;}
                    if (SinNull["RM_11"] == "B"){Puntaje_RM+=1;}
                    if (SinNull["RM_12"] == "B"){Puntaje_RM+=1;}
                    
                    if (SinNull["RM_13"] == "A"){Puntaje_RM+=1;}
                    if (SinNull["RM_14"] == "B"){Puntaje_RM+=1;}
                    if (SinNull["RM_15"] == "B"){Puntaje_RM+=1;}
                    if (SinNull["RM_16"] == "B"){Puntaje_RM+=1;}
                    
                    if (SinNull["RM_17"] == "A"){Puntaje_RM+=1;}
                    if (SinNull["RM_18"] == "B"){Puntaje_RM+=1;}
                    if (SinNull["RM_19"] == "B"){Puntaje_RM+=1;}
                    if (SinNull["RM_20"] == "A"){Puntaje_RM+=1;}
                    
                    if (SinNull["RM_21"] == "A"){Puntaje_RM+=1;}
                    if (SinNull["RM_22"] == "A"){Puntaje_RM+=1;}
                    if (SinNull["RM_23"] == "C"){Puntaje_RM+=1;}
                    if (SinNull["RM_24"] == "B"){Puntaje_RM+=1;}
                    
                    if (SinNull["RM_25"] == "B"){Puntaje_RM+=1;}
                    if (SinNull["RM_26"] == "B"){Puntaje_RM+=1;}
                    if (SinNull["RM_27"] == "B"){Puntaje_RM+=1;}
                    if (SinNull["RM_28"] == "C"){Puntaje_RM+=1;}
                    
                    if (SinNull["RM_29"] == "B"){Puntaje_RM+=1;}
                    if (SinNull["RM_30"] == "B"){Puntaje_RM+=1;}

                    //Corrección RV
                    if (SinNull["RV_1"] == "C"){Puntaje_RV+=1;}
                    if (SinNull["RV_2"] == "C"){Puntaje_RV+=1;}
                    if (SinNull["RV_3"] == "E"){Puntaje_RV+=1;}
                    if (SinNull["RV_4"] == "D"){Puntaje_RV+=1;}
                    
                    if (SinNull["RV_5"] == "B"){Puntaje_RV+=1;}
                    if (SinNull["RV_6"] == "D"){Puntaje_RV+=1;}
                    if (SinNull["RV_7"] == "C"){Puntaje_RV+=1;}
                    if (SinNull["RV_8"] == "B"){Puntaje_RV+=1;}
                    
                    if (SinNull["RV_9"] == "E"){Puntaje_RV+=1;}
                    if (SinNull["RV_10"] == "B"){Puntaje_RV+=1;}
                    if (SinNull["RV_11"] == "C"){Puntaje_RV+=1;}
                    if (SinNull["RV_12"] == "B"){Puntaje_RV+=1;}
                    
                    if (SinNull["RV_13"] == "A"){Puntaje_RV+=1;}
                    if (SinNull["RV_14"] == "E"){Puntaje_RV+=1;}
                    if (SinNull["RV_15"] == "B"){Puntaje_RV+=1;}
                    if (SinNull["RV_16"] == "B"){Puntaje_RV+=1;}
                    
                    if (SinNull["RV_17"] == "D"){Puntaje_RV+=1;}

                    Sujeto.Puntaje_RA = Puntaje_RA.ToString();
                    Sujeto.Puntaje_RM = Puntaje_RM.ToString();
                    Sujeto.Puntaje_RV = Puntaje_RV.ToString();

                    Sujeto.Puntaje_CS = SinNull["Puntaje_CS"];
                    Sujeto.Puntaje_CI = SinNull["Puntaje_CI"];

                    Sujeto.RA_TR = SinNull["RA_TR"];
                    Sujeto.RM_TR = SinNull["RM_TR"];
                    Sujeto.RV_TR = SinNull["RV_TR"];
                    Sujeto.CS_TR = SinNull["CS_TR"];
                    Sujeto.CI_TR = SinNull["CI_TR"];
                }
                catch (NullReferenceException)
                {
                    break;
                }
            }

            reader.Close();
            Conexion.Close();
            return Sujeto;
        }
    }
}