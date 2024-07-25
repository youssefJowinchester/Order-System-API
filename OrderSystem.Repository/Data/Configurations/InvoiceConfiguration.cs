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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Configures the <see cref="Invoice"/> entity for Entity Framework.
    /// </summary>
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        /// <summary>
        /// Configures the <see cref="Invoice"/> entity properties and relationships.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(e => e.Id); // Set Id as primary key

            builder.Property(e => e.InvoiceDate)
                   .IsRequired(); // InvoiceDate is required

            builder.Property(e => e.TotalAmount)
                   .IsRequired(); // TotalAmount is required

            builder.HasOne(e => e.Order) // Configure one-to-one relationship with Order
                   .WithOne()
                   .HasForeignKey<Invoice>(e => e.OrderId);
        }
    }

}
