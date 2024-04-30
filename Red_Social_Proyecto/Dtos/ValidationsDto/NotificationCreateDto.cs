using System.ComponentModel.DataAnnotations;

namespace Red_Social_Proyecto.Dtos.ValidationsDto
{
    public class NotificationCreateDto
    {
        [Required(ErrorMessage = "El Id del Usuario es Requerido")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "El tipo de notificacion es Requerido")]
        public string TypeNotification { get; set; }

        [Required(ErrorMessage = "El contenido de la notificacion es Requerido")]
        public string ContentNotification { get; set; }

    }
}
