
namespace Red_Social_Proyecto.ResetPassword
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}