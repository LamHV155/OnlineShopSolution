using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Data.Configurations
{
    class CategoryTranslationConfiguration : IEntityTypeConfiguration<CategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.ToTable("CategoryStranlations").HasKey(x => x.Id);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.CategoryTranslations)
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.Language)
                .WithMany(x => x.CategoryTranslations)
                .HasForeignKey(x => x.LanguageId);

        }
    }
}
