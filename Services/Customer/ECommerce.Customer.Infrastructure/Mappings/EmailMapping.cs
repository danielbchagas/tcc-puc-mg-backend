using ECommerce.Core.Models.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Customer.Infrastructure.Mappings
{
    public class EmailMapping : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.ToTable("Emails");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Address)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.CustomerId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
        }
    }
}
