using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Red_Social_Proyecto.Entities
{
    [Table("users", Schema = "security")]
    public class UsersEntity : IdentityUser
    {

        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; }

        [Column("photo_url")]
        [MaxLength(300)]
        public string PhotoUrl { get; set; }

        [Column("biography")]
        [MaxLength(300)]
        public string Biography { get; set; }

        [Column("links")]
        [MaxLength(300)]
        public string SocialMediaLinks { get; set; }

        public virtual ICollection<PublicationEntity> Publications { get; set; }
        public virtual ICollection<CommentsEntity> Comments { get; set; }
        public virtual ICollection<InteractionEntity> Interactions { get; set; }
        public virtual ICollection<FollowEntity> Following { get; set; }
        public virtual ICollection<FollowEntity> Followers { get; set; }

    }
}

