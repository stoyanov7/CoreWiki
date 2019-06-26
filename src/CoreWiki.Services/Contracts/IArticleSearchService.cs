namespace CoreWiki.Services.Contracts
{
    using Utilities;

    public interface IArticleSearchService
    {
        SearchResult Search(string query);
    }
}