namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class DeleteModel : PageModel
    {
        private readonly CoreWikiContext context;

        public DeleteModel(CoreWikiContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await this.context
                .Articles
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Article == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await this.context
                .Articles
                .FindAsync(id);

            if (Article != null)
            {
                this.context
                    .Articles
                    .Remove(Article);

                await this.context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
