using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Data.Configurations
{
    public class ProductTranslationConfiguration : IEntityTypeConfiguration<ProductTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable("ProductTranslations").HasKey(x => x.Id);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductTranslations)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Language)
                .WithMany(x => x.ProductTranslations)
                .HasForeignKey(x => x.LanguageId);
        }
    }
}
