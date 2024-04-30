using System.ComponentModel.DataAnnotations;

namespace Red_Social_Proyecto.Dtos.ValidationsDto
{
    public class FollowCreateDto
    {
        [Required(ErrorMessage = "El Id del Usuario seguidor es requerido")]
        public string UserId { get; set; }
    }
}
