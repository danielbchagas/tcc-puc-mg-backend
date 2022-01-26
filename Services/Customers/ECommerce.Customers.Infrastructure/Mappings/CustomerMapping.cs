using ECommerce.Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Customers.Infrastructure.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.Enabled)
                .HasColumnType("bit")
                .IsRequired();

            builder.HasOne(c => c.Document)
                .WithOne(d => d.Customer)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Email)
                .WithOne(e => e.Customer)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Phone)
                .WithOne(t => t.Customer)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Address)
                .WithOne(e => e.Customer)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
