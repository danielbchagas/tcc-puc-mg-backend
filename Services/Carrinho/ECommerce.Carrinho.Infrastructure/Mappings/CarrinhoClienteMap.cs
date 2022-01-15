using ECommerce.Carrinho.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Carrinho.Infrastructure.Mappings
{
    public class CarrinhoClienteMap : IEntityTypeConfiguration<CarrinhoCompras>
    {
        public void Configure(EntityTypeBuilder<CarrinhoCompras> builder)
        {
            builder.ToTable("Carrinhos");

            builder.HasKey(cc => cc.Id);

            builder.HasIndex(cc => cc.ClienteId);

            builder.Property(cc => cc.Valor)
                .HasColumnType("money");

            builder.HasMany(cc => cc.Itens)
                .WithOne(ic => ic.Carrinho)
                .HasForeignKey(ic => ic.CarrinhoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
