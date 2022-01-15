using ECommerce.Pedido.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Pedido.Infrastructure.Mappings
{
    public class TelefoneMap : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.ToTable("Telefones");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Numero)
                .HasColumnType("varchar(20)")
                .IsRequired();
        }
    }
}
