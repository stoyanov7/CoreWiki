namespace CoreWiki.Web.Pages.Article
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services.Contracts;

    public class AllModel : PageModel
    {
        private readonly IArticleService articleService;
        private const int PageSize = 2;

        public AllModel(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [FromRoute]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }

        public IEnumerable<AllArticlesDto> Articles { get; set; }

        public async Task OnGet(int pageNumber = 1)
        {
            this.Articles = await this.articleService
                .GetAllArticlesAsync<AllArticlesDto>(this.PageNumber, PageSize);

            var count = this.articleService.GetCount();

            this.TotalPages = (int)Math.Ceiling(count / (double)PageSize);
        }
    }
}