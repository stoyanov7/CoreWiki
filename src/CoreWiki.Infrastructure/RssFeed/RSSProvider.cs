namespace CoreWiki.Infrastructure.RssFeed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Repository;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Snickler.RSSCore.Models;
    using Snickler.RSSCore.Providers;

    public class RssProvider : IRSSProvider
    {
        private readonly IArticleRepository context;

        public RssProvider(IArticleRepository context, IConfiguration configuration)
        {
            this.context = context;
            this.Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public async Task<IList<RSSItem>> RetrieveSyndicationItems()
        {
            var articles = await this.context
                .Details()
                .OrderByDescending(a => a.Published)
                .Take(10)
                .ToListAsync();

            return articles.Select(rssItem =>
            {
                var wikiItem = new RSSItem
                {
                    Title = rssItem.Topic,
                    Content = rssItem.Content,  
                    PermaLink = new Uri($"{this.Configuration["UrlDetails"]}{rssItem.Slug}", UriKind.Absolute),
                    LinkUri = new Uri($"{this.Configuration["UrlDetails"]}{rssItem.Slug}", UriKind.Absolute),
                    PublishDate = rssItem.Published.ToDateTimeUtc(),
                    LastUpdated = rssItem.Published.ToDateTimeUtc()
                };
                
                return wikiItem;
            }).ToList();
        }
    }
}