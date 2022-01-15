using ECommerce.Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Catalogo.Infrastructure.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao)
                .HasColumnType("varchar(2000)")
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.Quantidade)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Imagem)
                .HasColumnType("text")
                .IsRequired(false);

            builder.Property(p => p.Ativo)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(p => p.Valor)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(p => p.DataCadastro)
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
