using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Red_Social_Proyecto.Dtos.Task
{
    public class CommentsDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid PublicationId { get; set; }
        public Guid? CommentParentId { get; set; } 
        public string Content { get; set; }

        public DateTime CommentDate { get; set; }
    }
}
