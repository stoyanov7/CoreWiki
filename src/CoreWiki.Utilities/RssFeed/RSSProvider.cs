namespace CoreWiki.Utilities.RssFeed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Snickler.RSSCore.Models;
    using Snickler.RSSCore.Providers;

    public class RssProvider : IRSSProvider
    {
        private readonly CoreWikiContext context;

        public RssProvider(CoreWikiContext context, IConfiguration configuration)
        {
            this.context = context;
            this.Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public async Task<IList<RSSItem>> RetrieveSyndicationItems()
        {
            var articles = await this.context
                .Articles
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
                    PublishDate = rssItem.PublishedOn,
                    LastUpdated = rssItem.PublishedOn
                };
                
                return wikiItem;
            }).ToList();
        }
    }
}