namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models;

    [Authorize("CanDeleteArticle")]
    public class DeleteModel : PageModel
    {
        private readonly CoreWikiContext context;

        public DeleteModel(CoreWikiContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (slug == null)
            {
                return this.NotFound();
            }

            this.Article = await this.context
                .Articles
                .FirstOrDefaultAsync(m => m.Slug == slug);

            if (this.Article == null)
            {
                return this.NotFound();
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(string slug)
        {
            if (slug == null)
            {
                return this.NotFound();
            }

            this.Article = await this.context
                .Articles
                .FindAsync(slug);

            if (this.Article != null)
            {
                this.context
                    .Articles
                    .Remove(this.Article);

                await this.context.SaveChangesAsync();
            }

            return this.RedirectToPage("./Index");
        }
    }
}
