using System.ComponentModel.DataAnnotations;

namespace Red_Social_Proyecto.Dtos.ValidationsDto
{
    public class InteractionCreateDto
    {
        [Required(ErrorMessage = "El tipo de interacción es requerido")]
        public string TypeInteraction { get; set; }

        [Required(ErrorMessage = "El Id del Usuario es requerido")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "El Id de la Publicación es requerido")]
        public Guid PublicationId { get; set; }

       
        public Guid CommentId { get; set; }

    }
}
