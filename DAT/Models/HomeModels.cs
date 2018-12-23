using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

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
    /*public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña nueva")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme la contraseña nueva")]
        [Compare("NewPassword", ErrorMessage = "La contraseña nueva y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña nueva")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme la contraseña nueva")]
        [Compare("NewPassword", ErrorMessage = "La contraseña nueva y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Número de teléfono")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Número de teléfono")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}*/