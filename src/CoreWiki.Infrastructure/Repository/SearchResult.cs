namespace CoreWiki.Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Repository;
    using Models;

    public class SearchResult : ISearchResult
    {
        public string Query { get; set; }

        public IEnumerable<Article> Articles { get; set; }

        public int TotalResults => this.Articles.Count();
    }
}