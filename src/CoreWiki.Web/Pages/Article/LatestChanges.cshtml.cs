namespace CoreWiki.Web.Pages.Article
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application;
    using Application.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class LatestChangesModel : PageModel
    {
        private readonly IMediator mediator;

        public LatestChangesModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IEnumerable<LatestArticleDto> Articles { get; set; }

        public async Task OnGetAsync()
        {
            var query = new GetLatestArticlesQuery(this.Articles);
            this.Articles = await this.mediator.Send(query);
        }
    }
}
