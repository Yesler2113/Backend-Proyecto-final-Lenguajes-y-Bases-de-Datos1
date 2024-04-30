using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Red_Social_Proyecto.Entities
{
    [Table("notifications", Schema ="public")]
    public class NotificationEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("type_notification")]
        [Required]
        [MaxLength(100)]
        public string TypeNotification { get; set; }

        [Column("content_notification")]
        [Required]
        [MaxLength(300)]
        public string ContentNotification { get; set; }

        [Column("notification_date")]
        [Required]
        public DateTime NotificationDate { get; set; }

        [ForeignKey("UserId")]
        public UsersEntity User { get; set; }
    }
}
