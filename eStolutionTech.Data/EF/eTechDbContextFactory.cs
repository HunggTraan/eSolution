using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eSolutionTech.Data.EF
{
    public class eTechDbContextFactory : IDesignTimeDbContextFactory<eTechDbContext>
    {
        public eTechDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("eTechDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<eTechDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new eTechDbContext(optionsBuilder.Options);
        }
    }
}