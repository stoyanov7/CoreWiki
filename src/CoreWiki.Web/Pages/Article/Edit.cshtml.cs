namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using Application.Commands;
    using Application.Queries;
    using Common;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models;

    public class EditModel : PageModel
    {
        private readonly IMediator mediator;

        public EditModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (slug == null)
            {
                return new ArticleNotFoundResult();
            }

            this.Article = await this.mediator.Send(new GetArticleForEditQuery(slug));

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

            await this.mediator.Send(new EditArticleCommand(this.Article));
            
            return this.RedirectToPage("/Article/Details", new { slug = this.Article.Slug });
        }
    }
}
