using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Red_Social_Proyecto.Dtos.Task
{
    public class NotificationDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string TypeNotification { get; set; }
        
        public string ContentNotification { get; set; }

        public DateTime NotificationDate { get; set; }
    }
}
