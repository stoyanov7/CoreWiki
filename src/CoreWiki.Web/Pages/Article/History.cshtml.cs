namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using DiffPlex;
    using DiffPlex.DiffBuilder;
    using DiffPlex.DiffBuilder.Model;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models;
    using Services.Contracts;
    using Utilities;

    public class HistoryModel : PageModel
    {
        private readonly IArticleService articleService;

        public HistoryModel(IArticleService articleService)
        {
            this.articleService = articleService;
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

            this.Article = await this.articleService
                .GetArticleWithHistoryDetails<Article>(slug);

            if (this.Article is null)
            {
                return new ArticleNotFoundResult();
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPost(string slug)
        {
            this.Article = await this.articleService
                .GetArticleHistoryAndAuthor<Article>(slug);

            var histories = await this.articleService
                .GetHistory<ArticleHistory>(this.Compare);

            this.DiffModel = new SideBySideDiffBuilder(new Differ())
                .BuildDiffModel(histories[0].Content, histories[1].Content);

            return this.Page();
        }
    }
}