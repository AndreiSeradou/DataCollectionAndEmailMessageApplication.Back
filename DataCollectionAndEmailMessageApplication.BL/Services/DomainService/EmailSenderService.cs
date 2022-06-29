using Configuration;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Services.DomainService
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task SendEmail()
        {




            Console.WriteLine("dwd");
                    
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 25);

                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("andrey03072000@gmail.com", "7798929aQ");
                        client.EnableSsl = true;

                        var mail = new MailMessage("andrey03072000@gmail.com", "maksastapmakslook@gmail.com");

                        mail.Subject = "test";
                        mail.Body = string.Format("hello my bad");
                        mail.IsBodyHtml = true;

                        await client.SendMailAsync(mail);

            Console.WriteLine("wd10000");
            
        }
    }
}

