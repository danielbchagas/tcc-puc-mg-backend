using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Pedido.Infrastructure.Mappings
{
    public class PedidoMap : IEntityTypeConfiguration<Domain.Models.Pedido>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Pedido> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Valor)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(c => c.Status)
                .HasColumnType("char(15)")
                .IsRequired();

            builder.HasOne(c => c.Cliente);

            builder.HasMany(c => c.Itens);
        }
    }
}
