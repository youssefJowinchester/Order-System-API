using OrderSystem.Core.Models.Emailllll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Services.Interfaces
{
    /// <summary>
    /// Provides methods to send emails.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="email">The email message to send.</param>
        void SendEmail(EmailMessage email);
    }

}
