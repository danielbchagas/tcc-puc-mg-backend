﻿using ECommerce.Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Customers.Infrastructure.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstLine)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(e => e.SecondLine)
                .HasColumnType("varchar(200)")
                .IsRequired(false);

            builder.Property(e => e.City)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(e => e.ZipCode)
                .HasColumnType("varchar(9)")
                .IsRequired();

            builder.Property(e => e.State)
                .HasColumnType("char(2)")
                .IsRequired();
        }
    }
}
