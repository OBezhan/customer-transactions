using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace OBezhan.CustomerService.API.Data.Model
{
    public class Transaction
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public decimal Amount { get; set; }
        public short CurrencyId { get; set; }
        public byte StatusId { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Status Status { get; set; }

        public static void Build(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Transaction> entityTypeBuilder = modelBuilder.Entity<Transaction>();
            entityTypeBuilder.ToTable("Transaction");
            entityTypeBuilder.HasKey(t => t.Id);
            entityTypeBuilder.Property(t => t.CustomerId).IsRequired();
            entityTypeBuilder.Property(t => t.DateTimeUtc).IsRequired();
            entityTypeBuilder.Property(t => t.Amount).IsRequired();
            entityTypeBuilder.Property(t => t.CurrencyId).IsRequired();
            entityTypeBuilder.Property(t => t.StatusId).IsRequired();

            entityTypeBuilder.HasOne(t => t.Currency).WithMany().HasForeignKey(t => t.CurrencyId);
            entityTypeBuilder.HasOne(t => t.Status).WithMany().HasForeignKey(t => t.StatusId);
        }
    }
}
