using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Red_Social_Proyecto.Dtos.Task
{
    public class InteractionDto
    {
        public Guid Id { get; set; }

        public string TypeInteraction { get; set; }

        public string UserId { get; set; }

        public Guid PublicationId { get; set; }

        public Guid CommentId { get; set; }

        public DateTime InteractionDate { get; set; }
    }
}
