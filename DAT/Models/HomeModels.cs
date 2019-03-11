namespace DAT.Models
{
    public class Sujeto
    {
        public string FechayHora { get; set; }
        public string ID { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Mail { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public string Carrera { get; set; }
        public string Universidad { get; set; }
        public string Cuatrimestre { get; set; }
        public string Año { get; set; }

        /* Respuestas de Razonamiento Abstracto */

        public string RA_1 { get; set; }
        public string RA_2 { get; set; }
        public string RA_3 { get; set; }
        public string RA_4 { get; set; }
        public string RA_5 { get; set; }
        public string RA_6 { get; set; }
        public string RA_7 { get; set; }
        public string RA_8 { get; set; }
        public string RA_9 { get; set; }
        public string RA_10 { get; set; }
        public string RA_11 { get; set; }
        public string RA_12 { get; set; }
        public string RA_13 { get; set; }
        public string RA_14 { get; set; }
        public string RA_15 { get; set; }
        public string RA_16 { get; set; }
        public string RA_17 { get; set; }

        /* Respuestas de Razonamiento Mecánico */

        public string RM_1 { get; set; }
        public string RM_2 { get; set; }
        public string RM_3 { get; set; }
        public string RM_4 { get; set; }
        public string RM_5 { get; set; }
        public string RM_6 { get; set; }
        public string RM_7 { get; set; }
        public string RM_8 { get; set; }
        public string RM_9 { get; set; }
        public string RM_10 { get; set; }
        public string RM_11 { get; set; }
        public string RM_12 { get; set; }
        public string RM_13 { get; set; }
        public string RM_14 { get; set; }
        public string RM_15 { get; set; }
        public string RM_16 { get; set; }
        public string RM_17 { get; set; }
        public string RM_18 { get; set; }
        public string RM_19 { get; set; }
        public string RM_20 { get; set; }
        public string RM_21 { get; set; }
        public string RM_22 { get; set; }
        public string RM_23 { get; set; }
        public string RM_24 { get; set; }
        public string RM_25 { get; set; }
        public string RM_26 { get; set; }
        public string RM_27 { get; set; }
        public string RM_28 { get; set; }
        public string RM_29 { get; set; }
        public string RM_30 { get; set; }

        /* Respuestas de Razonamiento Verbal */

        public string RV_1 { get; set; }
        public string RV_2 { get; set; }
        public string RV_3 { get; set; }
        public string RV_4 { get; set; }
        public string RV_5 { get; set; }
        public string RV_6 { get; set; }
        public string RV_7 { get; set; }
        public string RV_8 { get; set; }
        public string RV_9 { get; set; }
        public string RV_10 { get; set; }
        public string RV_11 { get; set; }
        public string RV_12 { get; set; }
        public string RV_13 { get; set; }
        public string RV_14 { get; set; }
        public string RV_15 { get; set; }
        public string RV_16 { get; set; }
        public string RV_17 { get; set; }

        /* Bloques de Corsi */
        public string Respuesta_CS { get; set; }
        public string Puntaje_CS { get; set; }
        public string Respuesta_CI { get; set; }
        public string Puntaje_CI { get; set; }
        
        /*Puntajes RA, RM, RV[Sólo para los Resultados del Sujeto] */
        public string Puntaje_RA { get; set; }
        public string Puntaje_RM { get; set; }
        public string Puntaje_RV { get; set; }

        /* Tiempos de Reacción */
        public string RA_TR { get; set; }
        public string RM_TR { get; set; }
        public string RV_TR { get; set; }
        public string CS_TR { get; set; }
        public string CI_TR { get; set; }

        public string Abandono { get; set; }
        public string FechayHoraSalida { get; set; }

    }

}