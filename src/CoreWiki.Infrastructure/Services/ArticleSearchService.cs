namespace CoreWiki.Infrastructure.Services
{
    using Domain.Repository;
    using Domain.Services;
    using Repository;

    public class ArticleSearchService : IArticleSearchService
    {
        private readonly IArticleRepository articleRepository;

        public ArticleSearchService(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public ISearchResult Search(string query)
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