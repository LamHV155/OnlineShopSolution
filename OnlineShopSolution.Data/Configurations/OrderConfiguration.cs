using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ShipEmail)
                .IsUnicode(false)
                .HasMaxLength(50);
            builder.Property(x => x.ShipName).IsRequired();
            builder.Property(x => x.ShipPhoneNumber).IsRequired();
            builder.Property(x => x.OrderDate).IsRequired();
        }
    }
}
