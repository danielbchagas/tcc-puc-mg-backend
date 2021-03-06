using ECommerce.Customer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Customer.Infrastructure.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

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
                .WithOne(d => d.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Email)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Phone)
                .WithOne(t => t.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Address)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
