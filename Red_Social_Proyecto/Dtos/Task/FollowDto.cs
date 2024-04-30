using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Red_Social_Proyecto.Dtos.Task
{
    public class FollowDto
    {
        public Guid Id { get; set; }

        public string FollowerId { get; set; }

        public string FollowedId { get; set; }

        public DateTime FollowDate { get; set; }
    }
}
