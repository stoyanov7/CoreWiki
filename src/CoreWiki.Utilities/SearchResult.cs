namespace CoreWiki.Utilities
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class SearchResult
    {
        public string Query { get; set; }

        public IEnumerable<Article> Articles { get; set; }

        public int TotalResults => this.Articles.Count();
    }
}