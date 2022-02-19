using ECommerce.Basket.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Basket.Infrastructure.Mappings
{
    public class ShoppingBasketMapping : IEntityTypeConfiguration<ShoppingBasket>
    {
        public void Configure(EntityTypeBuilder<ShoppingBasket> builder)
        {
            builder.ToTable("Baskets");

            builder.HasKey(cc => cc.Id);

            builder.HasIndex(cc => cc.CustomerId);

            builder.Property(cc => cc.Value)
                .HasColumnType("money");

            builder.HasMany(cc => cc.Items)
                .WithOne(ic => ic.ShoppingBasket)
                .HasForeignKey(ic => ic.ShoppingBasketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
