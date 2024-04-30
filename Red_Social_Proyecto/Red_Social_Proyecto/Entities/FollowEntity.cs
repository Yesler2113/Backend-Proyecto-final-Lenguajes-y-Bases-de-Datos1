using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Red_Social_Proyecto.Entities
{
    [Table("follows", Schema ="public")]
    public class FollowEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("follower_id")]
        [Required]
        public string FollowerId { get; set; }

        [Column("followed_id")]
        [Required]
        public string FollowedId { get; set; }

        [Column("follow_date")]
        [Required]
        public DateTime FollowDate { get; set; }

        [ForeignKey("FollowerId")]
        public UsersEntity Follower { get; set; }

        [ForeignKey("FollowedId")]
        public UsersEntity Followed { get; set; }
    }
}
