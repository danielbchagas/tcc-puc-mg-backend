using ECommerce.Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Customers.Infrastructure.Mappings
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
        }
    }
}
