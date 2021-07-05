﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopSolution.Data.Entities;
using OnlineShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;


namespace OnlineShopSolution.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions").HasKey(x => x.Id);
        }
    }
}
