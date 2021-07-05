using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopSolution.Data.Entities;
using OnlineShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopSolution.Data.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts").HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(Status.Active);
        }
    }
}
