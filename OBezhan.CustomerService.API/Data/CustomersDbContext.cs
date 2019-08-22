using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OBezhan.CustomerService.API.Data.Model;
using OBezhan.CustomerService.API.Infrastructure.Persistent;
using System;

namespace OBezhan.CustomerService.API.Data
{
    public class CustomersDbContext : DbContext
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public DbSet<Customer> Customers { get; private set; }

        public CustomersDbContext(IOptions<DatabaseOptions> databaseOptions)
        {
            if (databaseOptions.Value == null)
            {
                throw new ArgumentNullException(nameof(databaseOptions));
            }

            if (string.IsNullOrEmpty(databaseOptions.Value.ConnectionString))
            {
                throw new ArgumentException("Database connection string is null or empty.");
            }

            _databaseOptions = databaseOptions;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseOptions.Value.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Currency.Build(modelBuilder);
            Status.Build(modelBuilder);
            Transaction.Build(modelBuilder);
            Customer.Build(modelBuilder);
        }
    }
}
