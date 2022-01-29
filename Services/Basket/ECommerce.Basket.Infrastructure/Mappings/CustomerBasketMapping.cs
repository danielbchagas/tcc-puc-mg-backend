using ECommerce.Basket.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Basket.Infrastructure.Mappings
{
    public class CustomerBasketMapping : IEntityTypeConfiguration<CustomerBasket>
    {
        public void Configure(EntityTypeBuilder<CustomerBasket> builder)
        {
            builder.ToTable("Baskets");

            builder.HasKey(cc => cc.Id);

            builder.HasIndex(cc => cc.CustomerId);

            builder.Property(cc => cc.Value)
                .HasColumnType("money");

            builder.HasMany(cc => cc.Items)
                .WithOne(ic => ic.CustomerBasket)
                .HasForeignKey(ic => ic.CustomerBasketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
