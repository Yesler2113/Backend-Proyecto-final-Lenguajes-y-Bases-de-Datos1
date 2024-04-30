using System.ComponentModel.DataAnnotations;

namespace Red_Social_Proyecto.Dtos.Security
{
    public class LoginDto
    {
        [Display(Name = "Correo Electronico")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public string Password { get; set; }
    }
}
