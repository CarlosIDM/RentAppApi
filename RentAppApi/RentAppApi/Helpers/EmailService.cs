using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace RentAppApi.Helpers
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;

        public EmailService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task Send(string mail, string subject, string data, TypeEmail typeEmail)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_appSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(mail));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = Body(data, typeEmail) };
                using var smtp = new SmtpClient();
                smtp.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_appSettings.SmtpUser, _appSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {

            }
        }

        private string Body(string data,TypeEmail typeEmail)
        {
            try
            {
                if (typeEmail == TypeEmail.Code)
                {
                    return "";
                }
                else
                    return "";
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}

