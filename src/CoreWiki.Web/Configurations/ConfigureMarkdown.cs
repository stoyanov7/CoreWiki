namespace CoreWiki.Web.Configurations
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Westwind.AspNetCore.Markdown;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddMarkdownConfiguration(this IServiceCollection service)
        {
            service.AddMarkdown();

            return service;
        }

        public static IApplicationBuilder UseMarkdownConfiguration(this IApplicationBuilder app)
        {
            app.UseMarkdown();

            return app;
        }
    }
}