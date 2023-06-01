using ECommerce.Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Customers.Infrastructure.Mappings
{
    public class PhoneMapping : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("Phones");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Number)
                .HasColumnType("varchar(20)")
                .IsRequired();
        }
    }
}
