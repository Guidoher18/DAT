using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAT.Models
{
    public class HomeManager
    {
        /// <summary>
        /// Inserta al Sujeto y sus Respuestas en la Base de Datos
        /// </summary>
        /// <param name="Sujeto"></param>
        /// <returns></returns>
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
            Sentencia.CommandText = "INSERT INTO DAT_RA (Apellido, Nombre, Mail, Sexo, Edad, RA_1, RA_2, RA_3, RA_4, RA_5, RA_6, RA_7, RA_8, RA_9, RA_10, RA_11, RA_12, RA_13, RA_14, RA_15, RA_16, RA_17) OUTPUT INSERTED.ID VALUES(@Apellido, @Nombre, @Mail, @Sexo, @Edad, @RA_1, @RA_2, @RA_3, @RA_4, @RA_5, @RA_6, @RA_7, @RA_8, @RA_9, @RA_10, @RA_11, @RA_12, @RA_13, @RA_14, @RA_15, @RA_16, @RA_17)";

            //Vinculo las variables con los parámetros
            Sentencia.Parameters.AddWithValue("@Apellido", Sujeto.Apellido);
            Sentencia.Parameters.AddWithValue("@Nombre", Sujeto.Nombre);
            Sentencia.Parameters.AddWithValue("@Mail", Sujeto.Mail);
            Sentencia.Parameters.AddWithValue("@Sexo", Sujeto.Sexo);
            Sentencia.Parameters.AddWithValue("@Edad", Sujeto.Edad);
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
            Sujeto.ID = Sentencia.ExecuteNonQuery().ToString();

            //Cierro la Conexión
            Conexion.Close();

            return Sujeto;
        }
        /// <summary>
        /// Consulta si el Email ya fue insertado en la BBDD
        /// </summary>
        /// <param name="Mail"></param>
        /// <returns></returns>
        [HttpPost]
        public string Consultar(string Mail) {
            var Email = Mail;
            try
            {
                SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBase"]);
                Conexion.Open();
                SqlCommand Sentencia = Conexion.CreateCommand();
                Sentencia.CommandText = "SELECT * FROM DAT_RA WHERE (Mail = '" + Email + "')";
                string Consulta = Sentencia.ExecuteScalar().ToString();
                Conexion.Close();
                return Consulta;
            }
            catch (NullReferenceException) {
                return null;
            }
        }

    }
}