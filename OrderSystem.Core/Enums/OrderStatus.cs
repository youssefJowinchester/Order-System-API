using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Enums
{
    /// <summary>
    /// Represents the status of an order in the order management system.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Indicates that the order is pending.
        /// </summary>
        [EnumMember(Value = "Pending")]
        Pending,

        /// <summary>
        /// Indicates that the order is being processed.
        /// </summary>
        [EnumMember(Value = "Processing")]
        Processing,

        /// <summary>
        /// Indicates that the order has been shipped.
        /// </summary>
        [EnumMember(Value = "Shipped")]
        Shipped,

        /// <summary>
        /// Indicates that the order has been delivered.
        /// </summary>
        [EnumMember(Value = "Delivered")]
        Delivered,

        /// <summary>
        /// Indicates that the order has been cancelled.
        /// </summary>
        [EnumMember(Value = "Cancelled")]
        Cancelled
    }
}
