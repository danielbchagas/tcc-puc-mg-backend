using ECommerce.Cliente.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Cliente.Infrastructure.Mappings
{
    public class EmailMap : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.ToTable("Emails");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Endereco)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.ClienteId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
        }
    }
}
