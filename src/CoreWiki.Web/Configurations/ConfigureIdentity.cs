namespace CoreWiki.Web.Configurations
{
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Identity;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection service)
        {
            service.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.User.RequireUniqueEmail = true;
            })
            .AddDefaultUI()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<CoreWikiContext>();

            return service;
        }
    }
}