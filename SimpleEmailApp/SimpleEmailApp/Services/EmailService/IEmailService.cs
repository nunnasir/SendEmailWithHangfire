using SimpleEmailApp.Models;

namespace SimpleEmailApp.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(EmailDto request);
    }
}
