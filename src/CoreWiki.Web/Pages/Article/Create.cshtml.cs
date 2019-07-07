namespace CoreWiki.Web.Pages.Article
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Dto;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;
    using Services.Contracts;

    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IArticleService articleService;
        private readonly ILogger<CreateModel> logger;

        public CreateModel(
            IArticleService articleService,
            ILogger<CreateModel> logger)
        {
            this.articleService = articleService;
            this.logger = logger;
        }

        public IActionResult OnGet() => this.Page();

        [BindProperty]
        public CreateArticleDto Article { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var isTopicExist = this.articleService.IsArticleExist(this.Article.Topic);

            if (isTopicExist)
            {
                this.ModelState.AddModelError($"{nameof(this.Article)}.{nameof(this.Article.Topic)}",
                    $"The topic '{this.Article.Topic}' already exists.  Please choose another name!");

                this.logger.LogInformation($"The topic with name - {this.Article.Topic} exist.");

                return this.Page();
            }

            await this.articleService.Create(this.Article.Topic, this.Article.Content, this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            this.logger.LogInformation($"Create new article with topic name - {this.Article.Topic}");

            return this.RedirectToPage("./Index");
        }
    }
}