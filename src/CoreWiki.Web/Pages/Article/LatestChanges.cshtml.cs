namespace CoreWiki.Web.Pages.Article
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services.Contracts;

    public class LatestChangesModel : PageModel
    {
        private readonly IArticleService articleService;

        public LatestChangesModel(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public IList<LatestArticleDto> Articles { get; set; }

        public async Task OnGetAsync()
        {
            this.Articles = await this.articleService.GetAllArticlesAsync<LatestArticleDto>();
        }
    }
}
