namespace CoreWiki.Web.Pages.Article
{
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Queries;
    using Common;
    using DiffPlex;
    using DiffPlex.DiffBuilder;
    using DiffPlex.DiffBuilder.Model;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models;

    public class HistoryModel : PageModel
    {
        private readonly IMediator mediator;

        public HistoryModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Article Article { get; private set; }

        [BindProperty]
        public string[] Compare { get; set; }

        public SideBySideDiffModel DiffModel { get; set; }

        public async Task<IActionResult> OnGet(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return this.NotFound();
            }

            this.Article = await this.mediator.Send(new GetArticleWithHistoryDetailsQuery(slug));

            if (this.Article is null)
            {
                return new ArticleNotFoundResult();
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPost(string slug)
        {
            this.Article = await this.mediator.Send(new GetArticleHistoryAndAuthorQuery(slug));

            var histories = this.Article
                .History
                .Where(h => this.Compare.Any(c => c == h.Version.ToString()))
                .OrderBy(h => h.Version)
                .ToArray();

            this.DiffModel = new SideBySideDiffBuilder(new Differ())
                .BuildDiffModel(histories[0].Content, histories[1].Content);

            return this.Page();
        }
    }
}