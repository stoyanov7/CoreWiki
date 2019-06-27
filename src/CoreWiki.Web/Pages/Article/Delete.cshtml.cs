namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using Dto;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services.Contracts;
    using Utilities.Constants;

    [Authorize(PolicyConstants.CanDeleteArticle)]
    public class DeleteModel : PageModel
    {
        private readonly IArticleService articleService;

        public DeleteModel(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [BindProperty]
        public DeleteArticleDto Article { get; set; }

        public async Task<IActionResult> OnGet(string slug)
        {
            if (slug == null)
            {
                return this.NotFound();
            }

            this.Article = await this.articleService.FindBySlugAsync<DeleteArticleDto>(slug);

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

            await this.articleService.Delete(slug);
            
            return this.RedirectToPage("./Index");
        }
    }
}
