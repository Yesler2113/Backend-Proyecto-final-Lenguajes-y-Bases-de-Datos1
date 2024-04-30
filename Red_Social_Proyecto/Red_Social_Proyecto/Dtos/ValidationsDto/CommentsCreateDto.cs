using System.ComponentModel.DataAnnotations;

namespace Red_Social_Proyecto.Dtos.ValidationsDto
{
    public class CommentsCreateDto
    {

        [Required(ErrorMessage = "El Id del Usuario es requerido")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "El Id de la Publicación es requerido")]
        public Guid PublicationId { get; set; }

        public Guid? CommentParentId { get; set; }

        [Required(ErrorMessage = "El contenido del comentario es requerido")]
        public string Content { get; set; }
    }
}
