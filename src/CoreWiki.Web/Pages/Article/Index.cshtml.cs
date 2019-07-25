namespace CoreWiki.Web.Pages.Article
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Dto;
    using Application.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IList<IndexArticleDto> Article { get; set; }

        public async Task OnGetAsync()
        {
            var query = new GetArticlesForIndexPageQuery(this.Article);

            this.Article = await this.mediator.Send(query);
        }
    }
}
