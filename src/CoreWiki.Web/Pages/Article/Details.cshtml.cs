namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using NodaTime;

    public class DetailsModel : PageModel
    {
        private readonly CoreWikiContext context;
        private readonly IClock clock;

        public DetailsModel(CoreWikiContext context, IClock clock)
        {
            this.context = context;
            this.clock = clock;
        }

        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (slug == null)
            {
                return this.NotFound();
            }

            this.Article = await this.context
                .Articles
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(m => m.Slug == slug);

            if (this.Article == null)
            {
                return this.NotFound();
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(Comment comment)
        {
            this.TryValidateModel(comment);

            this.Article = await this.context
                .Articles
                .Include(x => x.Comments)
                .SingleOrDefaultAsync(m => m.Id == comment.ArticleId);

            if (this.Article == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            comment.Article = this.Article;
            comment.Submitted = this.clock.GetCurrentInstant();

            this.context.Comments.Add(comment);
            await this.context.SaveChangesAsync();

            return this.Redirect($"/Article/Details/{this.Article.Slug}");
        }
    }
}