namespace Red_Social_Proyecto.Dtos.Security
{
    public class LoginResponseDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
