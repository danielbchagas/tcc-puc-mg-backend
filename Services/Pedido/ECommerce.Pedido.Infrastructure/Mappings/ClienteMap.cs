using ECommerce.Pedido.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Pedido.Infrastructure.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(c => c.Sobrenome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.HasOne(c => c.Documento)
                .WithOne(d => d.Cliente);

            builder.HasOne(c => c.Email)
                .WithOne(d => d.Cliente);

            builder.HasOne(c => c.Telefone)
                .WithOne(d => d.Cliente);

            builder.HasOne(c => c.Endereco)
                .WithOne(d => d.Cliente);
        }
    }
}
