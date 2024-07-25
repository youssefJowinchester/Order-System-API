using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
    /// Configures the <see cref="Customer"/> entity for Entity Framework.
    /// </summary>
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        /// <summary>
        /// Configures the <see cref="Customer"/> entity properties and relationships.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id); // Set Id as primary key

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(50); // Name is required with a max length of 50

            builder.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(50); // Email is required with a max length of 50

            builder.HasMany(c => c.Orders) // Configure one-to-many relationship
                   .WithOne(o => o.Customer)
                   .HasForeignKey(o => o.CustomerId);
        }
    }

}
