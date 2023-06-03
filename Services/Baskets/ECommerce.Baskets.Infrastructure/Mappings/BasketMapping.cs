using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Basket.Infrastructure.Mappings
{
    public class BasketMapping : IEntityTypeConfiguration<Domain.Models.Basket>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Basket> builder)
        {
            builder.ToTable("Baskets");

            builder.HasKey(cc => cc.Id);

            builder.HasIndex(cc => cc.CustomerId);

            builder.Property(cc => cc.Value)
                .HasColumnType("money");

            builder.HasMany(cc => cc.Items);

            builder.HasQueryFilter(cc => cc.DeletedAt == null || !cc.IsEnded);
        }
    }
}
