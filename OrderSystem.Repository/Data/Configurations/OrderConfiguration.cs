using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Core.Enums;
using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Repository.Data.Configurations
{
    /// <summary>
    /// Configures the <see cref="Order"/> entity for Entity Framework.
    /// </summary>
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        /// <summary>
        /// Configures the <see cref="Order"/> entity properties and relationships.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id); // Set Id as primary key

            builder.Property(e => e.OrderDate)
                   .IsRequired(); // OrderDate is required

            builder.Property(o => o.TotalAmount)
                   .IsRequired(); // TotalAmount is required

            builder.Property(o => o.Status)
                .HasConversion(
                    os => os.ToString(), // Convert enum to string for storage
                    os => (OrderStatus)Enum.Parse(typeof(OrderStatus), os)) // Convert string back to enum
                .IsRequired();

            builder.Property(o => o.PaymentMethod)
                .HasConversion(
                    op => op.ToString(), // Convert enum to string for storage
                    op => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), op)) // Convert string back to enum
                .IsRequired();

            builder.HasOne(e => e.Customer) // Configure many-to-one relationship with Customer
                .WithMany(c => c.Orders)
                .HasForeignKey(e => e.CustomerId);
        }
    }
}
