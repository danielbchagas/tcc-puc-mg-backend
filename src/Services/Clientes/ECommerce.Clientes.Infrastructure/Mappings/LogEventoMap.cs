using ECommerce.Clientes.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Clientes.Infrastructure.Mappings
{
    public class LogEventoMap : IEntityTypeConfiguration<LogEvento>
    {
        public void Configure(EntityTypeBuilder<LogEvento> builder)
        {
            builder.ToTable("LogEventos");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Momento).HasColumnType("date").IsRequired();
            builder.Property(l => l.Uri).HasColumnType("varchar(50)").IsRequired();
            builder.Property(l => l.OrigemRequisicao).HasColumnType("varchar(50)").IsRequired();
            builder.Property(l => l.ClienteId).HasColumnType("varchar(36)").IsRequired();
        }
    }
}
