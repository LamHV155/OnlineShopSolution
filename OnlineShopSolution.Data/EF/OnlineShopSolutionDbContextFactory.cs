using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OnlineShopSolution.Data.EF
{
    public class OnlineShopSolutionDbContextFactory : IDesignTimeDbContextFactory<OnlineShopDbContext>
    {
        public OnlineShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .Build();

            var connectionString = configuration.GetConnectionString("OnlineShopDb");

            var optionBuilder = new DbContextOptionsBuilder<OnlineShopDbContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new OnlineShopDbContext(optionBuilder.Options);
        }
    }
}
