namespace CoreWiki.Application.Queries
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Services.Contracts;

    public class GetArticlesForIndexPageQueryHandler : IRequestHandler<GetArticlesForIndexPageQuery, IList<IndexArticleDto>>
    {
        private readonly IArticleService articleService;

        public GetArticlesForIndexPageQueryHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<IList<IndexArticleDto>> Handle(
            GetArticlesForIndexPageQuery request,
            CancellationToken cancellationToken)
        {
            return await this.articleService.GetAllArticlesAsync<IndexArticleDto>();
        }
    }
}