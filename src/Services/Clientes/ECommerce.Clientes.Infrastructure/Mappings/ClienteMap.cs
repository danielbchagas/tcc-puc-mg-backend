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

            builder.Property(c => c.NomeFantasia).HasColumnType("varchar(100)").IsRequired();
            builder.Property(c => c.Cnpj).HasColumnType("varchar(18)").IsRequired();
            builder.Property(c => c.Ativo).HasColumnType("bit").HasDefaultValue(true).IsRequired();

            builder.HasOne(e => e.Endereco).WithOne(c => c.Cliente).HasForeignKey<Endereco>(c => c.ClienteId);
        }
    }
}
