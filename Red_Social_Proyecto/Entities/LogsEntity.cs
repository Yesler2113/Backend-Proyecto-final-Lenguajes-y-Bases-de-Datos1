namespace Red_Social_Proyecto.Entities
{
    public class LogsEntity
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? Action { get; set; }
        public DateTime Date { get; set; }
    }
}
