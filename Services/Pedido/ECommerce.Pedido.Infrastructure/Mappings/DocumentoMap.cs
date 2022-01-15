using ECommerce.Pedido.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Pedido.Infrastructure.Mappings
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
        }
    }
}
