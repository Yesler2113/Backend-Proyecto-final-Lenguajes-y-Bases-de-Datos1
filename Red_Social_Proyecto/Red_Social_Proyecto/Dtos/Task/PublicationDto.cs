namespace Red_Social_Proyecto.Dtos.Task
{
    public class PublicationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
