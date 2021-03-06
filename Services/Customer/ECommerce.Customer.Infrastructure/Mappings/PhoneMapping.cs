using ECommerce.Customer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Customer.Infrastructure.Mappings
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

            builder.Property(e => e.UserId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
        }
    }
}
