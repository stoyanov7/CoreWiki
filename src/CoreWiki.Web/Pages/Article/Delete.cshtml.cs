namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models;
    using Repository.Contracts;

    [Authorize("CanDeleteArticle")]
    public class DeleteModel : PageModel
    {
        private readonly CoreWikiContext context;
        private readonly IArticleRepository articleRepository;

        public DeleteModel(CoreWikiContext context, IArticleRepository articleRepository)
        {
            this.context = context;
            this.articleRepository = articleRepository;
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (slug == null)
            {
                return this.NotFound();
            }

            this.Article = await this.articleRepository.FindBySlugAsync(slug);

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

            this.articleRepository.Delete(slug);
            await this.articleRepository.SaveChangesAsync();
            
            return this.RedirectToPage("./Index");
        }
    }
}
