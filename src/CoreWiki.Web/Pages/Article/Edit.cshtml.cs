namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models;
    using Repository.Contracts;
    using Utilities;

    public class EditModel : PageModel
    {
        private readonly IArticleRepository articleRepository;
        
        public EditModel(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (slug == null)
            {
                return new ArticleNotFoundResult();
            }

            this.Article = await this.articleRepository.FindBySlugAsync(slug);

            if (this.Article == null)
            {
                return new ArticleNotFoundResult();
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            try
            {
                await this.articleRepository.UpdateAsync(this.Article);
            }
            catch (ArticleNotFoundException)
            {
                return new ArticleNotFoundResult();
            }

            return this.RedirectToPage("/Article/Details", new { slug = this.Article.Slug });
        }
    }
}
