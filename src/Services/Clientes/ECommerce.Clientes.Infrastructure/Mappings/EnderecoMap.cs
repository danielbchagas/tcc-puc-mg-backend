using ECommerce.Clientes.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Clientes.Infrastructure.Mappings
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Enderecos");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Logradouro).HasColumnType("varchar(200)").IsRequired();
            builder.Property(e => e.Bairro).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.Cidade).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.Cep).HasColumnType("varchar(9)").IsRequired();
            builder.Property(e => e.Estado).HasColumnType("char(2)").IsRequired();
        }
    }
}
