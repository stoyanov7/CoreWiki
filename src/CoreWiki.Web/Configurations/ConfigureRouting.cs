namespace CoreWiki.Web.Configurations
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddRoutingConfiguration(this IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services
                .AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return services;
        }
    }
}