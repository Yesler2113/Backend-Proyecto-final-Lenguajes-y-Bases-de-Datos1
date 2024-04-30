using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Red_Social_Proyecto.Entities
{
    [Table("comments", Schema = "public")]
    public class CommentsEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("publication_id")]
        public Guid PublicationId { get; set; }

        [Column("comment_parent_id")]
        public Guid? CommentParentId { get; set; }

        [Column("content")]
        [Required]
        [MaxLength(300)]
        public string Content { get; set; }

        [Column("comment_date")]
        public DateTime CommentDate { get; set; }

        [ForeignKey("UserId")]
        public UsersEntity User { get; set; }

        [ForeignKey("PublicationId")]
        public PublicationEntity Publication { get; set; }

        [ForeignKey("CommentParentId")]
        public CommentsEntity CommentParent { get; set; }

        public virtual CommentsEntity ParentComment { get; set; }
        public virtual ICollection<CommentsEntity> ChildComments { get; set; }

    }
}
