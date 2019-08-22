using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OBezhan.CustomerService.API.Data.Model
{
    public class Currency
    {
        public short Id { get; set; }
        public string Code { get; set; }

        public static void Build(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Currency> entityTypeBuilder = modelBuilder.Entity<Currency>();
            entityTypeBuilder.ToTable("Currency");
            entityTypeBuilder.HasKey(t => t.Id);
            entityTypeBuilder.Property(t => t.Code).IsRequired().HasMaxLength(3);
        }
    }
}
