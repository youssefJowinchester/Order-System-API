using OrderSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Models
{
    /// <summary>
    /// Represents a user in the order management system.
    /// </summary>
    public class User : BaseModel
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the hashed password of the user.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public UserRole Role { get; set; }
    }

}
