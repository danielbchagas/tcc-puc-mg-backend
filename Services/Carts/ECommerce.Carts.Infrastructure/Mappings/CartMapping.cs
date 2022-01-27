using ECommerce.Carts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Carts.Infrastructure.Mappings
{
    public class CartMapping : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(cc => cc.Id);

            builder.HasIndex(cc => cc.CustomerId);

            builder.Property(cc => cc.Value)
                .HasColumnType("money");

            builder.HasMany(cc => cc.Itens)
                .WithOne(ic => ic.Cart)
                .HasForeignKey(ic => ic.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
