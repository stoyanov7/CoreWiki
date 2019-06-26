namespace CoreWiki.Services
{
    using System.Linq;
    using Contracts;
    using Data;
    using Utilities;

    public class ArticleSearchService : IArticleSearchService
    {
        private readonly CoreWikiContext context;

        public ArticleSearchService(CoreWikiContext context)
        {
            this.context = context;
        }

        public SearchResult Search(string query)
        {
            var trimmedQuery = query.Trim();

            var articles = this.context
                .Articles
                .Where(a =>
                    a.Topic.ToLower().Contains(trimmedQuery.ToLower()) ||
                    a.Content.ToLower().Contains(trimmedQuery.ToLower()))
                .OrderBy(a => a.Topic)
                .ToList();

            return new SearchResult
            {
                Query = trimmedQuery,
                Articles = articles
            };
        }
    }
}