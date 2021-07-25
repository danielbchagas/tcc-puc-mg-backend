using ECommerce.Produtos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Produtos.Infrastructure.Mappings
{
    class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Marca).HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.Nome).HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.Observacao).HasColumnType("varchar(200)").IsRequired();
            builder.Property(p => p.Quantidade).HasColumnType("int").IsRequired();
            builder.Property(p => p.Vencimento).HasColumnType("date").IsRequired();
            builder.Property(p => p.Fabricacao).HasColumnType("date").IsRequired();
        }
    }
}
