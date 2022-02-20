using ECommerce.Ordering.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Ordering.Infrastructure.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Ordering");

            builder.HasKey(o => o.Id);


            builder.Property(d => d.FullName)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(d => d.Document)
                .HasColumnType("varchar(18)")
                .IsRequired();

            builder.Property(e => e.Phone)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();


            builder.Property(e => e.FirstLine)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(e => e.SecondLine)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(e => e.City)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(e => e.ZipCode)
                .HasColumnType("varchar(9)")
                .IsRequired();

            builder.Property(e => e.State)
                .HasColumnType("char(2)")
                .IsRequired();


            builder.Property(o => o.Value)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(o => o.Status)
                .HasColumnType("char(15)")
                .IsRequired();

            builder.Property(o => o.RegistrationDate)
                .HasColumnType("datetime")
                .IsRequired();
            
            builder.HasMany(c => c.Items);
        }
    }
}
