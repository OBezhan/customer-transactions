using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace OBezhan.CustomerService.API.Data.Model
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public static void Build(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Customer> entityTypeBuilder = modelBuilder.Entity<Customer>();
            entityTypeBuilder.ToTable("Customer");
            entityTypeBuilder.HasKey(t => t.Id);
            entityTypeBuilder.Property(t => t.Name).IsRequired().HasMaxLength(30);
            entityTypeBuilder.Property(t => t.Email).IsRequired().HasMaxLength(25);
            entityTypeBuilder.Property(t => t.MobileNumber).HasMaxLength(10);

            entityTypeBuilder.HasMany(t => t.Transactions).WithOne().HasForeignKey(t => t.CustomerId);
        }
    }


}
