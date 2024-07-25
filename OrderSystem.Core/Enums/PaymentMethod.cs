using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Enums
{
    /// <summary>
    /// Represents the available payment methods in the order management system.
    /// </summary>
    public enum PaymentMethod
    {
        /// <summary>
        /// Payment made using a credit card.
        /// </summary>
        [EnumMember(Value = "CreditCard")]
        CreditCard,

        /// <summary>
        /// Payment made using PayPal.
        /// </summary>
        [EnumMember(Value = "PayPal")]
        PayPal,

        /// <summary>
        /// Payment made in cash.
        /// </summary>
        [EnumMember(Value = "Cash")]
        Cash
    }
}
