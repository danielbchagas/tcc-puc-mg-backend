using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Cliente.Infrastructure.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Domain.Models.Cliente>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(c => c.Sobrenome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.Ativo)
                .HasColumnType("bit")
                .IsRequired();

            builder.HasOne(c => c.Documento)
                .WithOne(d => d.Cliente);

            builder.HasOne(c => c.Email)
                .WithOne(e => e.Cliente);

            builder.HasOne(c => c.Telefone)
                .WithOne(t => t.Cliente);

            builder.HasOne(c => c.Endereco)
                .WithOne(e => e.Cliente);
        }
    }
}
