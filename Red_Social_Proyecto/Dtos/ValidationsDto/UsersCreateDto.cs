using System.ComponentModel.DataAnnotations;

namespace Red_Social_Proyecto.Dtos.ValidationsDto
{
    public class UsersCreateDto
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, ErrorMessage = "La contraseña debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        public string Password { get; set; }

        public string PhotoUrl { get; set; }

        [Required(ErrorMessage = "La biografía es requerida")]
        public string Biography { get; set; }

        public string SocialMediaLinks { get; set; }
    }
}

