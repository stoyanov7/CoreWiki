namespace CoreWiki.Web.Pages.Article
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class EditModel : PageModel
    {
        private readonly CoreWikiContext context;

        public EditModel(CoreWikiContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(string topic)
        {
            if (topic == null)
            {
                return this.NotFound();
            }

            this.Article = await this.context
                .Articles
                .FirstOrDefaultAsync(m => m.Topic == topic);

            if (this.Article == null)
            {
                return this.NotFound();
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.context
                .Attach(this.Article)
                .State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ArticleExists(this.Article.Topic))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.RedirectToPage("./Index");
        }

        private bool ArticleExists(string topic)
             => this.context
                 .Articles
                 .Any(e => e.Topic == topic);
    }
}
