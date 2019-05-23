namespace CoreWiki.Web.Pages.Article
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class AllModel : PageModel
    {
        private readonly CoreWikiContext context;
        private const int PageSize = 2;

        public AllModel(CoreWikiContext context)
        {
            this.context = context;
        }

        [FromRoute]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }

        public IEnumerable<Article> Articles { get; set; }

        public async Task OnGet(int pageNumber = 1)
        {
            this.Articles = await this.context
                .Articles
                .AsNoTracking()
                .OrderBy(a => a.Topic)
                .Skip((this.PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToArrayAsync();

            var count = await this.context
                .Articles
                .CountAsync();

            this.TotalPages = (int)Math.Ceiling(count / (double)PageSize);

        }
    }
}