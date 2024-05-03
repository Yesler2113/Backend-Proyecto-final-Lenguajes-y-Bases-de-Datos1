using SendGrid.Helpers.Mail;
using SendGrid;

namespace Red_Social_Proyecto.ResetPassword.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly string _sendGridKey;
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _sendGridKey = _configuration["SendGridKey"]!;
            if (string.IsNullOrEmpty(_sendGridKey))
            {
                throw new ArgumentException("SendGrid key not found in configuration.");
            }
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(subject, htmlMessage, email);
        }

        private async Task Execute(string subject, string message, string email)
        {
            try
            {
                var client = new SendGridClient(_sendGridKey);
                var msg = new SendGridMessage
                {
                    From = new EmailAddress("diazyes2001@gmail.com", "RedSocial"),
                    Subject = subject,
                    PlainTextContent = message,
                    HtmlContent = message
                };
                msg.AddTo(new EmailAddress(email));
                msg.SetClickTracking(false, false);

                var response = await client.SendEmailAsync(msg);
                Console.WriteLine($"Email sent. Status: {response.StatusCode}");


                await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                
                throw new InvalidOperationException("Error al enviar el email", ex);
            }
        }

    }

}
