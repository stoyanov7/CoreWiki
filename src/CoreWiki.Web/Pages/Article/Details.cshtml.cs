﻿namespace CoreWiki.Web.Pages.Article
{
    using System;
    using System.Threading.Tasks;
    using Application.Commands;
    using Application.Queries;
    using Data;
    using MediatR;
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
        private readonly IMediator mediator;
        private readonly IArticleService articleService;
        private readonly ICommentService commentService;
        private readonly IClock clock;

        public DetailsModel(
            CoreWikiContext context,
            IMediator mediator,
            IArticleService articleService,
            ICommentService commentService,
            IClock clock)
        {
            this.context = context;
            this.mediator = mediator;
            this.articleService = articleService;
            this.commentService = commentService;
            this.clock = clock;
        }

        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (slug == null)
            {
                return new ArticleNotFoundResult();
            }

            var query = new GetArticleDetailsQuery(slug);
            this.Article = await this.mediator.Send(query);

            if (this.Article == null)
            {
                return new ArticleNotFoundResult();
            }

            await this.ManageArticleIncrementCount(slug);

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

            await this.commentService.SetCommentToArticleAsync(comment, this.Article);
            
            await this.articleService.CanAuthorBeNotified(this.Article.AuthorId, this.Request.GetEncodedUrl());
            
            return this.Redirect($"/Article/Details/{this.Article.Slug}");
        }

        private async Task ManageArticleIncrementCount(string slug)
        {
            var incrementViewCount = (this.Request.Cookies[slug] == null);

            if (!incrementViewCount)
            {
                return;
            }

            this.Article.ViewCount++;
            this.Response.Cookies.Append(slug, "foo", new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMinutes(5)
            });

            await this.mediator.Send(new IncrementArticleViewCountCommand(slug));
        }
    }
}