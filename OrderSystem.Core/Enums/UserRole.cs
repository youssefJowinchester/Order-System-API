using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Enums
{
    /// <summary>
    /// Represents the roles assigned to users in the system.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Indicates an administrator role with full access.
        /// </summary>
        [EnumMember(Value = "Admin")]
        Admin,

        /// <summary>
        /// Indicates a customer role with limited access.
        /// </summary>
        [EnumMember(Value = "Customer")]
        Customer
    }
}
