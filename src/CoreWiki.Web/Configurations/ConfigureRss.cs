namespace CoreWiki.Web.Configurations
{
    using System;
    using Infrastructure.RssFeed;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Snickler.RSSCore.Extensions;
    using Snickler.RSSCore.Models;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddRssConfiguration(this IServiceCollection services)
        {
            services.AddRSSFeed<RssProvider>();

            return services;
        }

        public static IApplicationBuilder UseRssConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseRSSFeed("/feed", new RSSFeedOptions
            {
                Title = "CoreWiki RSS Feed",
                Copyright = DateTime.UtcNow.Year.ToString(),
                Description = "RSS Feed for CoreWiki",
                Url = new Uri(configuration["Url"], UriKind.Absolute)
            });

            return app;
        }
    }
}