using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Data.Configurations
{
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductInCategory");
            builder.HasKey(x =>  new { x.CategoryId, x.ProductId});

            builder.HasOne(x => x.Product)
                    .WithMany(x => x.ProductInCategories)
                    .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Category)
                    .WithMany(x => x.ProductInCategories)
                    .HasForeignKey(x => x.CategoryId);
        }           
    }
}
