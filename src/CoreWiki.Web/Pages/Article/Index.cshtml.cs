namespace CoreWiki.Web.Pages.Article
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services.Contracts;

    public class IndexModel : PageModel
    {
        private readonly IArticleService articleService;

        public IndexModel(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public IList<IndexArticleDto> Article { get; set; }

        public async Task OnGetAsync()
        {
            this.Article = await this.articleService.GetAllArticlesAsync<IndexArticleDto>();
        }
    }
}
