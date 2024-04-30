using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Red_Social_Proyecto.Entities
{
    [Table("publications", Schema ="public")]
    public class PublicationEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("content")]
        [Required]
        [MaxLength(300)]
        public string Content { get; set; }

        [Column("publication_date")]
        [Required]
        public DateTime PublicationDate { get; set; }

        [ForeignKey("UserId")]
        public UsersEntity User { get; set; }

        public virtual ICollection<CommentsEntity> Comments { get; set; }
    }
}
