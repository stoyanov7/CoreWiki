﻿namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class DetailsModel : PageModel
    {
        private readonly CoreWikiContext context;

        public DetailsModel(CoreWikiContext context)
        {
            this.context = context;
        }

        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            this.Article = await this.context
                .Articles
                .FirstOrDefaultAsync(m => m.Id == id);

            if (this.Article == null)
            {
                return this.NotFound();
            }

            return this.Page();
        }
    }
}