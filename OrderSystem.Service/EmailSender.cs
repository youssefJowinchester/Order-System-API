using OrderSystem.Core.Models.Emailllll;
using OrderSystem.Core.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace OrderSystem.Service
{

    /// <summary>
    /// Sends an email using the configured SMTP server.
    /// </summary>
    /// <param name="EM">The email message to send.</param>
    public class EmailSender : IEmailSender
    {

        public void SendEmail(EmailMessage EM)
        {
            // Email Server Gmail
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("youssef99119@gmail.com", "vmapxfpzwvstvaxc")
            };
            client.SendMailAsync("youssef99119@gmail.com", EM.To, EM.Subject, EM.Body);
        }
    }
}
