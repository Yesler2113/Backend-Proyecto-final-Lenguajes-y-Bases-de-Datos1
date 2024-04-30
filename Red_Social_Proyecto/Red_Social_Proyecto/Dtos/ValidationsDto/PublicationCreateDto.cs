using System.ComponentModel.DataAnnotations;

namespace Red_Social_Proyecto.Dtos.ValidationsDto
{
    public class PublicationCreateDto
    {
        [Required(ErrorMessage = "El Id del Usuario es Requerido")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "El contenido de la publicacion es Requerido")]
        public string Content { get; set; }
    }
}
