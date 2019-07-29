namespace CoreWiki.Domain.Services
{
    using Repository;

    public interface IArticleSearchService
    {
        ISearchResult Search(string query);
    }
}