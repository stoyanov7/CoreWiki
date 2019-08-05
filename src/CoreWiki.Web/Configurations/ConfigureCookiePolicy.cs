namespace CoreWiki.Web.Configurations
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddCookiePolicyConfiguration(this IServiceCollection service)
        {
            service.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            return service;
        }

        public static IApplicationBuilder UseCookiePolicyConfiguration(this IApplicationBuilder app)
        {
            app.UseCookiePolicy();

            return app;
        }
    }
}