using ECommerce.Clientes.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Clientes.Infrastructure.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(c => c.Documento).HasColumnType("varchar(18)").IsRequired();
            builder.Property(c => c.Ativo).HasColumnType("bit").IsRequired();

            builder.HasOne(c => c.Documento).WithOne(d => d.Cliente).HasForeignKey<Documento>(d => d.ClienteId);
            builder.HasOne(c => c.Endereco).WithOne(e => e.Cliente).HasForeignKey<Endereco>(e => e.ClienteId);
        }
    }
}
