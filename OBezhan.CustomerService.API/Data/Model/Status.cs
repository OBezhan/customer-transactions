using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OBezhan.CustomerService.API.Data.Model
{
    public class Status
    {
        public byte Id { get; set; }
        public string Name { get; set; }

        public static void Build(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Status> entityTypeBuilder = modelBuilder.Entity<Status>();
            entityTypeBuilder.ToTable("Status");
            entityTypeBuilder.HasKey(t => t.Id);
            entityTypeBuilder.Property(t => t.Name).IsRequired().HasMaxLength(16);
        }
    }
}
