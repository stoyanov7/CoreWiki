namespace CoreWiki.Web.Configurations
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection service, IConfiguration configuration)
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
    }
}