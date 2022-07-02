using Configuration;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Services.DomainService
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task Send(string email, string content)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 25);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("andrey03072000@gmail.com", "7798929aQ");
                client.EnableSsl = true;

                var mail = new MailMessage("andrey03072000@gmail.com", email);

                using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(content)))
                {
                    Attachment attachment = new Attachment(stream, new ContentType("text/csv"));
                    attachment.Name = "result.csv";
                    mail.Attachments.Add(attachment);
                }

                mail.Subject = "Information alert";
                mail.Body = string.Format("infarmation about your subscription");
                mail.IsBodyHtml = true;

                await client.SendMailAsync(mail);
            }
            catch
            {
                Console.WriteLine("Not Send");
            }
        }
    }
}

