using api2.Commons;
using Library.Functions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace api2.Services
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailOptions _appSettings;

        public EmailService(IOptions<EmailOptions> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public void Send(string to, string subject, string html, string from = null)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? new AES().Decryption(_appSettings.From)));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_appSettings.SmtpServer, _appSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(new AES().Decryption(_appSettings.From), new AES().Decryption(_appSettings.Password));
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
