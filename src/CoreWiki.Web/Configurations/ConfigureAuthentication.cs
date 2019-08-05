namespace CoreWiki.Web.Configurations
{
    using Common;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            if (!string.IsNullOrEmpty(configuration["Authentication:Microsoft:ClientId"]))
            {
                service.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = configuration["Authentication:Microsoft:ClientId"];
                    microsoftOptions.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
                });
            }

            service.AddAuthorization(AuthPolicy.Execute);

            return service;
        }

        public static IApplicationBuilder UseAuthenticationConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            return app;
        }
    }
}