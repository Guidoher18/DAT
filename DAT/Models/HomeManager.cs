﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
                Sentencia.CommandText = "SELECT * FROM DAT_RA WHERE (Mail = " + Email + ")";
                string Consulta = Sentencia.ExecuteScalar().ToString();
                Conexion.Close();
                return Consulta;
            }
            catch (NullReferenceException) {
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
            Sentencia.CommandText = "INSERT INTO DAT_RA (Apellido, Nombre, Mail, Sexo, Edad, Carrera, Universidad, RA_1, RA_2, RA_3, RA_4, RA_5, RA_6, RA_7, RA_8, RA_9, RA_10, RA_11, RA_12, RA_13, RA_14, RA_15, RA_16, RA_17) OUTPUT INSERTED.ID VALUES(@Apellido, @Nombre, @Mail, @Sexo, @Edad, @Carrera, @Universidad, @RA_1, @RA_2, @RA_3, @RA_4, @RA_5, @RA_6, @RA_7, @RA_8, @RA_9, @RA_10, @RA_11, @RA_12, @RA_13, @RA_14, @RA_15, @RA_16, @RA_17)";

            //Vinculo las variables con los parámetros
            Sentencia.Parameters.AddWithValue("@Apellido", Sujeto.Apellido);
            Sentencia.Parameters.AddWithValue("@Nombre", Sujeto.Nombre);
            Sentencia.Parameters.AddWithValue("@Mail", Sujeto.Mail);
            Sentencia.Parameters.AddWithValue("@Sexo", Sujeto.Sexo);
            Sentencia.Parameters.AddWithValue("@Edad", Sujeto.Edad);
            Sentencia.Parameters.AddWithValue("@Carrera", Sujeto.Carrera);
            Sentencia.Parameters.AddWithValue("@Universidad", Sujeto.Universidad);
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
            Sentencia.Parameters.AddWithValue("@RA_11", Sujeto.RA_11);
            Sentencia.Parameters.AddWithValue("@RA_12", Sujeto.RA_12);
            Sentencia.Parameters.AddWithValue("@RA_13", Sujeto.RA_13);
            Sentencia.Parameters.AddWithValue("@RA_14", Sujeto.RA_14);
            Sentencia.Parameters.AddWithValue("@RA_15", Sujeto.RA_15);
            Sentencia.Parameters.AddWithValue("@RA_16", Sujeto.RA_16);
            Sentencia.Parameters.AddWithValue("@RA_17", Sujeto.RA_17);

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
            Sentencia.CommandText = "UPDATE DAT_RA SET RM_1= @RM_1, RM_2= @RM_2, RM_3=@RM_3, RM_4=@RM_4, RM_5=@RM_5, RM_6=@RM_6, RM_7=@RM_7, RM_8=@RM_8, RM_9=@RM_9, RM_10=@RM_10, RM_11=@RM_11, RM_12=@RM_12, RM_13=@RM_13, RM_14=@RM_14, RM_15=@RM_15, RM_16=@RM_16, RM_17=@RM_17, RM_18=@RM_18, RM_19=@RM_19, RM_20=@RM_20, RM_21=@RM_21, RM_22=@RM_22, RM_23=@RM_23, RM_24=@RM_24, RM_25=@RM_25, RM_26=@RM_26, RM_27=@RM_27, RM_28=@RM_28, RM_29=@RM_29, RM_30=@RM_30 WHERE ID=@ID;";

            //Convierto Sujeto.ID (string) en Int 
            var a = Sujeto.ID;
            var ID = int.Parse(a);

            //Vinculo las variables con los parámetros
            /*Sentencia.Parameters.Add("@ID", SqlDbType.Int).Value = ID;;
            Sentencia.Parameters.Add("@RM_1", SqlDbType.Char).Value = Sujeto.RM_1;
            Sentencia.Parameters.Add("@RM_2", SqlDbType.Char).Value = Sujeto.RM_2;
            */
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
            Sentencia.Parameters.AddWithValue("@RM_19", Sujeto.RM_19);
            Sentencia.Parameters.AddWithValue("@RM_20", Sujeto.RM_20);
            Sentencia.Parameters.AddWithValue("@RM_21", Sujeto.RM_21);
            Sentencia.Parameters.AddWithValue("@RM_22", Sujeto.RM_22);
            Sentencia.Parameters.AddWithValue("@RM_23", Sujeto.RM_23);
            Sentencia.Parameters.AddWithValue("@RM_24", Sujeto.RM_24);
            Sentencia.Parameters.AddWithValue("@RM_25", Sujeto.RM_25);
            Sentencia.Parameters.AddWithValue("@RM_26", Sujeto.RM_26);
            Sentencia.Parameters.AddWithValue("@RM_27", Sujeto.RM_27);
            Sentencia.Parameters.AddWithValue("@RM_28", Sujeto.RM_28);
            Sentencia.Parameters.AddWithValue("@RM_29", Sujeto.RM_29);
            Sentencia.Parameters.AddWithValue("@RM_30", Sujeto.RM_30);
            
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
            Sentencia.CommandText = "UPDATE DAT_RA SET RV_1=@RV_1, RV_2=@RV_2, RV_3=@RV_3, RV_4=@RV_4, RV_5=@RV_5, RV_6=@RV_6, RV_7=@RV_7, RV_8=@RV_8, RV_9=@RV_9, RV_10=@RV_10, RV_11=@RV_11, RV_12=@RV_12, RV_13=@RV_13, RV_14=@RV_14, RV_15=@RV_15, RV_16=@RV_16, RV_17=@RV_17 WHERE ID=@ID;";

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
            Sentencia.Parameters.AddWithValue("@RV_11", Sujeto.RV_11);
            Sentencia.Parameters.AddWithValue("@RV_12", Sujeto.RV_12);
            Sentencia.Parameters.AddWithValue("@RV_13", Sujeto.RV_13);
            Sentencia.Parameters.AddWithValue("@RV_14", Sujeto.RV_14);
            Sentencia.Parameters.AddWithValue("@RV_15", Sujeto.RV_15);
            Sentencia.Parameters.AddWithValue("@RV_16", Sujeto.RV_16);
            Sentencia.Parameters.AddWithValue("@RV_17", Sujeto.RV_17);

            //Ejecuto
            Sentencia.ExecuteNonQuery();

            //Cierro la Conexión
            Conexion.Close();

            return Sujeto;
        }
    }
}