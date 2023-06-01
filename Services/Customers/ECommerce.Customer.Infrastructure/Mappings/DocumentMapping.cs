using ECommerce.Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Customers.Infrastructure.Mappings
{
    public class DocumentMapping : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Number)
                .HasColumnType("varchar(18)")
                .IsRequired();
        }
    }
}
