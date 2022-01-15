using ECommerce.Carrinho.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Carrinho.Infrastructure.Mappings
{
    public class CarrinhoItemMap : IEntityTypeConfiguration<CarrinhoItem>
    {
        public void Configure(EntityTypeBuilder<CarrinhoItem> builder)
        {
            builder.ToTable("Itens");

            builder.HasKey(ci => ci.Id);

            builder.Property(ic => ic.Imagem)
                .HasColumnType("text")
                .IsRequired(false);
            builder.Property(ic => ic.Nome)
                .HasColumnType("varchar(20)")
                .IsRequired();
            builder.Property(ic => ic.Valor)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(ic => ic.Quantidade)
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
