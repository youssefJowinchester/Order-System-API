using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Models.Emailllll
{
    /// <summary>
    /// Represents an email message to be sent.
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// Gets or sets the recipient email address.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the subject of the email.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body content of the email.
        /// </summary>
        public string Body { get; set; }
    }
}
