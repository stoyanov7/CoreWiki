namespace CoreWiki.Web.Pages.Article
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Dto;
    using Application.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services.Contracts;

    public class AllModel : PageModel
    {
        private readonly IArticleService articleService;
        private readonly IMediator mediator;
        private const int PageSize = 2;

        public AllModel(IArticleService articleService, IMediator mediator)
        {
            this.articleService = articleService;
            this.mediator = mediator;
        }

        [FromRoute]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }

        public IEnumerable<AllArticlesDto> Articles { get; set; }

        public async Task OnGet(int pageNumber = 1)
        {
            this.Articles = await this.mediator
                .Send(new GetAllArticlesQuery(this.PageNumber, PageSize));
                
            var count = this.articleService.GetCount();

            this.TotalPages = (int)Math.Ceiling(count / (double)PageSize);
        }
    }
}