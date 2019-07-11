namespace CoreWiki.Web.Pages.Article
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models;
    using NodaTime;
    using Services.Contracts;
    using Utilities;

    public class DetailsModel : PageModel
    {
        private readonly CoreWikiContext context;
        private readonly IArticleService articleService;
        private readonly IClock clock;

        public DetailsModel(
            CoreWikiContext context,
            IArticleService articleService,
            IClock clock)
        {
            this.context = context;
            this.articleService = articleService;
            this.clock = clock;
        }

        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (slug == null)
            {
                return new ArticleNotFoundResult();
            }

            this.Article = await this.articleService.DetailsAsync<Article>(slug);

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
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.TryValidateModel(comment);

            this.Article = await this.articleService.DetailsAsync<Article>(comment);

            if (this.Article == null)
            {
                return new ArticleNotFoundResult();
            }
            
            comment.Article = this.Article;
            comment.Submitted = this.clock.GetCurrentInstant();

            this.context.Comments.Add(comment);
            await this.context.SaveChangesAsync();
            
            await this.articleService.CanAuthorBeNotified(this.Article.AuthorId, this.Request.GetEncodedUrl());
            
            return this.Redirect($"/Article/Details/{this.Article.Slug}");
        }
    }
}