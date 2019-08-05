namespace CoreWiki.Web.Configurations
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContextPool<CoreWikiContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return service;
        }
    }
}