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
    /// Configures the <see cref="Product"/> entity for Entity Framework.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Configures the <see cref="Product"/> entity properties.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100); // Name is required with a max length of 100

            builder.Property(p => p.Stock)
                   .IsRequired(); // Stock is required

            builder.Property(p => p.Price)
                   .IsRequired(); // Price is required
        }
    }
}
