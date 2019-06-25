namespace CoreWiki.Web.Pages.Article
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models;
    using Repository.Contracts;

    public class IndexModel : PageModel
    {
        private readonly IArticleRepository articleRepository;

        public IndexModel(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public IList<Article> Article { get; set; }

        public async Task OnGetAsync()
        {
            this.Article = await this.articleRepository.All();

            foreach (var current in this.Article)
            {
                if (current.Content.Length >= 50)
                {
                    current.Content = current.Content.Substring(0, 50) + "...";
                }
            }
        }
    }
}
