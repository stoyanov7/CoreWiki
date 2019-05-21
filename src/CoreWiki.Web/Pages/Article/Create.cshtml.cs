﻿namespace CoreWiki.Web.Pages.Article
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Data;
    using Models;
    using NodaTime;

    public class CreateModel : PageModel
    {
        private readonly CoreWikiContext context;
        private readonly IClock clock;

        public CreateModel(CoreWikiContext context, IClock clock)
        {
            this.context = context;
            this.clock = clock;
        }

        public IActionResult OnGet() => this.Page();

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.Article.Published = this.clock.GetCurrentInstant();

            this.context
                .Articles
                .Add(this.Article);

            await this.context.SaveChangesAsync();

            return this.RedirectToPage("./Index");
        }
    }
}