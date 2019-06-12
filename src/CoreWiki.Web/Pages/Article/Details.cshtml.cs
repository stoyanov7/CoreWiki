namespace CoreWiki.Web.Pages.Article
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Models;
    using Models.Identity;
    using NodaTime;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using Utilities;

    public class DetailsModel : PageModel
    {
        private readonly CoreWikiContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IClock clock;
        private readonly IConfiguration configuration;

        public DetailsModel(
            CoreWikiContext context,
            UserManager<ApplicationUser> userManager,
            IClock clock,
            IConfiguration configuration)
        {
            this.context = context;
            this.userManager = userManager;
            this.clock = clock;
            this.configuration = configuration;
        }

        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (slug == null)
            {
                return new ArticleNotFoundResult();
            }

            this.Article = await this.context
                .Articles
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(m => m.Slug == slug);

            if (this.Article == null)
            {
                return new ArticleNotFoundResult();
            }

            if (this.Request.Cookies[this.Article.Topic] == null)
            {
                this.Article.ViewCount++;

                this.Response.Cookies.Append(this.Article.Topic, "foo", new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(1)
                });
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
                return new ArticleNotFoundResult();
            }

            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            comment.Article = this.Article;
            comment.Submitted = this.clock.GetCurrentInstant();

            this.context.Comments.Add(comment);
            await this.context.SaveChangesAsync();

            // TODO: Add notification here
            var authorEmail = (await this.userManager.FindByIdAsync(this.Article.AuthorId)).Email;

            var message = new SendGridMessage();
            message.SetFrom(new EmailAddress("noreply@corewiki.com", "No Reply Team"));
            var recipient = new EmailAddress(authorEmail);

            message.AddTo(recipient);
            message.SetSubject("You have a new comment!");

            var thisUrl = this.Request.GetEncodedUrl();
            message.AddContent(MimeType.Text, "Test content");

            var apiKey = this.configuration["SendGridApiKey"];
            var client = new SendGridClient(apiKey);

            var response = await client.SendEmailAsync(message);

            return this.Redirect($"/Article/Details/{this.Article.Slug}");
        }
    }
}