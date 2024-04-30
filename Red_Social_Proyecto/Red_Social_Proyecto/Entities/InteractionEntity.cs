using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Red_Social_Proyecto.Entities
{
    [Table("interactions", Schema ="public")]
    public class InteractionEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("type_interaction")]
        [Required]
        [MaxLength(10)]
        public string TypeInteraction { get; set; }

        [Column("user_id")]
        [Required]
        public string UserId { get; set; }

        [Column("publication_id")]
        [Required]
        public Guid PublicationId { get; set; }

        [Column("comment_id")]
        [Required]
        public Guid CommentId { get; set; }

        [Column("interaction_date")]
        [Required]
        public DateTime InteractionDate { get; set; }

        [ForeignKey("UserId")]
        public UsersEntity User { get; set; }

        [ForeignKey("PublicationId")]
        public PublicationEntity Publication { get; set; }

        [ForeignKey("CommentId")]
        public CommentsEntity Comment { get; set; }


        public virtual ICollection<InteractionEntity> Interactions { get; set; }

    }
}
