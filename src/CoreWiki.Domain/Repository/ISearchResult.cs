namespace CoreWiki.Domain.Repository
{
    using System.Collections.Generic;
    using Models;

    public interface ISearchResult
    {
        string Query { get; }

        IEnumerable<Article> Articles { get; }

        int TotalResults { get; }
    }
}