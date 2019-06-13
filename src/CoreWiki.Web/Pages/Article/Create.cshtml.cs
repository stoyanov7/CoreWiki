namespace CoreWiki.Web.Pages.Article
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;
    using Models;
    using NodaTime;
    using Utilities;

    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CoreWikiContext context;
        private readonly IClock clock;
        private readonly ILogger<CreateModel> logger;

        public CreateModel(CoreWikiContext context, IClock clock, ILogger<CreateModel> logger)
        {
            this.context = context;
            this.clock = clock;
            this.logger = logger;
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

            var isTopicExist = this.context
                .Articles
                .Any(x => x.Topic == this.Article.Topic);

            if (isTopicExist)
            {
                this.ModelState.AddModelError($"{nameof(this.Article)}.{nameof(this.Article.Topic)}",
                    $"The topic '{this.Article.Topic}' already exists.  Please choose another name!");

                this.logger.LogInformation($"The topic with name - {this.Article.Topic} exist.");

                return this.Page();
            }

            this.Article.Published = this.clock.GetCurrentInstant();
            this.Article.Slug = UrlHelpers.UrlFriendly(this.Article.Topic.ToLower());
            this.Article.AuthorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.context
                .Articles
                .Add(this.Article);

            await this.context.SaveChangesAsync();

            this.logger.LogInformation($"Create new article with topic name - {this.Article.Topic}");

            return this.RedirectToPage("./Index");
        }
    }
}