﻿using ECommerce.Baskets.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Baskets.Infrastructure.Mappings
{
    public class ItemMapping : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");

            builder.HasKey(ci => ci.Id);

            builder.Property(ic => ic.Name)
                .HasColumnType("varchar(20)")
                .IsRequired();
            builder.Property(ic => ic.Value)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(ic => ic.Quantity)
                .HasColumnType("int")
                .IsRequired();

            builder.HasQueryFilter(cc => cc.DeletedAt == null);
        }
    }
}
