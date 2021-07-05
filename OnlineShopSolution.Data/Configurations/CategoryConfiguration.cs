using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopSolution.Data.Entities;
using OnlineShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories")
                    .HasKey(x => x.Id);
            builder.Property(x => x.Status).HasDefaultValue(Status.Active);
            
        }
    }
}
