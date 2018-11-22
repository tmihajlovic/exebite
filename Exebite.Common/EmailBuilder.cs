using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Exebite.Common
{
    public class EmailSender
    {
        public EmailSender()
        {
        }

        private void SendMail(MailAddress from, IEnumerable<MailAddress> to, string subject, string body, IEnumerable<MailAddress> cc)
        {
            using (var message = new MailMessage())
            {

                message.From = from;

                foreach (var item in to)
                {
                    message.To.Add(item);
                }

                message.IsBodyHtml = true;

                foreach (var item in cc)
                {
                    message.CC.Add(item);
                }


                message.Subject = subject;
                message.Body = body;

                using (var smtpClient = new SmtpClient("mail.mydomain.com"))
                {
                    smtpClient.Credentials = new NetworkCredential();
                    smtpClient.Send(message);
                }
            }
        }
    }
}
