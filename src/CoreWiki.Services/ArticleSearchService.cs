namespace CoreWiki.Services
{
    using Contracts;
    using Repository.Contracts;
    using Utilities;

    public class ArticleSearchService : IArticleSearchService
    {
        private readonly IArticleRepository articleRepository;

        public ArticleSearchService(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public SearchResult Search(string query)
        {
            var trimmedQuery = query.Trim();

            var articles = this.articleRepository
                .Get(a =>
                    a.Topic.ToLower().Contains(trimmedQuery.ToLower()) ||
                    a.Content.ToLower().Contains(trimmedQuery.ToLower()));

            return new SearchResult
            {
                Query = trimmedQuery,
                Articles = articles
            };
        }
    }
}