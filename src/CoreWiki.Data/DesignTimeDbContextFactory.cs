namespace CoreWiki.Data
{
    using System;
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CoreWikiContext>
    {
        public CoreWikiContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var appsettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "../CoreWiki.Web");

            if (!File.Exists(appsettingsPath))
            {
                throw new DirectoryNotFoundException("The appsettings.json file was not found.");
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(appsettingsPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var builder = new DbContextOptionsBuilder<CoreWikiContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new CoreWikiContext(builder.Options);
        }
    }
}