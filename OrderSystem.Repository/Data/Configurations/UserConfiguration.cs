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
    /// Configures the <see cref="User"/> entity for Entity Framework.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configures the <see cref="User"/> entity properties and relationships.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id); // Set Id as primary key

            builder.Property(e => e.Username)
                   .IsRequired()
                   .HasMaxLength(100); // Username is required with a max length of 100

            builder.Property(e => e.PasswordHash)
                   .IsRequired(); // PasswordHash is required

            builder.Property(e => e.Role)
                   .HasConversion(
                       ur => ur.ToString(), // Convert enum to string for storage
                       ur => (UserRole)Enum.Parse(typeof(UserRole), ur)) // Convert string back to enum
                   .IsRequired(); // Role is required
        }
    }
}
