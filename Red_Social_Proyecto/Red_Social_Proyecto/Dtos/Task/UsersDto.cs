namespace Red_Social_Proyecto.Dtos.Task
{
    public class UsersDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } 
        public string UserName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PhotoUrl { get; set; }
        public string Biography { get; set; }
        public string SocialMediaLinks { get; set; }
    }
}

