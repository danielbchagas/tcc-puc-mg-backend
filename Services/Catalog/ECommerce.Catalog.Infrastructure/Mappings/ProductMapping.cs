using ECommerce.Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Catalog.Infrastructure.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Description)
                .HasColumnType("varchar(2000)")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.Quantity)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Image)
                .HasColumnType("text")
                .IsRequired(false);

            builder.Property(p => p.Value)
                .HasColumnType("money")
                .IsRequired();

            builder.HasQueryFilter(p => p.DeletedAt == null);
        }
    }
}
