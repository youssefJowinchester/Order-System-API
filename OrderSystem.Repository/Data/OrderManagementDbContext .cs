using Microsoft.EntityFrameworkCore;
using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Repository.Data
{
    /// <summary>
    /// Represents the database context for the Order Management System.
    /// </summary>
    public class OrderManagementDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderManagementDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options)
            : base(options) { }

        /// <summary>
        /// Configures the model for the context using the specified <see cref="ModelBuilder"/>.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Apply configurations from the assembly

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Gets or sets the Customers DbSet.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Gets or sets the Orders DbSet.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the OrderItems DbSet.
        /// </summary>
        public DbSet<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Gets or sets the Products DbSet.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the Invoices DbSet.
        /// </summary>
        public DbSet<Invoice> Invoices { get; set; }

        /// <summary>
        /// Gets or sets the Users DbSet.
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
