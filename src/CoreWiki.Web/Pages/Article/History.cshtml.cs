namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Utilities;

    public class HistoryModel : PageModel
    {
        private readonly CoreWikiContext context;

        public HistoryModel(CoreWikiContext context)
        {
            this.context = context;
        }

        public Article Article { get; private set; }

        public ArticleHistory History { get; set; }

        public async Task<IActionResult> OnGet(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return this.NotFound();
            }

            this.Article = await this.context
                .Articles
                .Include(h => h.History)
                .SingleOrDefaultAsync(s => s.Slug == slug);

            if (this.Article is null)
            {
                return new ArticleNotFoundResult();
            }

            return this.Page();
        }
    }
}