using ECommerce.Produtos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Produtos.Infrastructure.Mappings
{
    public class LogMap : IEntityTypeConfiguration<LogEvento>
    {
        public void Configure(EntityTypeBuilder<LogEvento> builder)
        {
            builder.ToTable("LogEventos");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Momento).HasColumnType("date").IsRequired();
            builder.Property(l => l.Uri).HasColumnType("varchar(50)").IsRequired();
            builder.Property(l => l.OrigemRequisicao).HasColumnType("varchar(50)").IsRequired();
            builder.Property(l => l.ProdutoId).HasColumnType("varchar(36)").IsRequired();
        }
    }
}
