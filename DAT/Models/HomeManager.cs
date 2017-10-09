using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAT.Models
{
    public class HomeManager
    {
        /// <summary>
        /// Inserta un Sujeto Nuevo en la Base de Datos
        /// </summary>
        /// <param name="Sujeto"></param>
        /// <returns></returns>

        public Sujeto Insertar(Sujeto Sujeto) {
            //Conexion a DAT (BBDD)
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBase"]);

            //Inicio la Conexion
            Conexion.Open();

            //Creo el Objeto que me permite ingresar la instancia
            SqlCommand Sentencia = Conexion.CreateCommand();

            //Escribo la Sentencia SQL
            Sentencia.CommandText = "INSERT INTO DAT_RA (Apellido, Nombre, Mail, Sexo, Edad) OUTPUT INSERTED.ID VALUES(@Apellido, @Nombre, @Mail, @Sexo, @Edad)";

            //Vinculo las variables con los parámetros
            Sentencia.Parameters.AddWithValue("@Apellido", Sujeto.Apellido);
            Sentencia.Parameters.AddWithValue("@Nombre", Sujeto.Nombre);
            Sentencia.Parameters.AddWithValue("@Mail", Sujeto.Mail);
            Sentencia.Parameters.AddWithValue("@Sexo", Sujeto.Sexo);
            Sentencia.Parameters.AddWithValue("@Edad", Sujeto.Edad);

            //Ejecuto
            Sujeto.ID = Sentencia.ExecuteNonQuery().ToString();

            //Cierro la Conexión
            Conexion.Close();

            return Sujeto;
        }

        /// <summary>
        /// Inserta las respuestas del Raz Abstracto del Sujeto
        /// </summary>
        /// <param name="Respuestas"></param>
        /// <returns></returns>

        /*public Abstracto InsertarRespuestas(Abstracto Respuestas)
        {
            //Conexion a DAT (BBDD)
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBase"]);

            //Inicio la Conexion
            Conexion.Open();

            //Creo el Objeto que me permite ingresar la instancia
            SqlCommand Sentencia = Conexion.CreateCommand();

            //Escribo la Sentencia SQL
            Sentencia.CommandText = "UPDATE DAT Where ID (Apellido, Nombre, Mail, Sexo, Edad) OUTPUT INSERTED.ID VALUES(@Apellido, @Nombre, @Mail, @Sexo, @Edad)";

            //Vinculo las variables con los parámetros
            Sentencia.Parameters.AddWithValue("@Apellido", Sujeto.Apellido);
            Sentencia.Parameters.AddWithValue("@Nombre", Sujeto.Nombre);
            Sentencia.Parameters.AddWithValue("@Mail", Sujeto.Mail);
            Sentencia.Parameters.AddWithValue("@Sexo", Sujeto.Sexo);
            Sentencia.Parameters.AddWithValue("@Edad", Sujeto.Edad);

            //Ejecuto
            Sujeto.ID = Sentencia.ExecuteScalar().ToString();

            //Cierro la Conexión
            Conexion.Close();

            return Sujeto;
        }*/
    }
}