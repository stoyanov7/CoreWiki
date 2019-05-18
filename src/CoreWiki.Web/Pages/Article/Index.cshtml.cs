namespace CoreWiki.Web.Pages.Article
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class IndexModel : PageModel
    {
        private readonly CoreWikiContext context;

        public IndexModel(CoreWikiContext context)
        {
            this.context = context;
        }

        public IList<Article> Article { get;set; }

        public async Task OnGetAsync()
        {
            this.Article = await this.context
                .Articles
                .ToListAsync();
        }
    }
}
