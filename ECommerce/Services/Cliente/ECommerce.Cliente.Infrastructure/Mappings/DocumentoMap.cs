using ECommerce.Cliente.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Cliente.Infrastructure.Mappings
{
    public class DocumentoMap : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.ToTable("Documentos");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Numero)
                .HasColumnType("varchar(18)")
                .IsRequired();

            builder.Property(e => e.ClienteId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
        }
    }
}
