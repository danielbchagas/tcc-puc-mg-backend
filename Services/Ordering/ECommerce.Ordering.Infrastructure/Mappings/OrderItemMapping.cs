using ECommerce.Core.Models.Ordering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Ordering.Infrastructure.Mappings
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("Items");

            builder.HasKey(p => p.Id);

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
        }
    }
}
