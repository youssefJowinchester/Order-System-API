using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Repository.Data.Configurations
{
    /// <summary>
    /// Configures the <see cref="OrderItem"/> entity for Entity Framework.
    /// </summary>
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        /// <summary>
        /// Configures the <see cref="OrderItem"/> entity properties and relationships.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id); // Set Id as primary key

            builder.Property(oi => oi.Quantity)
                   .IsRequired(); // Quantity is required

            builder.Property(oi => oi.UnitPrice)
                   .IsRequired(); // UnitPrice is required

            builder.HasOne(oi => oi.Order) // Configure many-to-one relationship with Order
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(oi => oi.OrderId);

            builder.HasOne(oi => oi.Product) // Configure many-to-one relationship with Product
                   .WithMany()
                   .HasForeignKey(oi => oi.ProductId);
        }
    }
}
