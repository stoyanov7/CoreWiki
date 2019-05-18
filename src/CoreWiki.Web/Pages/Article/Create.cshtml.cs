namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Data;
    using Models;

    public class CreateModel : PageModel
    {
        private readonly CoreWikiContext context;

        public CreateModel(CoreWikiContext context)
        {
            this.context = context;
        }

        public IActionResult OnGet() => this.Page();

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.context
                .Articles
                .Add(this.Article);

            await this.context.SaveChangesAsync();

            return this.RedirectToPage("./Index");
        }
    }
}