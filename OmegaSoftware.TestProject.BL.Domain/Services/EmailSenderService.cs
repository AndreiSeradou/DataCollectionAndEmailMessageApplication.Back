using Microsoft.Extensions.Options;
using OmegaSoftware.TestProject.BL.Domain.Configuration;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace OmegaSoftware.TestProject.BL.Domain.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailConfig _emailConfig;

        public EmailSenderService(IOptionsMonitor<EmailConfig> optionsMonitor)
        {
            _emailConfig = optionsMonitor.CurrentValue;
        }

        public async Task Send(string email, string content)
        {

            try
            {
                SmtpClient client = new SmtpClient(ApplicationConfiguration.MailSmtp, ApplicationConfiguration.Port);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(ApplicationConfiguration.QuartzEmail, _emailConfig.EmailPassword);
                client.EnableSsl = true;

                var mail = new MailMessage(ApplicationConfiguration.QuartzEmail, email);

                using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(content)))
                {
                    Attachment attachment = new Attachment(stream, new ContentType(ApplicationConfiguration.FileFormat));
                    attachment.Name = ApplicationConfiguration.FileName;
                    mail.Attachments.Add(attachment);
                }

                mail.Subject = ApplicationConfiguration.MailSubject;
                mail.Body = ApplicationConfiguration.EmailMessage;
                mail.IsBodyHtml = true;

                await client.SendMailAsync(mail);
            }
            catch
            {
                Console.WriteLine(ApplicationConfiguration.ErrorSend);
            }
        }
    }
}

