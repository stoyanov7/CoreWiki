namespace CoreWiki.Web.Pages
{
    using System.Linq;
    using Domain.Repository;
    using Domain.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class SearchModel : PageModel
    {
        private readonly IArticleSearchService articleSearchService;

        public SearchModel(IArticleSearchService articleSearchService)
        {
            this.articleSearchService = articleSearchService;
        }

        public ISearchResult SearchResult { get; private set; }

        public IActionResult OnGet()
        {
            var isQueryPresent = this.Request
                .Query
                .TryGetValue("searchValue", out var query);

            if (isQueryPresent && !string.IsNullOrEmpty(query.First()))
            {
                this.SearchResult = this.articleSearchService.Search(query.First());
            }

            return this.Page();
        }
    }
}