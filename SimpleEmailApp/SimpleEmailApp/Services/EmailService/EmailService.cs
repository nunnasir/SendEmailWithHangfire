using Hangfire;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using SimpleEmailApp.Models;

namespace SimpleEmailApp.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmail(EmailDto request)
        {
            try
            {
                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "EmailTemplate.html");

                var template = await File.ReadAllTextAsync(templatePath);
                var emailBody = string.Format(template, request.Subject, request.RecipientName, request.Body);

                for (int i = 0; i < 100; i++)
                {
                    BackgroundJob.Enqueue(() => SendToClients(request.To, request.Subject, emailBody));
                }

                //BackgroundJob.Enqueue(() => SendToClients(request.To, request.Subject, emailBody));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SendToClients(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            //smtp.Authenticate("cyrus88@ethereal.email", "H6BKY4yZxE1p6UEh1X");
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
