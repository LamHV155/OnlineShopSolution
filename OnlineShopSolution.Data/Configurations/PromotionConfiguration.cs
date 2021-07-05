using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopSolution.Data.Entities;
using OnlineShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Data.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions").HasKey(x => x.Id);
            builder.Property(x => x.ApplyForAll).HasDefaultValue(false);
            builder.Property(x => x.Status).HasDefaultValue(Status.Inactive);
        }
    }
}
