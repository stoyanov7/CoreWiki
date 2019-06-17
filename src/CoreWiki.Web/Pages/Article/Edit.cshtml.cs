namespace CoreWiki.Web.Pages.Article
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using NodaTime;
    using Utilities;

    public class EditModel : PageModel
    {
        private readonly CoreWikiContext context;
        private readonly IClock clock;

        public EditModel(CoreWikiContext context, IClock clock)
        {
            this.context = context;
            this.clock = clock;
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (slug == null)
            {
                return new ArticleNotFoundResult();
            }

            this.Article = await this.context
                .Articles
                .FirstOrDefaultAsync(m => m.Slug == slug);

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

            this.context
                .Attach(this.Article)
                .State = EntityState.Modified;

            var existingArticle = this.context
                .Articles
                .AsNoTracking()
                .First(a => a.Id == this.Article.Id);

            this.Article.ViewCount = existingArticle.ViewCount;
            this.Article.Version = existingArticle.Version + 1;

            this.Article.Published = this.clock.GetCurrentInstant();
            this.Article.Slug = UrlHelpers.UrlFriendly(this.Article.Topic.ToLower());
            this.Article.AuthorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.context.ArticleHistories.Add(ArticleHistory.FromArticle(this.Article));

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ArticleExists(this.Article.Topic))
                {
                    return new ArticleNotFoundResult();
                }
                else
                {
                    throw;
                }
            }

            return this.RedirectToPage("/Article/Details", new { slug = this.Article.Slug });
        }

        private bool ArticleExists(string slug)
             => this.context
                 .Articles
                 .Any(e => e.Slug == slug);
    }
}
