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

            builder.HasOne(c => c.Document)
                .WithOne(c => c.Customer)
                .HasForeignKey<Document>(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Email)
                .WithOne(c => c.Customer)
                .HasForeignKey<Email>(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Phone)
                .WithOne(c => c.Customer)
                .HasForeignKey<Phone>(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Address)
                .WithOne(c => c.Customer)
                .HasForeignKey<Address>(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(c => c.DeletedAt == null);
        }
    }
}
