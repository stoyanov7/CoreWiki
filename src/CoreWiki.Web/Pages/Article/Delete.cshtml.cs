namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using Application.Commands;
    using Dto;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services.Contracts;
    using Utilities.Constants;

    [Authorize(PolicyConstants.CanDeleteArticle)]
    public class DeleteModel : PageModel
    {
        private readonly IArticleService articleService;
        private readonly IMediator mediator;

        public DeleteModel(IArticleService articleService, IMediator mediator)
        {
            this.articleService = articleService;
            this.mediator = mediator;
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

            await this.mediator.Send(new DeleteArticleCommand(slug));
            
            return this.RedirectToPage("./Index");
        }
    }
}
