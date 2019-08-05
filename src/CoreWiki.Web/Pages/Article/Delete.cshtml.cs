namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using Application.Commands;
    using Application.Queries;
    using Common.Constants;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize(PolicyConstants.CanDeleteArticle)]
    public class DeleteModel : PageModel
    {
        private readonly IMediator mediator;

        public DeleteModel(IMediator mediator)
        {
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

            this.Article = await this.mediator.Send(new DeleteArticleQuery(slug));

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
