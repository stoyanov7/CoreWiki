namespace CoreWiki.Web.Pages.Article
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class LatestChangesModel : PageModel
    {
        private readonly CoreWikiContext context;

        public LatestChangesModel(CoreWikiContext context)
        {
            this.context = context;
        }

        public IList<Article> Articles { get; set; }

        public async Task OnGetAsync()
        {
            this.Articles = await this.context
                .Articles
                .OrderByDescending(x => x.PublishedOn)
                .Take(5)
                .ToListAsync();
        }
    }
}
